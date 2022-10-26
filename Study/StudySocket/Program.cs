using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace StudySocket
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化套接字类型
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //创建终结点（EndPoint）
            IPAddress iP = IPAddress.Parse("127.0.0.1");

            IPEndPoint endPoint = new IPEndPoint(iP, 8099);

            //用于绑定 IPEndPoint 对象
            socket.Bind(endPoint);

            //监听
            socket.Listen(10);

            //接收连接
            Socket temp = socket.Accept();

            byte[] buff = new byte[1024];
            //接收消息

            int i = 0;
            string recvStr = "";
            i = temp.Receive(buff, buff.Length, 0);
            recvStr = Encoding.UTF8.GetString(buff, 0, i);

            Console.WriteLine(recvStr);
            Console.ReadKey();

            //string host;
            //int port = 80;

            //if (args.Length == 0)
            //    // If no server name is passed as argument to this program,
            //    // use the current host name as the default.
            //    //服务器名称
            //    host = Dns.GetHostName();
            //else
            //    host = args[0];

            //string result = SocketSendReceive(host, port);
            //Console.WriteLine(result);
        }

        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

        // This method requests the home page content for the specified server.
        private static string SocketSendReceive(string server, int port)
        {
            string request = "GET / HTTP/1.1\r\nHost: " + server +
                "\r\nConnection: Close\r\n\r\n";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];
            string page = "";

            // Create a socket connection with the specified server and port.
            using (Socket s = ConnectSocket(server, port))
            {

                if (s == null)
                    return ("Connection failed");

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;
                //page = "Default HTML page on " + server + ":\r\n";

                // The following will block until the page is transmitted.
                do
                {
                    //
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);

                    page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                }
                while (bytes > 0);
            }

            return page;
        }
    }
}
