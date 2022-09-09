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
        public void Abcdd(RpcServerContext context)
        {
            try
            {
                var args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
        }



        public void Mcsgd(RpcServerContext context)
        {
            try
            {
                _logger.LogWarning("Mcsgd" + context.LogId);
                var args = context.GetArgs<Istudy>();

                StudyInfoBLL.SelectInfo(args);


                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
        }
    }
}
