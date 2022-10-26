using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Baisse.Model.Models.AppsettingModel;

namespace Baisse.Study.Common
{
    public class ServiceHelp
    {
        public static string LogId { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetInstance<T>()
        {
            try
            {
                return Program._serviceProvider.GetService<ILoggerFactory>().CreateLogger<T>();
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        public static AppSettings AppSettings()
        {
            try
            {
                return Program._serviceProvider.GetService<AppSettings>();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }
    }
}
