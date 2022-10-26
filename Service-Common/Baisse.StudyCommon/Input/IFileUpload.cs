using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Model.Models.AppsettingModel;

namespace Baisse.StudyCommon.Input
{
    public class IFileUpload
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string FileID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] FileContent { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileLength { get; set; }
        /// <summary>
        /// 文件序号
        /// </summary>
        public string FileSerialNo { get; set; }

        /// <summary>
        /// 服务配置信息
        /// </summary>
        public ServiceConfig serviceConfig { get; set; }
    }
}
