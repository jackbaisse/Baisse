using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Model.Models.RPCModel;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;

namespace Baisse.StudyCommon
{
    public interface IStudyService
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OFileUpload> FileUpload(RpcServerContext context, IFileUpload args);
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OFileDownload> FileDownload(RpcServerContext context, IFileDownload args);

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OSeeFile> SeeFile(RpcServerContext context, ISeeFile args);
        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OUpdateFile> UpdateFile(RpcServerContext context, IUpdateFile args);

        /// <summary>
        /// 获取服务信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OSeeServicesInfo> SeeServicesInfo(RpcServerContext context, ISeeServicesInfo args);
        /// <summary>
        /// 启动或停止服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ResponseContent<OStartOrStopWindowsService> StartOrStopWindowsService(RpcServerContext context, IStartOrStopWindowsService args);

    }
}
