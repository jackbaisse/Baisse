using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.ApiModel
{
    public class ApiResponse<T>
    {
        public readonly DateTime Time = DateTime.Now;
        public string Code { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public ApiPageInfo Page { get; set; }
        public int TotalCount { get; set; }
    }

    public class ApiResponse
    {
        public static ApiResponse<T> Success<T>(T data)
            => new ApiResponse<T>() { Success = true, Data = data };

        public static ApiResponse<T> Success<T>(T data, int totalCount)
            => new ApiResponse<T>() { Success = true, Data = data, TotalCount = totalCount };

        public static ApiResponse<T> Fail<T>(Exception exception)
            => new ApiResponse<T>() { Success = false, Message = exception.Message };

        public static ApiResponse<T> Fail<T>(string message)
            => new ApiResponse<T>() { Success = false, Message = message };
    }
}
