using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Baisse.StudyCommon.RPC.RPCModel;
using Newtonsoft.Json;

namespace TCP_Client1
{
    public class StudyClass : IStudyService
    {
        private RPCClient RPCConnect = new RPCClient("127.0.0.1", 5959);

        public ResponseContent<OFile> FileUpload(RpcServerContext context, IFile args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OFile>>(context);
        }

        public ResponseContent<Ostudy> Studyss1(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<Ostudy>>(context);
        }

        public ResponseContent<Ostudy> Studyss2(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<Ostudy>>(context);
        }

        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<Ostudy>>(context);
        }


        public ResponseContent<Ostudy> Studyss6(RpcServerContext context, Istudy args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<Ostudy>>(context);
        }
    }
}
