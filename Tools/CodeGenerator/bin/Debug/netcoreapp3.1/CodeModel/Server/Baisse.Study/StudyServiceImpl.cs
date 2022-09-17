using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Study.BLL;
using Baisse.Study.Common;
using Baisse.StudyCommon;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Baisse.StudyCommon.RPC.RPCModel;
using Microsoft.Extensions.Logging;

namespace Baisse.Study
{
    public class StudyServiceImpl : IStudyService
    {
        public ILogger _logger { get; set; }
        public StudyServiceImpl()
        {
            _logger = LogHelp.GetInstance<StudyService>();
        }
        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy args)
        {
            try
            {
                _logger.LogInformation("Studyss");
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
        /// 文件上传下载
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

                BLL.StudyInfoBLL.FileUpload(args);

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
