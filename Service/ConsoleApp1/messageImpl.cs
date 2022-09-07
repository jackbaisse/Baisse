using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class messageImpl : Imessage
    {
        public void Abcdd(RpcServerContext context)
        {
            Istudy istudy = JsonConvert.DeserializeObject<Istudy>(context.Body);

            Ostudy ostudy = new Ostudy
            {
                msg = istudy.name
            };
            context.Return = JsonConvert.SerializeObject(ostudy);
        }

        public void Mcsgd(RpcServerContext context)
        {
            try
            {
                RpcServerContext result = new RpcServerContext();
                Ostudy ostudy = new Ostudy();
                ostudy.msg = "成功";

                context.Return = JsonConvert.SerializeObject(ostudy);
            }
            catch (Exception)
            {
                context.Return = "失败";
            }

        }
    }
}
