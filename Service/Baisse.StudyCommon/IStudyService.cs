using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;

namespace Baisse.StudyCommon
{
    public interface IStudyService
    {

        void Mcsgd(RpcServerContext context);
        void Abcdd(RpcServerContext context);
        void Studys(RpcServerContext context, Action<Istudy> action = null);
        void Studyss(RpcServerContext context, Func<RpcServerContext, Istudy> func = null);
        void Studyss1(RpcServerContext context, Func<Istudy, RpcServerContext> func = null);
        Ostudy Studyss2(RpcServerContext context, Func<Istudy, RpcServerContext> func = null);
        Ostudy Studyss3(RpcServerContext context, Istudy istudy = null);
        Ostudy Studyss3(RpcServerContext context, Action<Istudy> action);
        ResponseContent<Ostudy> Studyss4(RpcServerContext context, Action<Istudy> action);
        ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy istudy);
    }
}
