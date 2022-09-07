using System;
using System.ServiceProcess;
using System.Threading;
using Baisse.Study.ConfigModel;
using Baisse.StudyCommon;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Baisse.Study
{
    class Program
    {
        public static IServiceProvider _serviceProvider { get; set; }
        /// <summary>
        /// 配置文件类
        /// </summary>
        static void Main(string[] args)
        {
            InitBuilder();

            Thread.Sleep(20000);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new StudyService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static void InitBuilder()
        {
            //获取配置文件
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var settings = builder.GetSection("AppSettings").Get<AppSettings>();

            //注入容器
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => {
                    x.AddLog4Net();
                    //默认的配置文件路径是在根目录，且文件名为log4net.config
                    //如果文件路径或名称有变化，需要重新设置其路径或名称
                    //比如在项目根目录下创建一个名为cfg的文件夹，将log4net.config文件移入其中，并改名为log.config
                    //则需要使用下面的代码来进行配置
                    //x.AddLog4Net(new Log4NetProviderOptions()
                    //{
                    //    Log4NetConfigFileName = "cfg/log.config",
                    //    Watch = true
                    //});
                })
                .AddSingleton(settings)
                .AddSingleton<IStudyService, StudyServiceImpl>()
                .BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }
    }
}
