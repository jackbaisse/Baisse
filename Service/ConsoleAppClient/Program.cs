using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleAppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            string portS = "5959";
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(portS))
            {
                Console.WriteLine("请输入端口和Ip", "提示");
                return;
            }
            else
            {
                SocketError socketError = Request.Connect(ip, int.Parse(portS));
                if (socketError == SocketError.Success)
                {

                    Console.WriteLine("已连接到主机 \r\n");
                    Request.OnReceiveData += Request_OnReceiveData;
                    Request.OnServerClosed += Request_OnServerClosed;
                    Request.StartHeartbeat();
                }
            }

            string a = "{\"LogId\":\"77ef2e5b-615b-4780-b5db-2f6e13ac1d0d\",\"MethodName\":\"Studyss5\",\"jwtToken\":null,\"RequestData\":\"{\\\"id\\\":\\\"1\\\",\\\"name\\\":\\\"张三\\\",\\\"age\\\":\\\"18\\\",\\\"address\\\":\\\"wxfk\\\",\\\"Methon\\\":\\\"Istudy\\\",\\\"MethonName\\\":\\\"Mcsgd\\\"}\",\"ResponseData\":null}";


            string result = string.Empty;

            var aa = Send(a);

            //while (true)
            //{
            //    var a = Console.ReadLine();
            //    Send(a);
            //}

        }

        public delegate SocketError del();
        /// <summary>
        /// server 断开
        /// </summary>
        private static void Request_OnServerClosed()
        {
            Console.WriteLine("server 已断开" + "\r\n");
            Request.Disconnect();

            //SocketError socketError= Request.TryConnect();
            //if (socketError == SocketError.Success) 
            //{
            //    this.BeginInvoke(new MessageHandle(UpdateRece), "已再次连接到server " + "\r\n");
            //}

            del del3 = new del(Request.TryConnect);

            IAsyncResult iar2 = del3.BeginInvoke(Connect2Server, del3);

        }

        private static void Connect2Server(IAsyncResult ar)
        {

            Console.WriteLine("已连接服务器");

        }
        public delegate void MessageHandle(string msg);
        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="message"></param>
        private static void Request_OnReceiveData(byte[] message)
        {
            string msg = Encoding.UTF8.GetString(message);
            Console.WriteLine("request:" + msg);
            _action.Invoke(msg);
            autoConnectEvent.Set(); //释放阻塞.  
            //this.BeginInvoke(new MessageHandle(UpdateRece), msg);
        }

        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="message"></param>
        //private static void Request_OnReceiveData(byte[] message)
        //{
        //    string msg = Encoding.UTF8.GetString(message);
        //    Console.WriteLine("request:" + msg);
        //    //this.BeginInvoke(new MessageHandle(UpdateRece), msg);
        //}



        //private static void Send(string message)
        //{

        //    if (string.IsNullOrWhiteSpace(message))
        //    {
        //        Console.WriteLine("请输入信息");
        //        return;
        //    }
        //    else
        //    {
        //        bool isSent = Request.Send(message);
        //        if (isSent)
        //        {
        //            Console.WriteLine("send:" + message + "\r\n");
        //        }

        //    }

        //}

        static Action<string> _action;

        private static AutoResetEvent autoConnectEvent = new AutoResetEvent(false);

        private static string Send(string message)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("请输入信息");
                return result;
            }
            else
            {
                bool isSent = Request.Send(message);
                if (isSent)
                {
                    _action = (x) =>
                    {
                        result = x;
                    };

                    autoConnectEvent.WaitOne();


                    //while (string.IsNullOrEmpty(result))
                    //{
                    //    Thread.Sleep(300);
                    //}

                    Console.WriteLine("send:" + message + "\r\n");
                }
            }
            return result;
        }
    }
}
