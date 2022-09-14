using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.Study.BLL;
using Baisse.Study.Common;
using Baisse.StudyCommon;
using Baisse.StudyCommon.common;
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
                    msg = args.name
                };

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                return ResponseContent.Fail<Ostudy>(e.Message);
            }
        }

        public async Task<ResponseContent<Ostudy>> Studyss6(RpcServerContext context, Istudy args)
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
    }
}
