using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.StudyCommon.common
{
    public class RpcServerContext
    {
        public string MethodName { get; set; }
        public string ClassdName { get; set; }
        public string Body { get; set; }
        public string Return { get; set; }
    }
}
