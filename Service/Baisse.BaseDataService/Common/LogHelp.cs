using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Baisse.BaseDataService.Common
{
    public class LogHelp
    {
        public static string LogId { get; set; }

        public static ILogger GetInstance<T>()
        {
            return Program._serviceProvider.GetService<ILoggerFactory>().CreateLogger<T>();
        }
    }
}
