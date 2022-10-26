using System;
using System.Text;
using Baisse.Model.Models.RPCModel;
using Baisse.StudyCommon;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Newtonsoft.Json;

namespace TCP_Client1
{
    public class StudyClass : IStudyService
    {
        private RPCClient RPCConnect = new RPCClient("127.0.0.1", 5959);
        public ResponseContent<OFileDownload> FileDownload(RpcServerContext context, IFileDownload args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OFileDownload>>(context);
        }

        public ResponseContent<OFileUpload> FileUpload(RpcServerContext context, IFileUpload args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OFileUpload>>(context);
        }

        public ResponseContent<OSeeFile> SeeFile(RpcServerContext context, ISeeFile args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OSeeFile>>(context);
        }

        public ResponseContent<OSeeServicesInfo> SeeServicesInfo(RpcServerContext context, ISeeServicesInfo args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OSeeServicesInfo>>(context);
        }

        public ResponseContent<OStartOrStopWindowsService> StartOrStopWindowsService(RpcServerContext context, IStartOrStopWindowsService args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OStartOrStopWindowsService>>(context);
        }

        public ResponseContent<OUpdateFile> UpdateFile(RpcServerContext context, IUpdateFile args)
        {
            context.RequestData = JsonConvert.SerializeObject(args);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return RPCConnect.Send<RpcServerContext, ResponseContent<OUpdateFile>>(context);
        }
    }
}
