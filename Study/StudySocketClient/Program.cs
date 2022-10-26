using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace StudySocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPAddress iP = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iP, 8099);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Connect(iPEndPoint);
            while (true)
            {
                string str = Console.ReadLine();
                byte[] a = Encoding.UTF8.GetBytes(str);
                int send = serverSocket.Send(a, 0);
            }
        }
    }
}
