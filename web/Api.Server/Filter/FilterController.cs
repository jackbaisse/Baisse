using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HDF.Blog.WebApi.Filter
{
    public class FilterController : ActionFilterAttribute
    {
        private readonly ILogger<FilterController> _logger;
        private readonly string _loggid;
        public FilterController(ILogger<FilterController> logger)
        {
            Guid guid = Guid.NewGuid();
            _logger = logger;
            _loggid = guid.ToString();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                //获取控制器名称
                var controllerName = descriptor.ControllerName;
                //获取action名称
                var actionName = descriptor.ActionName;
                //获取body内容
                var content = JsonConvert.SerializeObject(context.Result);
                _logger.LogWarning($"出参{_loggid}:{controllerName}/{actionName},{content}");
            }
            catch (Exception)
            {

            }

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                //获取控制器名称
                var controllerName = descriptor.ControllerName;
                //获取action名称
                var actionName = descriptor.ActionName;
                //获取body内容
                var content = JsonConvert.SerializeObject(context.ActionArguments);
                _logger.LogWarning($"入参{_loggid}：{controllerName}/{actionName},{content}");
            }
            catch (Exception)
            {

            }
        }
    }
}
