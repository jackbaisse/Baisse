using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.AppsettingModel
{
    public class AppSettings
    {
        public TokenConfig TokenConfig { get; set; }
        public CorsConfig CorsConfig { get; set; }
        public SpaConfig SpaConfig { get; set; }
        /// <summary>
        /// 数据库配置
        /// </summary>
        public DBConfig DBConfig { get; set; }
        /// <summary>
        /// 服务配置
        /// </summary>
        public List<ServiceConfig> ServiceSettings { get; set; }
    }
}
