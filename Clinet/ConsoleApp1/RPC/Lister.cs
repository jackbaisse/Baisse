using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Lister
    {
        private Server m_socket;

        public Lister()
        {
            m_socket = new Server(300, 1024);
            m_socket.Init();
            StartLister();
            m_socket.ServerStopedEvent += m_socket_ServerStopedEvent;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        private void StartLister()
        {
            string portStr = "5959";
            if (string.IsNullOrEmpty(portStr))
            {
                return;
            }
            else
            {

                int port = int.Parse(portStr);
                bool isStart = m_socket.Start(new IPEndPoint(IPAddress.Any, port));
                if (isStart)
                {
                    m_socket.ClientNumberChange += m_socket_ClientNumberChange;
                    m_socket.ReceiveClientData += m_socket_ReceiveClientData;
                }
            }
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
            //   Console.WriteLine("Rece:{0}", msg);
            //this.BeginInvoke(new MessageHandle(UpdateRece), msg);
            ///发送消息
            byte[] message = Encoding.UTF8.GetBytes("success");
            bool issent = m_socket.SendMessage(token, message);
            if (issent)
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
