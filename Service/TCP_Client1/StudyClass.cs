using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Newtonsoft.Json;

namespace TCP_Client1
{
    public class StudyClass : IStudyService
    {
        private RPCConnectClient RPCConnect = new RPCConnectClient("127.0.0.1", 5959);
        public ResponseContent<Ostudy> Studyss1(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.ConnectService<RpcServerContext, ResponseContent<Ostudy>>(context);
        }

        public ResponseContent<Ostudy> Studyss2(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.ConnectService<RpcServerContext, ResponseContent<Ostudy>>(context);
        }

        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.ConnectService<RpcServerContext, ResponseContent<Ostudy>>(context);
        }

        public async Task<ResponseContent<Ostudy>> Studyss6(RpcServerContext context, Istudy args)
        {
            RPCClient rpc = new RPCClient();
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string data = JsonConvert.SerializeObject(context);
            await rpc.ConnectServiceAsync(data);
            ResponseContent<Ostudy> ostudy = JsonConvert.DeserializeObject<ResponseContent<Ostudy>>("");
            return ostudy;
        }
    }
}
