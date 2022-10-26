using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using Baisse.StudyCommon;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Baisse.Study.Common;
using Baisse.Study.RpcServer;
using System.Threading;
using Baisse.Model.Models.AppsettingModel;
using System.IO;
using Microsoft.Win32;
using Microsoft.AspNetCore.Http.Connections;
using System.Net.Security;
using System.Diagnostics;

namespace Baisse.Study
{
    internal class StudyService : ServiceBase
    {
        private readonly AppSettings _appSettings;
        private static ILogger _logger;
        public StudyService()
        {
            _logger = ServiceHelp.GetInstance<StudyService>();
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

        /// <summary>
        /// 启动Windows 服务
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="stop"></param>
        public static bool StartWindowsService(string servicesName)
        {
            try
            {
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == servicesName)
                    {
                        ServiceController sc = new ServiceController(servicesName);

                        //if (sc.Status == ServiceControllerStatus.Running)
                        //{
                        //    throw new Exception(servicesName + "服务已运行");
                        //}

                        while (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            sc.Start();
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 停止Windows 服务
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="stop"></param>
        public static bool StopWindowsService(string servicesName)
        {
            try
            {
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == servicesName)
                    {
                        ServiceController sc = new ServiceController(servicesName);

                        //if (sc.Status == ServiceControllerStatus.Stopped)
                        //{
                        //    throw new Exception(servicesName + "服务已停止");
                        //}

                        while (sc.Status == ServiceControllerStatus.Running)
                        {
                            sc.Stop();
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
