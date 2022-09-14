using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Baisse.StudyCommon.common
{
    public class RPCConnectClient
    {
        byte[] buffer = new byte[1024];
        private int count = 0;
        private readonly string _ip;
        private readonly int _prot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot">端口</param>
        public RPCConnectClient(string ip, int prot)
        {
            _ip = ip;
            _prot = prot;
        }

        public string ConnectService(string message)
        {
            string result = string.Empty;
            //①创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //②连接到指定服务器的指定端口
                socket.Connect(_ip, _prot); //localhost代表本机
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">入参</typeparam>
        /// <typeparam name="F">反参</typeparam>
        /// <param name="func"></param>
        public F ConnectService<T, F>(T args)
        {
            string data = JsonConvert.SerializeObject(args);
            string result = string.Empty;
            //①创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //②连接到指定服务器的指定端口
                socket.Connect(_ip, _prot); //localhost代表本机
                Console.WriteLine("client:connect to server success!", ConsoleColor.White);
                var outputBuffer = Encoding.UTF8.GetBytes(data);
                //发送消息
                socket.Send(outputBuffer);

                int length = socket.Receive(buffer);  // length 接收字节数组长度
                result = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(result, ConsoleColor.White);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            return JsonConvert.DeserializeObject<F>(result);
        }
    }
}
