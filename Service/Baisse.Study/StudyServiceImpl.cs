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

        public void Studys(RpcServerContext context, Action<Istudy> action = null)
        {
            try
            {
                _logger.LogInformation("Studyss");
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

        public void Studyss(RpcServerContext context, Func<RpcServerContext, Istudy> func = null)
        {
            try
            {
                _logger.LogInformation("Studyss");
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

        public void Studyss1(RpcServerContext context, Func<Istudy, RpcServerContext> func = null)
        {
            try
            {
                _logger.LogInformation("Studyss");
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

        public Ostudy Studyss2(RpcServerContext context, Func<Istudy, RpcServerContext> func = null)
        {
            try
            {
                _logger.LogInformation("Studyss");
                var args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));
                return ostudy;
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
            return null;
        }

        public Ostudy Studyss3(RpcServerContext context, Istudy istudy = null)
        {
            try
            {
                _logger.LogInformation("Studyss");
                var args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));
                return ostudy;
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
            return null;
        }

        public Ostudy Studyss3(RpcServerContext context, Action<Istudy> action)
        {
            try
            {
                _logger.LogInformation("Studyss");
                var args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));
                return ostudy;
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
            return null;
        }

        public ResponseContent<Ostudy> Studyss4(RpcServerContext context, Action<Istudy> action)
        {
            try
            {
                _logger.LogInformation("Studyss");
                var args = context.GetArgs<Istudy>();

                Ostudy ostudy = new Ostudy
                {
                    msg = args.name
                };

                context.Return(ResponseContent.Success(ostudy));

                return ResponseContent.Success(ostudy);
            }
            catch (Exception e)
            {
                context.Return(ResponseContent.Fail<string>(e.Message));
            }
            return null;
        }

        public ResponseContent<Ostudy> Studyss5(RpcServerContext context, Istudy istudy)
        {
            try
            {
                _logger.LogInformation("Studyss");
                var args = context.GetArgs<Istudy>();

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
    }
}
