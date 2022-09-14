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
        ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Studyss1(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Studyss2(RpcServerContext context, Istudy args);
        Task<ResponseContent<Ostudy>> Studyss6(RpcServerContext context, Istudy args);
    }
}
