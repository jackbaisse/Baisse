using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.FilenamesjCommon.Input;
using Baisse.FilenamesjCommon.Output;
using Baisse.FilenamesjCommon.RPC.RPCModel;

namespace Baisse.FilenamesjCommon
{
    public interface IFilenamesjService
    {
        ResponseContent<Ostudy> Filenamesjss5(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Filenamesjss1(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Filenamesjss2(RpcServerContext context, Istudy args);
        ResponseContent<Ostudy> Filenamesjss6(RpcServerContext context, Istudy args);
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
