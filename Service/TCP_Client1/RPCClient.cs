using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon.common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TCP_Client1
{
    public class RPCClient
    {
        public byte[] buffer = new byte[1024];
        public StringBuilder str = new StringBuilder();

        /// <summary>
        /// 同步连接
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ConnectService(string message)
        {
            string result = string.Empty;
            //①创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {

                //②连接到指定服务器的指定端口
                socket.Connect("127.0.0.1", 5959); //localhost代表本机
                Console.WriteLine("client:connect to server success!", ConsoleColor.White);
                var outputBuffer = Encoding.UTF8.GetBytes(message);
                //发送消息
                socket.Send(outputBuffer);

                int length = socket.Receive(buffer);  // length 接收字节数组长度

                result = Encoding.UTF8.GetString(buffer, 0, length);

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                socket.Close();
            }
            return result;
        }

        public async Task<string> ConnectServiceasync(string message)
        {
            string result = string.Empty;
            //①创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //②连接到指定服务器的指定端口
                socket.Connect("127.0.0.1", 5959); //localhost代表本机
                Console.WriteLine("client:connect to server success!", ConsoleColor.White);
                var outputBuffer = Encoding.UTF8.GetBytes(message);
                //发送消息
                socket.Send(outputBuffer);
                int length = socket.Receive(buffer);  // length 接收字节数组长度
                result = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                socket.Close();
            }
            return await Task.FromResult(result);
        }

        public async Task ConnectServiceAsync(string message)
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


                ////①创建一个Socket
                //var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                ////②连接到指定服务器的指定端口
                //socket.Connect("127.0.0.1", 5959); //localhost代表本机

                //WriteLine("client:connect to server success!", ConsoleColor.White);

                ////③实现异步接受消息的方法 客户端不断监听消息
                //socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

                ////④接受用户输入，将消息发送给服务器端
                //while (true)
                //{
                //    var message = Console.ReadLine();

                //    message = messagecept(message);
                //    var outputBuffer = Encoding.UTF8.GetBytes(message);
                //    socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                //}


                var outputBuffer = Encoding.UTF8.GetBytes(message);

                socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                socket.Close();
            }
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
                str.Append(message);
                Console.WriteLine(message);
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
