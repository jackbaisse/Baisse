
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace HDF.Blog.WebApi.Extensions
{
    /// <summary>
    /// 数据库拓展
    /// </summary>
    public static class DbContextExtension
    {

        ///// <summary>
        ///// 配置DbContext
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="dBConfig"></param>
        //public static void AddDbContextService(this IServiceCollection services, DBConfig dBConfig)
        //{
        //    ILoggerFactory consoleLoggerFactory = LoggerFactory.Create(builder =>
        //    {
        //        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
        //            .AddConsole();
        //    });

        //    services.AddDbContextService();

        //    services.AddDbContext<GTCMCDSContext>(options =>
        //    {
        //        options.UseLoggerFactory(consoleLoggerFactory);
        //        options.EnableSensitiveDataLogging();
        //        action?.Invoke(options);
        //    });
        //}
    }
}
