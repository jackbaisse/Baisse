using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Model.Models.RPCModel;
using Baisse.BaseDataService.BLL;
using Baisse.BaseDataService.Common;
using Microsoft.Extensions.Logging;
using Baisse.BaseDataCommon;
using Baisse.BaseDataCommon.Output;
using Baisse.BaseDataCommon.Input;

namespace Baisse.BaseDataService
{
    public class BaseDataServiceImpl : IBaseDataService
    {
        public ILogger _logger { get; set; }
        public BaseDataServiceImpl()
        {
            _logger = LogHelp.GetInstance<BaseDataService>();
        }
        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "Studyss5"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public ResponseContent<Ostudy> Studyss6(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public ResponseContent<Ostudy> Studyss1(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "Studyss1"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public ResponseContent<Ostudy> Studyss2(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "Studyss2"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OFile> FileUpload(RpcServerContext context, IFile args)
        {
            try
            {
                _logger.LogInformation("Studyss");
                args = context.GetArgs<IFile>();

                BLL.BaseDataBLL.FileUpload(args);

                OFile ostudy = new OFile
                {
                    FileID = args.FileID
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OFile>(e);
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OFile> FileDownload(RpcServerContext context, IFile args)
        {
            try
            {
                _logger.LogInformation("FileDownload");
                args = context.GetArgs<IFile>();

                var result = BLL.BaseDataBLL.FileDownload(args);

                return ResponseContent.Success(result);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OFile>(e.Message);
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
                var result = BLL.BaseDataBLL.SeeFile(args);

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
        public ResponseContent<OSeeFile> UpdateFile(RpcServerContext context, ISeeFile args)
        {
            try
            {
                _logger.LogInformation("UpdateFile");
                args = context.GetArgs<ISeeFile>();
                BLL.BaseDataBLL.UpdateFile(args);
                return ResponseContent.Success(new OSeeFile());
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OSeeFile>(e.Message);
            }
        }
    }
}
