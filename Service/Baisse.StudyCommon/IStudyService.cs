using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Baisse.StudyCommon.RPC.RPCModel;

namespace Baisse.StudyCommon
{
    public interface IStudyService
    {
        ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Studyss1(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Studyss2(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Studyss6(RpcServerContext context, Istudy args);
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OFile> FileUpload(RpcServerContext context, IFile args);
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OFile> FileDownload(RpcServerContext context, IFile args);

    }
}
