using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Baisse.StudyCommon.RPC.RpcServer
{
    public class Lister
    {
        private Server m_socket;

        /// <summary>
        /// 最大连接数
        /// </summary>
        /// <param name="numConnections">最大连接数</param>
        /// <param name="receiveBufferSize">缓冲区大小</param>
        public Lister(int numConnections, int receiveBufferSize)
        {
            m_socket = new Server(numConnections, receiveBufferSize);
            m_socket.Init();
            //m_socket.ServerStopedEvent += m_socket_ServerStopedEvent;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">端口号</param>
        public bool StartLister(int port)
        {
            try
            {
                bool isStart = m_socket.Start(new IPEndPoint(IPAddress.Any, port));
                if (isStart)
                {
                    m_socket.ClientNumberChange += m_socket_ClientNumberChange;
                    m_socket.ReceiveClientData += m_socket_ReceiveClientData;
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">端口号</param>
        public void StopLister()
        {
            m_socket.Stop();
        }

        /// <summary>
        /// server停止
        /// </summary>
        void m_socket_ServerStopedEvent()
        {

        }
        public delegate void MessageHandle(string msg);
        /// <summary>
        /// 收到来自client的消息
        /// </summary>
        /// <param name="socketAsyncEventarfs"></param>
        /// <param name="token"></param>
        /// <param name="buff"></param>
        void m_socket_ReceiveClientData(AsyncUserToken token, byte[] buff)
        {
            // Console.WriteLine("收到client来的数据");
            string msg = Encoding.UTF8.GetString(buff);

            ///业务逻辑
            //var result = GetMethodInvoke(msg);

            //   Console.WriteLine("Rece:{0}", msg);
            //this.BeginInvoke(new MessageHandle(UpdateRece), msg);
            ///发送消息
            byte[] message = Encoding.UTF8.GetBytes("success");
            bool issent = m_socket.SendMessage(token, message);
            //发送失败，是否重试
            if (!issent)
            {
                //this.BeginInvoke(new MessageHandle(UpdateSend), "success");
            }
            //转发到另外一个client上,如果收到心跳包null,则不转发，直接server回复
            if (m_socket.ClientList.Count > 1)
            {
                AsyncUserToken ntoken = m_socket.ClientList.Find(s => s.IPAddress != token.IPAddress);
                if (ntoken != null)
                {
                    m_socket.SendMessage(ntoken, buff);
                }
            }
        }

        //public string GetMethodInvoke(string msg)
        //{
        //    if (string.IsNullOrEmpty(msg)) return msg;
        //    RpcServerContext rpcServerContext = JsonConvert.DeserializeObject<RpcServerContext>(msg);
        //    Type t = _methodName.GetType();
        //    //object obj = Activator.CreateInstance(t, new object[] { _connectionString });//创建一个obj对象
        //    MethodInfo mi = t.GetMethod(rpcServerContext.MethodName);
        //    var inc = mi.Invoke(_methodName, new object[] { rpcServerContext, null });
        //    return JsonConvert.SerializeObject(inc);
        //}

        /// <summary>
        /// client 连接或断开时
        /// </summary>
        /// <param name="num"></param>
        /// <param name="token"></param>
        void m_socket_ClientNumberChange(int num, AsyncUserToken token)
        {
            if (num > 0)
            {
                string msg = string.Format("{0},{1}已连接", num, token.Remote.ToString());
            }
            if (num < 0)
            {
                string msg = string.Format("{0},{1}断开连接", num, token.Remote.ToString());
            }

        }


        private void StopList()
        {
            this.m_socket.Stop();
        }

        private void SendList()
        {
            if (m_socket.ClientList.Count > 0)
            {
                AsyncUserToken token = null;
                // 查找要发送的client的socket对象。
                token = m_socket.ClientList.Find(x => x.IPAddress.ToString() == "127.0.0.1");
                string message = "1111";
                if (string.IsNullOrWhiteSpace(message))
                {

                    //MessageBox.Show("请输入要发送的内容", "提示", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    if (token != null)
                    {
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        bool issent = m_socket.SendMessage(token, messageBuffer);
                        if (issent)
                        {
                            //this.textBoxSend.AppendText("Send: " + message + "\r\n");
                        }
                    }
                }


            }
            else
            {
                //MessageBox.Show("未监听到连接，无法发送", "提示", MessageBoxButtons.OK);
                return;
            }
        }


    }
}
