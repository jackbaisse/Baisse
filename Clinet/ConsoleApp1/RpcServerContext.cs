using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class RpcServerContext
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
        public string RequestData { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        ///		带参数成功返回
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="results">返回参数</param>
        /// <remarks>返回null等同于无参数返回</remarks>
        public void Return<T>(ResponseContent<T> results)
        {
            ResponseData = JsonConvert.SerializeObject(results);
        }

        /// <summary>
        ///		带参数请求
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="results">返回参数</param>
        /// <remarks>返回null等同于无参数返回</remarks>
        public void Requtst<T>(RequestContent<T> results)
        {
            RequestData = JsonConvert.SerializeObject(results);
        }
    }
}
