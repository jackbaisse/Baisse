using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Baisse.Study.Common
{
    public static class LogHelp
    {
        public static ILogger CreateLogger<T>()
        {
            var logger = Program._serviceProvider.GetService<ILoggerFactory>().CreateLogger<T>();
            return logger;
        }

    }
}
