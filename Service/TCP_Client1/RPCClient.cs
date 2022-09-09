using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Baisse.StudyCommon.common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TCP_Client1
{
    public class RPCClient
    {
        static byte[] buffer = new byte[1024];
        List<string> list = new List<string>();
        public string ConnectService(string message)
        {
            string result = string.Empty;
            //①创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //②连接到指定服务器的指定端口
                socket.Connect("127.0.0.1", 5959); //localhost代表本机

                //③实现异步接受消息的方法 客户端不断监听消息
                //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

                //④接受用户输入，将消息发送给服务器端

                var outputBuffer = Encoding.UTF8.GetBytes(message);

                socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

                if (list != null && list.Count > 1)
                {
                    result = list[1];
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
            return result;
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;

                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.UTF8.GetString(buffer, 0, length);

                if (message != null)
                {
                    list.Add(message);
                }

                //显示消息

                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
