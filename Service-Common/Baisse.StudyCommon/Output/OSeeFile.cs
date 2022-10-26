using System;
using System.Collections.Generic;
using System.Text;

namespace Baisse.StudyCommon.Output
{
    public class OSeeFile
    {
        public List<OSeeFileList> ListFileid { get; set; }
    }
    public class OSeeFileList
    {
        public string FileID { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] Content { get; set; }
    }
}
