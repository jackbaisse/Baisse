using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.StudyCommon.Output
{
    public class OFileDownload
    {
        public string FileID { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] Content { get; set; }
    }
}
