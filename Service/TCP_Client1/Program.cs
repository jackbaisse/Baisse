using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using Baisse.StudyCommon;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Newtonsoft.Json;

namespace TCP_Client1
{
    class Program
    {
        static byte[] buffer = new byte[1024];
        static void Main(string[] args)
        {
            try
            {

                RpcServerContext rpcServer = new RpcServerContext()
                {
                    LogId = Guid.NewGuid().ToString(),
                    MethodName = "Mcsgd",
                };
                Istudy istudy = new Istudy()
                {
                    Methon = "Istudy",
                    MethonName = "Mcsgd",
                    address = "wxfk",
                    age = "18",
                    id = "1",
                    name = "张三"
                };
                StudyClass studyClass = new StudyClass();

                //studyClass.Studyss(rpcServer, (x) =>
                //{
                //    return istudy;
                //});

                var a = studyClass.Studyss5(rpcServer, istudy);


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
            }
            catch (Exception ex)
            {
                WriteLine("client:error " + ex.Message, ConsoleColor.Red);
            }
            finally
            {
                Console.Read();
            }
        }

        public static TResult ExcuteService<TResult>() where TResult : new()
        {
            return new TResult();
        }



        public static string messagecept(string text)
        {
            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
                MethodName = "Mcsgd",
            };
            Istudy istudy = new Istudy()
            {
                Methon = "Istudy",
                MethonName = "Mcsgd",
                address = "wxfk",
                age = "18",
                id = "1",
                name = "张三"
            };

            if (!string.IsNullOrEmpty(text))
            {
                istudy.name = text;
            }

            //k.Methon = "Istudy";
            //k.MethonName = "Mcsgd";
            //k.address = "wxfk";
            //k.age = "18";
            //k.id = "1";
            //k.name = "张三";

            StudyClass studyClass = new StudyClass();

            studyClass.Studys(rpcServer, (k) =>
            {
                k.Methon = "Istudy";
                k.MethonName = "Mcsgd";
                k.address = "wxfk";
                k.age = "18";
                k.id = "1";
                k.name = "张三";
            });

            studyClass.Studyss(rpcServer, (x) =>
            {
                return istudy;
            });


            rpcServer.Requtst(RequestContent.Request(istudy, "Mcsgd"));

            return JsonConvert.SerializeObject(rpcServer);

        }

        public void aaa(Baisse.StudyCommon.Input.Istudy b)
        {

        }

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="ar"></param>
        public static void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;

                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.UTF8.GetString(buffer, 0, length);

                //显示消息
                WriteLine(message, ConsoleColor.White);

                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message, ConsoleColor.Red);
            }
        }

        public static void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("MM-dd HH:mm:ss"), str);
        }
    }
}
