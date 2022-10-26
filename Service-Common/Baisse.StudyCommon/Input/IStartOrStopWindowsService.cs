using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Model.Models.AppsettingModel;

namespace Baisse.StudyCommon.Input
{
    public class IStartOrStopWindowsService
    {
        /// <summary>
        /// 更新服务配置信息
        /// </summary>
        public ServiceConfig serviceConfig { get; set; }

        public ServiceControllerStatus servicestatus { get; set; }
    }

    public enum ServiceControllerStatus
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 1,
        /// <summary>
        /// 启动
        /// </summary>
        Start = 2,
    }
}
