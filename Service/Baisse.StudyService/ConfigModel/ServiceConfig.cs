using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.Study.ConfigModel
{
    /// <summary>
    /// 服务信息类
    /// </summary>
    public class ServiceConfig
    {
        public string ServiceName { get; set; }
        public int RpcServerPort { get; set; }
        public int Listen { get; set; }
        public string Address { get; set; }

    }
}
