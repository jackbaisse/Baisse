using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.Model.Models.RPCModel
{
    public class ResponseContent<T>
    {
        public readonly DateTime Time = DateTime.Now;
        public string Code { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public ApiPageInfo Page { get; set; }
        public int TotalCount { get; set; }
    }

    public class ResponseContent
    {
        public static ResponseContent<T> Success<T>(T data)
            => new ResponseContent<T>() { Success = true, Data = data, Code = "200", Message = "成功" };

        public static ResponseContent<T> Success<T>(T data, int totalCount)
            => new ResponseContent<T>() { Success = true, Data = data, TotalCount = totalCount };

        public static ResponseContent<T> Fail<T>(Exception exception)
            => new ResponseContent<T>() { Success = false, Message = exception.Message };

        public static ResponseContent<T> Fail<T>(string message)
            => new ResponseContent<T>() { Success = false, Message = message };

    }
}
