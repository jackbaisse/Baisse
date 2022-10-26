using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Server.Repository;
using Baisse.Model.Models.ApiModel;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baisse.Model.Models.RPCModel;
using Baisse.Model.Models.AppsettingModel;

namespace Api.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
            // _studyClient = studyClient;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<OFileUpload> FileUpload(ApiRequest input)
        {
            try
            {
                string filepath = @"C:\Users\jackbaisse\Desktop\aa";
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";//文件名
                string zipPath = AppDomain.CurrentDomain.BaseDirectory + "FileUpload\\" + filename;
                //压缩
                Baisse.Common.CompressHelp.CompressionFile(filepath, zipPath);

                RpcServerContext rpcServer = new RpcServerContext()
                {
                    LogId = Guid.NewGuid().ToString(),
                };

                StudyService studyClass = new StudyService();

                //获取配置服务信息
                var y = studyClass.SeeServicesInfo(rpcServer, new ISeeServicesInfo());
                if (!y.Success) return ApiResponse.Fail<OFileUpload>("获取SeeServicesInfo服务失败");
                //服务信息
                var servicesinfo = y.Data;
                var serviceConfig = servicesinfo.serviceList.FirstOrDefault(x => x.ServiceName == "Baisse.BaseDataService");
                if (string.IsNullOrEmpty(serviceConfig.ServiceName) && string.IsNullOrEmpty(serviceConfig.Address)) return ApiResponse.Fail<OFileUpload>("请配置服务信息内容");

                #region 文件分块传输
                int length = 0;
                byte[] buff = new byte[1024 * 1024];
                int i = 0;
                using (FileStream fileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fileStream))
                    {
                        while ((length = fileStream.Read(buff, 0, buff.Length)) > 0)
                        {
                            IFileUpload file = new IFileUpload
                            {
                                FileID = Baisse.Common.Utils.GenerateMD5(filename),
                                FileContent = buff,
                                FileName = filename,
                                FileLength = length.ToString(),
                                FileSerialNo = i.ToString(),
                                serviceConfig = new ServiceConfig
                                {
                                    Address = serviceConfig.Address,
                                    ServiceName = serviceConfig.ServiceName
                                }
                            };
                            var a3 = studyClass.FileUpload(rpcServer, file);
                            i++;
                        }
                    }
                }
                #endregion

            }
            catch (Exception e)
            {
                return ApiResponse.Fail<OFileUpload>(e);
            }
            return ApiResponse.Success(new OFileUpload() { Content = null, FileID = "" });
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<OFileDownload> FileDownload(ApiRequest input)
        {
            IFileDownload filey = new IFileDownload
            {
                FileID = "0934_222",
                FileName = "20221025175230.zip",
                //FileName = Path.GetFileName(filepath),
            };

            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
            };

            StudyService studyClass = new StudyService();

            var y = studyClass.FileDownload(rpcServer, filey);

            if (y.Success)
            {
                var oFile = y.Data;
                string pathName = AppDomain.CurrentDomain.BaseDirectory + "FileDownload\\" + filey.FileName;

                using (FileStream stream = new FileStream(pathName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    stream.Write(oFile.Content);
                }

                return ApiResponse.Success(oFile);
            }
            return ApiResponse.Fail<OFileDownload>("失败");
        }


        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<OUpdateFile> UpdateFile(ApiRequest input)
        {
            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
            };

            StudyService studyClass = new StudyService();

            //获取配置服务信息
            var y = studyClass.SeeServicesInfo(rpcServer, new ISeeServicesInfo());
            if (!y.Success) return ApiResponse.Fail<OUpdateFile>("获取SeeServicesInfo服务失败");
            //服务信息
            var servicesinfo = y.Data;
            var serviceConfig = servicesinfo.serviceList.FirstOrDefault(x => x.ServiceName == "Baisse.BaseDataService");
            if (string.IsNullOrEmpty(serviceConfig.ServiceName) && string.IsNullOrEmpty(serviceConfig.Address)) return ApiResponse.Fail<OUpdateFile>("请配置服务信息内容");

            //查询更新包
            ISeeFile seeFile = new ISeeFile
            {
                serviceConfig = new ServiceConfig
                {
                    Address = serviceConfig.Address,
                    ServiceName = serviceConfig.ServiceName
                }
            };
            var y1 = studyClass.SeeFile(rpcServer, seeFile);
            if (!y1.Success) return ApiResponse.Fail<OUpdateFile>("获取SeeFile服务失败");
            var seefileResult = y1.Data;

            //停止服务
            IStartOrStopWindowsService stopWindows = new IStartOrStopWindowsService
            {
                serviceConfig = new ServiceConfig
                {
                    Address = serviceConfig.Address,
                    ServiceName = serviceConfig.ServiceName
                },
                servicestatus = ServiceControllerStatus.Stop
            };
            var y5 = studyClass.StartOrStopWindowsService(rpcServer, stopWindows);
            if (!y5.Success) return ApiResponse.Fail<OUpdateFile>("停止服务失败");

            //更新服务参数
            IUpdateFile filey = new IUpdateFile
            {
                ListFileID = new List<IUpdateFileList>(),
                serviceConfig = new ServiceConfig()
            };
            foreach (var item in seefileResult.ListFileid)
            {
                IUpdateFileList fileList = new IUpdateFileList
                {
                    FileID = Baisse.Common.Utils.GenerateMD5(item.FileID),
                    FileName = item.FileID
                };
                filey.ListFileID.Add(fileList);
            }
            filey.serviceConfig.Address = serviceConfig.Address;
            filey.serviceConfig.ServiceName = serviceConfig.ServiceName;
            var y2 = studyClass.UpdateFile(rpcServer, filey);
            //更新成功
            if (y1.Success)
            {

                //开始服务
                IStartOrStopWindowsService startWindows = new IStartOrStopWindowsService
                {
                    serviceConfig = new ServiceConfig
                    {
                        Address = serviceConfig.Address,
                        ServiceName = serviceConfig.ServiceName
                    },
                    servicestatus = ServiceControllerStatus.Start
                };
                var y6 = studyClass.StartOrStopWindowsService(rpcServer, startWindows);
                if (!y6.Success) return ApiResponse.Fail<OUpdateFile>("停止服务失败");

                var oFile = y2.Data;
                return ApiResponse.Success(oFile);
            }
            else
            {
                //回退更新文件逻辑
            }
            return ApiResponse.Fail<OUpdateFile>("失败");
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<OSeeFile>> SeeFile(ApiRequest input)
        {
            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
            };

            StudyService studyClass = new StudyService();

            //获取配置服务信息
            var y = studyClass.SeeServicesInfo(rpcServer, new ISeeServicesInfo());
            if (!y.Success) return ApiResponse.Fail<OSeeFile>("获取SeeServicesInfo服务失败");
            //服务信息
            var servicesinfo = y.Data;
            var serviceConfig = servicesinfo.serviceList.FirstOrDefault(x => x.ServiceName == "Baisse.BaseDataService");
            if (string.IsNullOrEmpty(serviceConfig.ServiceName) && string.IsNullOrEmpty(serviceConfig.Address)) return ApiResponse.Fail<OSeeFile>("请配置服务信息内容");

            ISeeFile seeFile = new ISeeFile
            {
                serviceConfig = new ServiceConfig
                {
                    Address = serviceConfig.Address,
                    ServiceName = serviceConfig.ServiceName
                }
            };
            var y1 = studyClass.SeeFile(rpcServer, seeFile);
            if (y.Success)
            {
                var oFile = y1.Data;
                return ApiResponse.Success(oFile);
            }
            return ApiResponse.Fail<OSeeFile>("失败");
        }

        /// <summary>
        /// 查看服务配置信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<OSeeServicesInfo>> SeeServicesInfo(ApiRequest input)
        {
            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
            };

            StudyService studyClass = new StudyService();

            var y = studyClass.SeeServicesInfo(rpcServer, new ISeeServicesInfo());

            if (y.Success)
            {
                var oFile = y.Data;
                return ApiResponse.Success(oFile);
            }
            return ApiResponse.Fail<OSeeServicesInfo>("失败");
        }

        /// <summary>
        /// 启动或关闭服务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<OStartOrStopWindowsService>> StartOrStopWindowsService(ApiRequest input)
        {
            RpcServerContext rpcServer = new RpcServerContext()
            {
                LogId = Guid.NewGuid().ToString(),
            };

            StudyService studyClass = new StudyService();

            IStartOrStopWindowsService startOrStopWindows = new IStartOrStopWindowsService
            {
                serviceConfig = new ServiceConfig
                {
                    Address = "D:\\15BaisseService\\Baisse.BaseDataService",
                    Listen = 10,
                    RpcServerPort = 6969,
                    ServiceName = "Baisse.BaseDataService"
                },
                servicestatus = ServiceControllerStatus.Stop
            };

            var y = studyClass.StartOrStopWindowsService(rpcServer, startOrStopWindows);

            if (y.Success)
            {
                var oFile = y.Data;
                return ApiResponse.Success(oFile);
            }
            return ApiResponse.Fail<OStartOrStopWindowsService>("失败");
        }
    }
}
