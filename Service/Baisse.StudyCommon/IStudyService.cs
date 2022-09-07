using System;
using System.Collections.Generic;
using System.Text;
using Baisse.StudyCommon.common;

namespace Baisse.StudyCommon
{
    public interface IStudyService
    {
        void Mcsgd(RpcServerContext context);
        void Abcdd(RpcServerContext context);
    }
}
