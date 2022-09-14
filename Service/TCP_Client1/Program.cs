using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
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

                var a = studyClass.Studyss5(rpcServer, istudy);
                var a1 = studyClass.Studyss1(rpcServer, istudy);
                var a2 = studyClass.Studyss2(rpcServer, istudy);

                //var b = studyClass.Studyss6(rpcServer, istudy);

                //while (!b.IsCompleted)
                //{
                //    Thread.Sleep(1000);
                //}

                //for (int i = 0; i < 10; i++)
                //{
                //    var a1 = studyClass.Studyss6(rpcServer, istudy);
                //    if (a1 == null)
                //    {

                //    }
                //    else
                //    {

                //    }
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
