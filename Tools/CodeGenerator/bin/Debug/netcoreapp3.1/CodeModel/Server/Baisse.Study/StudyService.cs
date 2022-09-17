using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using Baisse.Study.ConfigModel;
using Baisse.StudyCommon;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Baisse.Study.Common;
using Baisse.Study.RpcServer;

namespace Baisse.Study
{
    internal class StudyService : ServiceBase
    {
        private readonly AppSettings _appSettings;
        private static ILogger _logger;
        public StudyService()
        {
            _logger = LogHelp.GetInstance<StudyService>();
            //_iStudyService = Program._serviceProvider.GetService<IStudyService>();
            _appSettings = Program._serviceProvider.GetService<AppSettings>();
            _logger.LogWarning("实例化成功");
        }

        protected override void OnStart(string[] args)
        {
            Lister<StudyServiceImpl> lister = new Lister<StudyServiceImpl>(10, 1024);
            if (lister.StartLister(5959))
            {
                _logger.LogInformation("服务启动成功");
            }
            else
            {
                _logger.LogError("服务启动失败");
            }
        }

        protected override void OnStop()
        {

        }
    }
}
