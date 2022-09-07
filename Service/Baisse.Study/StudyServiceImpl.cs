using System;
using System.Collections.Generic;
using System.Text;
using Baisse.StudyCommon;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;
using Newtonsoft.Json;

namespace Baisse.Study
{
    public class StudyServiceImpl : IStudyService
    {
        public void Abcdd(RpcServerContext context)
        {
            Istudy istudy = JsonConvert.DeserializeObject<Istudy>(context.Body);

            Ostudy ostudy = new Ostudy
            {
                msg = istudy.name
            };
            context.Return = JsonConvert.SerializeObject(ostudy);
        }

        public void Mcsgd(RpcServerContext context)
        {
            try
            {
                RpcServerContext result = new RpcServerContext();
                Ostudy ostudy = new Ostudy();
                ostudy.msg = "成功";

                context.Return = JsonConvert.SerializeObject(ostudy);
            }
            catch (Exception)
            {
                context.Return = "失败";
            }
        }
    }
}
