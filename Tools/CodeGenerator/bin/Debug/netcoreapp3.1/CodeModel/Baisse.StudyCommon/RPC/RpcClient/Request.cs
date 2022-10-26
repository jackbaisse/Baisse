using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baisse.StudyCommon.RPC.RpcClient
{
    public class Request
    {
        /// <summary>  
        /// 标识，只是一个编号而已  
        /// </summary>  
        public int ArgsTag { get; set; }

        /// <summary>  
        /// 设置/获取使用状态  
        /// </summary>  
        public bool IsUsing { get; set; }

        //定义,最好定义成静态的, 因为我们只需要一个就好  
        static Client smanager = null;


        //定义事件与委托  
        //public delegate void ReceiveData(byte[] message);
        public delegate void ReceiveData(byte[] message);
        public delegate void ServerClosed();
        public static event ReceiveData OnReceiveData;
        public static event ServerClosed OnServerClosed;


        /// <summary>  
        /// 心跳定时器  
        /// </summary>  
        static System.Timers.Timer heartTimer = null;


        /// <summary>  
        /// 判断是否已连接  
        /// </summary>  
        public static bool Connected
        {
            get { return smanager != null && smanager.Connected; }
        }


        /// <summary>  
        /// 连接到服务器  
        /// </summary>  
        /// <returns></returns>  
        public static SocketError Connect(string ip, int port)
        {
            if (Connected) return SocketError.Success;
            //string ip = "192.168.1.158";
            //int port = 50000;
            if (string.IsNullOrWhiteSpace(ip) || port <= 1000) return SocketError.Fault;
            //创建连接对象, 连接到服务器  
            smanager = new Client(ip, port);
            // SocketError error = smanager.Connect();
            SocketError socketError = TryConnect();

            if (socketError == SocketError.Success)
            {
                //连接成功后,就注册事件. 最好在成功后再注册.  
                smanager.ServerDataHandler += OnReceivedServerData;
                smanager.ServerStopEvent += OnServerStopEvent;

            }
            return socketError;
        }

        /// <summary>  
        /// 断开连接  
        /// </summary>  
        public static void Disconnect()
        {
            try
            {
                smanager.Disconnect();
                if (heartTimer != null)
                    heartTimer = null;
            }
            catch (Exception)
            {
                Console.WriteLine("未能关闭socket连接");
            }
        }

        /// <summary>
        /// 尝试连接server，成功则返回true
        /// </summary>
        /// <returns></returns>
        public static SocketError TryConnect()
        {
            SocketError socketError = SocketError.ConnectionRefused;
            try
            {
                do
                {
                    socketError = smanager.Connect();
                    if (socketError == SocketError.Success)
                    {
                        //触发事件
                        break;
                    }
                } while (socketError != SocketError.Success);
            }
            catch (Exception)
            {


            }
            return socketError;

        }
        /// <summary>  
        /// 发送消息  
        /// </summary>  
        /// <param name="message">消息实体</param>  
        /// <returns>True.已发送; False.未发送</returns>  
        public static bool Send(string message)
        {
            if (!Connected) return false;

            byte[] buff = Encoding.UTF8.GetBytes(message);
            //加密,根据自己的需要可以考虑把消息加密  
            //buff = AESEncrypt.Encrypt(buff, m_aesKey);  
            smanager.Send(buff);

            Console.WriteLine("send:" + message + "\r\n");



            return true;
        }

        /// <summary>  
        /// 发送消息  
        /// </summary>  
        /// <param name="message">消息实体</param>  
        /// <returns>True.已发送; False.未发送</returns>  
        public static bool AsyncSend(string message)
        {
            if (!Connected) return false;

            byte[] buff = Encoding.UTF8.GetBytes(message);
            //加密,根据自己的需要可以考虑把消息加密  
            //buff = AESEncrypt.Encrypt(buff, m_aesKey);  
            smanager.Send(buff);
            return true;
        }


        /// <summary>  
        /// 发送字节流  
        /// </summary>  
        /// <param name="buff"></param>  
        /// <returns></returns>  
        static bool Send(byte[] buff)
        {
            if (!Connected) return false;
            smanager.Send(buff);
            return true;
        }



        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="buff"></param>  
        private static void OnReceivedServerData(byte[] buff)
        {
            //To do something  
            //你要处理的代码,可以实现把buff转化成你具体的对象, 再传给前台  
            if (OnReceiveData != null)
                OnReceiveData(buff);
        }



        /// <summary>  
        /// 服务器已断开  
        /// </summary>  
        private static void OnServerStopEvent()
        {
            if (OnServerClosed != null)
                OnServerClosed();
        }


        public static void StartHeartbeat()
        {
            if (heartTimer == null)
            {
                heartTimer = new System.Timers.Timer();
                heartTimer.Elapsed += TimeElapsed;
            }
            heartTimer.AutoReset = true;     //循环执行  
            heartTimer.Interval = 30 * 1000; //每30秒执行一次  
            heartTimer.Enabled = true;
            heartTimer.Start();

            //初始化心跳包消息  
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TimeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            Request.Send("null");
        }
    }

}
