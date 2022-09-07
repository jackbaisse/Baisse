using System;
using System.ServiceProcess;
using System.Threading;
using Baisse.Study.ConfigModel;
using Microsoft.Extensions.Configuration;

namespace Baisse.Study
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var settings = builder.GetSection("AppSettings").Get<AppSettings>();
            Thread.Sleep(20000);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new StudyService(settings)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
