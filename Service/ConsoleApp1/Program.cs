using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static byte[] buffer = new byte[1024];
        private static int count = 0;
        private static Imessage _message;
        static void Main(string[] args)
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
            rpcServer.Requtst(RequestContent.Request(istudy));

            string ss = JsonConvert.SerializeObject(rpcServer);

            var cc = JsonConvert.DeserializeObject<RpcServerContext>(ss);
        }
    }
}
