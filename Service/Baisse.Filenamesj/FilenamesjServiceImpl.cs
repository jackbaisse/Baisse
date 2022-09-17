using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Filenamesj.BLL;
using Baisse.Filenamesj.Common;
using Baisse.FilenamesjCommon;
using Baisse.FilenamesjCommon.Input;
using Baisse.FilenamesjCommon.Output;
using Baisse.FilenamesjCommon.RPC.RPCModel;
using Microsoft.Extensions.Logging;

namespace Baisse.Filenamesj
{
    public class FilenamesjServiceImpl : IFilenamesjService
    {
        public ILogger _logger { get; set; }
        public FilenamesjServiceImpl()
        {
            _logger = LogHelp.GetInstance<FilenamesjService>();
        }
        public ResponseContent<Ostudy> Filenamesjss5(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Filenamesjss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "adsfasdfasdkjklajsdlfkjasdlkfjalskdjflaskdjflkasjdflkajsdflkjasdlkfjasldkjfalksdjflkasjdflkasjdflkjasdflkjasdlkfjaslkdjfalskdjflaksjdfklajsdflkjasdlfkjasdlkfjaskldjfaklsdjflkasjdflkajsdflkjasdlkfjaslkdjfaklsdjfklasjdflkjsadklfjasdlkfjasldkjflaskdjflkasjdflksajdfkjasdlfkjasdjflaksdjfaskdjflkasjdflkasjdflkjasdlfkjasdklfjaslkdjflkasjdfereresdafasdfasdfasererdfadsfalsdkjflaksdjflkajsdflkjasdlkfjadsfasdfasdkjklajsdlfkjasdlkfjalskdjflaskdjflkasjdflkajsdflkjasdlkfjasldkjfalksdjflkasjdflkasjdflkjasdflkjasdlkfjaslkdjfalskdjflaksjdfklajsdflkjasdlfkjasdlkfjaskldjfaklsdjflkasjdflkajsdflkjasdlkfjaslkdjfaklsdjfklasjdflkjsadklfjasdlkfjasldkjflaskdjflkasjdflksajdfkjasdlfkjasdjflaksdjfaskdjflkasjdflkasjdflkjasdlfkjasdklfjaslkdjflkasjdfereresdafasdfasdfasererdfadsfalsdkjflaksdjflkajsdflkjasdlkfjalskdjflaskjdflkasjdflkajsdflkjasdlkfjaslkdjflaksdjflkajsdflkadsfasdfasdkjklajsdlfkjasdlkfjalskdjflaskdjflkasjdflkajsdflkjasdlkfjasldkjfalksdjflkasjdflkasjdflkjasdflkjasdlkfjaslkdjfalskdjflaksjdfklajsdflkjasdlfkjasdlkfjaskldjfaklsdjflkasjdflkajsdflkjasdlkfjaslkdjfaklsdjfklasjdflkjsadklfjasdlkfjasldkjflaskdjflkasjdflksajdfkjasdlfkjasdjflaksdjfaskdjflkasjdflkasjdflkjasdlfkjasdklfjaslkdjflkasjdfereresdafasdfasdfasererdfadsfalsdkjflaksdjflkajsdflkjasdlkfjalskdjflaskjdflkasjdflkajsdflkjasdlkfjaslkdjflaksdjflkajsdflkjasdlfkjasldkfjaslkdjf00000000adsfasdfasdkjklajsdlfkjasdlkfjalskdjflaskdjflkasjdflkajsdflkjasdlkfjasldkjfalksdjflkasjdflkasjdflkjasdflkjasdlkfjaslkdjfalskdjflaksjdfklajsdflkjasdlfkjasdlkfjaskldjfaklsdjflkasjdflkajsdflkjasdlkfjaslkdjfaklsdjfklasjdflkjsadklfjasdlkfjasldkjflaskdjflkasjdflksajdfkjasdlfkjasdjflaksdjfaskdjflkasjdflkasjdflkjasdlfkjasdklfjaslkdjflkasjdfereresdafasdfasdfasererdfadsfalsdkjflaksdjflkajsdflkjasdlkfjalskdjflaskjdflkasjdflkajsdflkjasdlkfjaslkdjflaksdadsfasdfasdkjklajsdlfkjasdlkfjalskdjflaskdjflkasjdflkajsdflkjasdlkfjasldkjfalksdjflkasjdflkasjdflkjasdflkjasdlkfjaslkdjfalskdjflaksjdfklajsdflkjasdlfkjasdlkfjaskldjfaklsdjflkasjdflkajsdflkjasdlkfjaslkdjfaklsdjfklasjdflkjsadklfjasdlkfjasldkjflaskdjflkasjdflksajdfkjasdlfkjasdjflaksdjfaskdjflkasjdflkasjdflkjasdlfkjasdklfjaslkdjflkasjdfereresdafasdfasdfasererdfadsfalsdkjflaksdjflkajsdflkjasdlkfjalskdjflaskjdflkasjdflkajsdflkjasdlkfjaslkdjflaksdjflkajsdflkjasdlfkjasldkfjaslkdjf00000000jflkajsdflkjasdlfkjasldkfjaslkdjf00000000jasdlfkjasldkfjaslkdjf00000000alskdjflaskjdflkasjdflkajsdflkjasdlkfjaslkdjflaksdjflkajsdflkjasdlfkjasldkfjaslkdjf000000001111111"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public ResponseContent<Ostudy> Filenamesjss6(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Filenamesjss");
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

        public ResponseContent<Ostudy> Filenamesjss1(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Filenamesjss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "Filenamesjss1"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public ResponseContent<Ostudy> Filenamesjss2(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Filenamesjss");
                args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = "Filenamesjss2"
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        /// <summary>
        /// 文件上传下载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResponseContent<OFile> FileUpload(RpcServerContext context, IFile args)
        {
            try
            {
                _logger.LogInformation("Filenamesjss");
                args = context.GetArgs<IFile>();

                BLL.FilenamesjInfoBLL.FileUpload(args);

                OFile ostudy = new OFile
                {
                    FileID = args.FileID
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<OFile>(e.Message);
            }
        }

        public ResponseContent<OFile> FileDownload(RpcServerContext context, IFile args)
        {
            throw new NotImplementedException();
        }
    }
}
