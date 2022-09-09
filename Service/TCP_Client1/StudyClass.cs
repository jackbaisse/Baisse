using System;
using System.Collections.Generic;
using System.Text;
using Baisse.StudyCommon;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Newtonsoft.Json;

namespace TCP_Client1
{
    public class StudyClass : IStudyService
    {
        public void Abcdd(Baisse.StudyCommon.common.RpcServerContext context)
        {
            throw new NotImplementedException();
        }

        public void Mcsgd(Baisse.StudyCommon.common.RpcServerContext context)
        {
            throw new NotImplementedException();
        }

        public void Studys(RpcServerContext context, Action<Baisse.StudyCommon.Input.Istudy> action)
        {
            action.Invoke(new Istudy());
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string json = JsonConvert.SerializeObject(context);
        }

        public void Studyss(RpcServerContext context, Func<RpcServerContext, Baisse.StudyCommon.Input.Istudy> func = null)
        {
            //RPCClient rpc = new RPCClient();

            //var a = func.Invoke(new RpcServerContext());
            //context.RequestData = JsonConvert.SerializeObject(a);
            //context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //string data = JsonConvert.SerializeObject(context);

            //func = (x) =>
            //{
            //    context.ResponseData = rpc.ConnectService(data).ResponseData;

            //    RequestContent<Ostudy> ostudy = JsonConvert.DeserializeObject<RequestContent<Ostudy>>(context.ResponseData);

            //    return null;
            //};

            //var a1 = func.Invoke(context);

        }

        public void Inveok<T>()
        {

        }

        public void Studyss1(RpcServerContext context, Func<Istudy, RpcServerContext> func = null)
        {
            throw new NotImplementedException();
        }

        public Ostudy Studyss2(RpcServerContext context, Func<Istudy, RpcServerContext> func = null)
        {
            throw new NotImplementedException();
        }

        public Ostudy Studyss3(RpcServerContext context, Istudy istudy = null)
        {
            throw new NotImplementedException();
        }

        public Ostudy Studyss3(RpcServerContext context, Action<Istudy> action)
        {
            throw new NotImplementedException();
        }

        public ResponseContent<Ostudy> Studyss4(RpcServerContext context, Action<Istudy> action)
        {
            throw new NotImplementedException();
        }

        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy istudy)
        {
            RPCClient rpc = new RPCClient();
            context.RequestData = JsonConvert.SerializeObject(istudy);
            context.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string data = JsonConvert.SerializeObject(context);
            string result = rpc.ConnectService(data);
            ResponseContent<Ostudy> ostudy = JsonConvert.DeserializeObject<ResponseContent<Ostudy>>(result);
            return ostudy;
        }
    }
}
