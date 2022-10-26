using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Baisse.Model.Models.ApiModel
{
    public class AccessToken
    {
        public AccessToken(IHttpContextAccessor contextAccessor)
        {
            var context = contextAccessor.HttpContext;

            if (context == default || context.User == default)
                return;

            Id = context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            UserCode = context.User.Claims.FirstOrDefault(c => c.Type == "UserCode")?.Value;
            UserName = context.User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
            UserRoles = context.User.Claims.FirstOrDefault(c => c.Type == "UserRoles")?.Value;
            UserType = context.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
        }

        public bool Valid => !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(UserCode);


        public string Id { get; }

        public string UserCode { get; }
        public string UserName { get; }
        public string UserRoles { get; }
        public string UserType { get; set; }
    }
}
