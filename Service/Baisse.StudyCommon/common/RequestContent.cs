using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.StudyCommon.common
{
    public class RequestContent<T>
    {
        /// <summary>
        /// 请求日志id
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string jwtToken { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public T Data { get; set; }

    }

    public class RequestContent
    {
        public static RequestContent<T> Request<T>(T data, string MethodName, string Logid)
            => new RequestContent<T>() { MethodName = MethodName, Data = data, LogId = Logid };

        public static RequestContent<T> Request<T>(T data, string MethodName)
            => new RequestContent<T>() { MethodName = MethodName, Data = data };

    }
}
