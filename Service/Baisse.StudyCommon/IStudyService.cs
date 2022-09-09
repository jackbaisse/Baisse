using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Baisse.StudyCommon.common;
using Baisse.StudyCommon.Input;
using Baisse.StudyCommon.Output;

namespace Baisse.StudyCommon
{
    public interface IStudyService
    {

        void Mcsgd(RpcServerContext context);
        void Abcdd(RpcServerContext context);
    }
}
