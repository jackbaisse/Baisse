using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using Baisse.Filenamesj.ConfigModel;
using Baisse.FilenamesjCommon;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Baisse.Filenamesj.Common;
using Baisse.Filenamesj.RpcServer;

namespace Baisse.Filenamesj
{
    internal class FilenamesjService : ServiceBase
    {
        private readonly AppSettings _appSettings;
        private static ILogger _logger;
        public FilenamesjService()
        {
            _logger = LogHelp.GetInstance<FilenamesjService>();
            //_iFilenamesjService = Program._serviceProvider.GetService<IFilenamesjService>();
            _appSettings = Program._serviceProvider.GetService<AppSettings>();
            _logger.LogWarning("实例化成功");
        }

        protected override void OnStart(string[] args)
        {
            Lister<FilenamesjServiceImpl> lister = new Lister<FilenamesjServiceImpl>(10, 1024);
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
