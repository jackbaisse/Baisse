using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Baisse.StudyCommon.RPC.RpcClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Server.Rpc
{
    public class RPCClient
    {
        public RPCClient(string ip, int prot)
        {
            SocketError socketError = Request.Connect(ip, prot);
            if (socketError == SocketError.Success)
            {
                Console.WriteLine("已连接到主机 \r\n");
                Request.OnReceiveData += Request_OnReceiveData;
                Request.OnServerClosed += Request_OnServerClosed;
                //Request.StartHeartbeat();//心跳检测
            }
        }

        public delegate SocketError del();
        /// <summary>
        /// server 断开
        /// </summary>
        private void Request_OnServerClosed()
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

        private void Connect2Server(IAsyncResult ar)
        {

            Console.WriteLine("已连接服务器");

        }
        public delegate void MessageHandle(string msg);
        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="message"></param>
        private void Request_OnReceiveData(byte[] message)
        {
            string msg = Encoding.UTF8.GetString(message);
            Console.WriteLine("request:" + msg);
            _action.Invoke(msg);
            autoConnectEvent.Set(); //释放阻塞.  
            //this.BeginInvoke(new MessageHandle(UpdateRece), msg);
        }

        static Action<string> _action;

        private AutoResetEvent autoConnectEvent = new AutoResetEvent(false);

        private string Send(string message)
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

                    Console.WriteLine("send:" + message + "\r\n");
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">入参</typeparam>
        /// <typeparam name="F">反参</typeparam>
        /// <param name="args"></param>
        public F Send<T, F>(T args)
        {
            try
            {
                string data = JsonConvert.SerializeObject(args);
                string result = string.Empty;
                bool isSent = Request.Send(data);
                if (isSent)
                {
                    _action = (x) =>
                    {
                        result = x;
                    };
                    autoConnectEvent.WaitOne();
                }
                return JsonConvert.DeserializeObject<F>(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
