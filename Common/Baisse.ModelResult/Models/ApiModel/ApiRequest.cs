using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.ApiModel
{
    public class ApiRequest
    {
        public string service { get; set; }
        public string ispCode { get; set; }
        public string appid { get; set; }
        public string hospitalCode { get; set; }
        public string version { get; set; }
        public string sign { get; set; }
        public string requestEncrypted { get; set; }
    }
}
