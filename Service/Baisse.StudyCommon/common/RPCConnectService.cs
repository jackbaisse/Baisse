using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Baisse.StudyCommon.common
{
    public class RPCConnectService<T> where T : new()
    {
        byte[] buffer = new byte[1024];
        private int count = 0;
        private T _methodClassName;

        public RPCConnectService()
        {
            _methodClassName = new T();
        }



        public bool Run(int RpcServerPort, int Listen)
        {
            bool b = false;
            try
            {
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                IPEndPoint point = new IPEndPoint(ip, RpcServerPort);
                socketWatch.Bind(point);
                socketWatch.Listen(Listen);
                //socketWatch.Accept();
                //④开始接受客户端连接请求
                socketWatch.BeginAccept(new AsyncCallback(ClientAccepted), socketWatch);
                b = true;
            }
            catch (Exception)
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// 客户端连接成功
        /// </summary>
        /// <param name="ar"></param>
        public void ClientAccepted(IAsyncResult ar)
        {
            #region
            //设置计数器
            count++;
            var socket = ar.AsyncState as Socket;
            //这就是客户端的Socket实例，我们后续可以将其保存起来
            var client = socket.EndAccept(ar);
            //客户端IP地址和端口信息
            IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;

            //接收客户端的消息(这个和在客户端实现的方式是一样的）异步
            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
            //准备接受下一个客户端请求(异步)
            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
            #endregion
        }

        /// <summary>
        /// 接收某一个客户端的消息
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveMessage(IAsyncResult ar)
        {
            int length = 0;
            string message = "";
            var socket = ar.AsyncState as Socket;
            //客户端IP地址和端口信息
            IPEndPoint clientipe = (IPEndPoint)socket.RemoteEndPoint;
            try
            {
                #region
                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                length = socket.EndReceive(ar);
                //读取出来消息内容
                message = Encoding.UTF8.GetString(buffer, 0, length);
                message = ReflexMethod(message);
                //输出接收信息
                WriteLine(clientipe + " ：" + message, ConsoleColor.White);
                //服务器发送消息
                socket.Send(Encoding.UTF8.GetBytes(message)); //默认Unicode
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息）异步
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
                #endregion
            }
            catch (Exception ex)
            {
            }
        }
        private void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("MM-dd HH:mm:ss"), str);
        }

        private string ReflexMethod(string methon)
        {
            if (string.IsNullOrEmpty(methon)) return "";
            RpcServerContext istudy = JsonConvert.DeserializeObject<RpcServerContext>(methon);
            Type t = _methodClassName.GetType();
            //object obj = Activator.CreateInstance(t, new object[] { _connectionString });//创建一个obj对象
            MethodInfo mi = t.GetMethod(istudy.MethodName);
            var inc = mi.Invoke(_methodClassName, new object[] { istudy, null });
            return JsonConvert.SerializeObject(inc);
        }
    }
}
