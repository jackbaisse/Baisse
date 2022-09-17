using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.StudyCommon.RPC.RPCModel
{
    public class RequestContent<T>
    {
        /// <summary>
        /// 请求日志id
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public T Data { get; set; }

    }

    public class RequestContent
    {
        public static RequestContent<T> Request<T>(T data)
            => new RequestContent<T>() { Data = data};
    }
}
