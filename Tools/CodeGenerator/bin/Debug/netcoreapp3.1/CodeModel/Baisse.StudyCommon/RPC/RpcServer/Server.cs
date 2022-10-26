using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baisse.StudyCommon.RPC.RpcServer
{
    /// <summary>
    /// 实现套接字服务器的连接逻辑。接受连接后，从客户端读取的所有数据被发送回客户端。读取并返回到客户端模式直到客户端断开连接。
    /// </summary>
    public class Server
    {
        private int m_maxConnectNum;    //最大连接数  
        private int m_revBufferSize;    //最大接收字节数  
        BufferManager m_bufferManager; //缓冲区管理器
        const int opsToAlloc = 2;
        Socket listenSocket;            //监听Socket  
        public SocketEventPool m_pool; //异步套接字连接池
        int m_clientCount;              //连接的客户端数量  
        //信号量， 可以用来控制同时访问特定资源的线程数量，通过协调各个线程，以保证合理的使用资源。
        //可以把它简单的理解成我们停车场入口立着的那个显示屏，每有一辆车进入停车场显示屏就会显示剩余车位减1，每有一辆车从停车场出去，显示屏上显示的剩余车辆就会加1，
        //当显示屏上的剩余车位为0时，停车场入口的栏杆就不会再打开，车辆就无法进入停车场了，直到有一辆车从停车场出去为止。
        Semaphore m_maxNumberAcceptedClients; //限制可以访问资源或资源池的线程数
        List<AsyncUserToken> m_clients; //客户端列表  


        #region
        /// <summary>  
        /// 客户端连接数量变化时触发  
        /// </summary>  
        /// <param name="num">当前增加客户的个数(用户退出时为负数,增加时为正数,为1)</param>  
        /// <param name="token">增加用户的信息</param>  
        public delegate void OnClientNumberChange(int num, AsyncUserToken token);
        /// <summary>  
        /// 接收到客户端的数据  
        /// </summary>  
        /// <param name="token">客户端</param>  
        /// <param name="buff">客户端数据</param>  
        public delegate void OnReceiveData(AsyncUserToken token, byte[] buff);

        /// <summary>
        /// 通知界面，server已经停止
        /// </summary>
        public delegate void stopedDel();
        public event stopedDel ServerStopedEvent;

        /// <summary>  
        /// 客户端连接数量变化事件  
        /// </summary>  
        public event OnClientNumberChange ClientNumberChange;

        /// <summary>  
        /// 接收到客户端的数据事件  
        /// </summary>  
        public event OnReceiveData ReceiveClientData;


        #endregion

        #region 定义属性

        /// <summary>  
        /// 获取客户端列表  
        /// </summary>  
        public List<AsyncUserToken> ClientList { get { return m_clients; } }

        #endregion

        /// <summary>  
        /// 实例化服务  
        /// </summary>  
        /// <param name="numConnections">最大连接数</param>  
        /// <param name="receiveBufferSize">最大接收字节数</param>  
        public Server(int numConnections, int receiveBufferSize)
        {
            m_clientCount = 0;//连接的客户端数量
            m_maxConnectNum = numConnections;//最大连接数
            m_revBufferSize = receiveBufferSize;//最大接收字节数
            //分配缓冲区，以便最大数量的套接字可以有一个未完成的读取和
            //同时写入到套接字  
            m_bufferManager = new BufferManager(receiveBufferSize * numConnections * opsToAlloc, receiveBufferSize);
            ///套接字事件池
            m_pool = new SocketEventPool(numConnections);
            //控制同时访问特定资源的线程数量
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        /// <summary>  
        /// 初始化  
        /// </summary>  
        public void Init()
        {
            //分配一个大字节缓冲区，所有I/O操作都使用该缓冲区防止内存碎片.
            m_bufferManager.InitBuffer();
            //初始化客户端列表
            m_clients = new List<AsyncUserToken>();
            // preallocate pool of SocketAsyncEventArgs objects  
            SocketAsyncEventArgs readWriteEventArg;

            for (int i = 0; i < m_maxConnectNum; i++)
            {
                readWriteEventArg = new SocketAsyncEventArgs();
                //用于完成异步操作的事件。
                readWriteEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                //获取或设置与此异步套接字关联的用户或应用程序对象操作。
                readWriteEventArg.UserToken = new AsyncUserToken();
                // 将缓冲池中的字节缓冲区分配给SocketAsyncEventArg对象
                m_bufferManager.SetBuffer(readWriteEventArg);
                // add SocketAsyncEventArg to the pool  
                //add异步套接字连接池
                m_pool.Push(readWriteEventArg);
            }
        }


        /// <summary>  
        /// 启动服务  
        /// </summary>  
        /// <param name="localEndPoint"></param>  
        public bool Start(IPEndPoint localEndPoint)
        {
            try
            {
                //清除客户端列表
                m_clients.Clear();
                //初始化Socket对象
                listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                //关联具有本地端口的套接字。
                listenSocket.Bind(localEndPoint);
                // 开始监听端口 m_maxConnectNum：连接数
                listenSocket.Listen(m_maxConnectNum);
                // post accepts on the listening socket  
                StartAccept(null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 停止服务  
        /// </summary>  
        public void Stop()
        {
            AsyncUserToken[] clients = m_clients.ToArray();
            for (int i = 0; i < clients.Length; i++)
            {
                try
                {
                    AsyncUserToken token = clients[i];
                    //关闭用户socket 确定数据是否发送成功，因为调用shutdown()时只有在缓存中的数据全部发送成功后才会返回。
                    token.Socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
            }

            try
            {
                if (m_clients.Count > 0)
                    //断开连接监听的socket
                    listenSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)//当socket 无连接时（即没有可用已连接的client）
            {
                // listenSocket.Disconnect(false);///因为无可用的socket连接，此方法不可用
                Console.WriteLine(e.Message);
            }
            finally
            {
                //关闭socket
                listenSocket.Close();
                //通知界面，server已经停止
                if (ServerStopedEvent != null)
                    ServerStopedEvent();
                lock (m_clients)
                {
                    //清空客户端列表
                    m_clients.Clear();
                }

            }


        }


        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="token"></param>
        public void CloseClient(AsyncUserToken token)
        {
            try
            {
                token.Socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 开始接受来自客户端的连接请求的操作
        /// </summary>
        /// <param name="acceptEventArg"></param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                //用于完成异步操作的事件。
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                // 由于上下文对象正在重用，因此必须清除套接字
                acceptEventArg.AcceptSocket = null;
            }
            //等待
            m_maxNumberAcceptedClients.WaitOne();

            if (!listenSocket.AcceptAsync(acceptEventArg))
            {
                ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// 此方法是与Socket关联的回调方法。接受异步操作，并在接受操作完成时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        /// 进程接受
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                //递增指定变量并将结果存储为原子操作。
                Interlocked.Increment(ref m_clientCount);
                //获取接受的客户端连接的套接字并将其放入ReadEventArg对象用户令牌
                SocketAsyncEventArgs readEventArgs = m_pool.Pop();
                AsyncUserToken userToken = (AsyncUserToken)readEventArgs.UserToken;
                userToken.usernamr = "Mtt";
                userToken.Socket = e.AcceptSocket;
                userToken.ConnectTime = DateTime.Now;
                userToken.Remote = e.AcceptSocket.RemoteEndPoint;
                userToken.IPAddress = ((IPEndPoint)(e.AcceptSocket.RemoteEndPoint)).Address;
                ClientList.Add(userToken);

                //添加客户端列表
                lock (m_clients) 
                { 
                    m_clients.Add(userToken); 
                }
                //客户连接事件
                if (ClientNumberChange != null)
                    ClientNumberChange(1, userToken);

                //获取或设置要使用的套接字或为接受连接而创建的套接符,接收消息
                if (!e.AcceptSocket.ReceiveAsync(readEventArgs))
                {
                    ProcessReceive(readEventArgs);
                }
            }
            catch (Exception me)
            {
                Console.WriteLine(me.Message);

            }

            // 接受下一个连接请求
            if (e.SocketError == SocketError.OperationAborted) return;
            StartAccept(e);
        }


        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler  
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }

        }


        // This method is invoked when an asynchronous receive operation completes.   
        // If the remote host closed the connection, then the socket is closed.    
        // If data was received then the data is echoed back to the client.  
        //  
        /// <summary>
        /// 异步接收操作完成时调用此方法。如果远程主机关闭了连接，则套接字将关闭。如果接收到数据，则将数据回送回客户端。
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                // check if the remote host closed the connection  
                AsyncUserToken token = (AsyncUserToken)e.UserToken;
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    //读取数据  
                    byte[] data = new byte[e.BytesTransferred];
                    Array.Copy(e.Buffer, e.Offset, data, 0, e.BytesTransferred);
                    lock (token.Buffer)
                    {
                        token.Buffer.AddRange(data);
                    }
                    do
                    {
                        //判断包的长度  
                        byte[] lenBytes = token.Buffer.GetRange(0, 4).ToArray();
                        int packageLen = BitConverter.ToInt32(lenBytes, 0);
                        if (packageLen > token.Buffer.Count - 4)
                        {   //长度不够时,退出循环,让程序继续接收  
                            break;
                        }

                        //包够长时,则提取出来,交给后面的程序去处理  
                        byte[] rev = token.Buffer.GetRange(4, packageLen).ToArray();
                        //从数据池中移除这组数据  
                        lock (token.Buffer)
                        {
                            token.Buffer.RemoveRange(0, packageLen + 4);
                        }

                        if (ReceiveClientData != null)
                            ReceiveClientData(token, rev);

                    } while (token.Buffer.Count > 4);
                    if (!token.Socket.ReceiveAsync(e))
                        this.ProcessReceive(e);
                }
                else
                {
                    CloseClientSocket(e);
                }
            }
            catch (Exception xe)
            {
                Console.WriteLine(xe.Message + "\r\n" + xe.StackTrace);
            }
        }

        // This method is invoked when an asynchronous send operation completes.    
        // The method issues another receive on the socket to read any additional   
        // data sent from the client  
        //  
        // <param name="e"></param>  
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client  
                AsyncUserToken token = (AsyncUserToken)e.UserToken;
                // read the next block of data send from the client  
                bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        //关闭客户端  
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = e.UserToken as AsyncUserToken;
            ClientList.Remove(token);

            lock (m_clients) { m_clients.Remove(token); }
            //如果有事件,则调用事件,发送客户端数量变化通知  
            if (ClientNumberChange != null)
                ClientNumberChange(-1, token);
            // close the socket associated with the client  
            try
            {
                token.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception) { }
            token.Socket.Close();
            // decrement the counter keeping track of the total number of clients connected to the server  
            Interlocked.Decrement(ref m_clientCount);
            m_maxNumberAcceptedClients.Release();
            // Free the SocketAsyncEventArg so they can be reused by another client  
            e.UserToken = new AsyncUserToken();
            m_pool.Push(e);
        }

        /// <summary>  
        /// 对数据进行打包,然后再发送  
        /// </summary>  
        /// <param name="token"></param>  
        /// <param name="message"></param>  
        /// <returns></returns>  
        public bool SendMessage(AsyncUserToken token, byte[] message)
        {
            bool isSuccess = false;
            if (token == null || token.Socket == null || !token.Socket.Connected)
                return isSuccess;

            try
            {
                //对要发送的消息,制定简单协议,头4字节指定包的大小,方便客户端接收(协议可以自己定)  
                byte[] buff = new byte[message.Length + 4];
                byte[] len = BitConverter.GetBytes(message.Length);
                Array.Copy(len, buff, 4);
                Array.Copy(message, 0, buff, 4, message.Length);
                //token.Socket.Send(buff);  //
                //新建异步发送对象, 发送消息  
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                sendArg.UserToken = token;
                sendArg.SetBuffer(buff, 0, buff.Length);  //将数据放置进去.  
                isSuccess = token.Socket.SendAsync(sendArg);
            }
            catch (Exception e)
            {
                Console.WriteLine("SendMessage - Error:" + e.Message);
            }
            return isSuccess;
        }

    }
}
