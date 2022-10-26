using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Model.Models.RPCModel;
using Baisse.Study.BLL;
using Baisse.Study.Common;
using Baisse.StudyCommon;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Microsoft.Extensions.Logging;

namespace Baisse.Study
{
    public class StudyServiceImpl : IStudyService
    {
        public ILogger _logger { get; set; }
        public StudyServiceImpl()
        {
            _logger = ServiceHelp.GetInstance<StudyService>();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OFileUpload> FileUpload(RpcServerContext context, IFileUpload args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<IFileUpload>();

                BLL.StudyInfoBLL.FileUpload(args);

                OFileUpload ostudy = new OFileUpload
                {
                    FileID = args.FileID
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OFileUpload>(e);
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OFileDownload> FileDownload(RpcServerContext context, IFileDownload args)
        {
            try
            {
                _logger.LogInformation("FileDownload");
                args = context.GetArgs<IFileDownload>();

                var result = BLL.StudyInfoBLL.FileDownload(args);

                return ResponseContent.Success(result);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OFileDownload>(e.Message);
            }
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OSeeFile> SeeFile(RpcServerContext context, ISeeFile args)
        {
            try
            {
                _logger.LogInformation("SeeFile");
                args = context.GetArgs<ISeeFile>();
                var result = BLL.StudyInfoBLL.SeeFile(args);

                return ResponseContent.Success(result);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OSeeFile>(e.Message);
            }
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OUpdateFile> UpdateFile(RpcServerContext context, IUpdateFile args)
        {
            try
            {
                _logger.LogInformation("UpdateFile");
                args = context.GetArgs<IUpdateFile>();
                BLL.StudyInfoBLL.UpdateFile(args);
                return ResponseContent.Success(new OUpdateFile());
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OUpdateFile>(e.Message);
            }
        }

        /// <summary>
        /// 获取服务配置信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OSeeServicesInfo> SeeServicesInfo(RpcServerContext context, ISeeServicesInfo args)
        {
            try
            {
                _logger.LogInformation("SeeServicesInfo");
                args = context.GetArgs<ISeeServicesInfo>();

                OSeeServicesInfo result = new OSeeServicesInfo();

                var appSettings = ServiceHelp.AppSettings();
                result.serviceList = appSettings.ServiceSettings;
                return ResponseContent.Success(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseContent.Fail<OSeeServicesInfo>(e.Message);
            }
        }


        /// <summary>
        /// 启动服务或停止服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OStartOrStopWindowsService> StartOrStopWindowsService(RpcServerContext context, IStartOrStopWindowsService args)
        {
            try
            {
                _logger.LogInformation("SeeServicesInfo");
                args = context.GetArgs<IStartOrStopWindowsService>();

                string servicename = args.serviceConfig.ServiceName;//服务名称

                switch (args.servicestatus)
                {
                    case ServiceControllerStatus.Stop:
                        StudyService.StopWindowsService(servicename);
                        break;
                    case ServiceControllerStatus.Start:
                        StudyService.StartWindowsService(servicename);
                        break;
                    default:
                        return ResponseContent.Fail<OStartOrStopWindowsService>("未找到服务名");
                }
                return ResponseContent.Success(new OStartOrStopWindowsService { Msg = "成功" });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseContent.Fail<OStartOrStopWindowsService>(e.Message);
            }

            return ResponseContent.Fail<OStartOrStopWindowsService>("失败");
        }
    }
}
