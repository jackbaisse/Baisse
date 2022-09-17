using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.Study.ConfigModel
{
    public class ServiceConfig
    {
        public string ServiceName { get; set; }
        public int RpcServerPort { get; set; }
        public int Listen { get; set; }

    }
}
