using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.AppsettingModel
{
    public class TokenConfig
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int AccessExpiration { get; set; }
        /// <summary>
        /// 暂时无用
        /// </summary>
        public int RefreshExpiration { get; set; }
    }
}
