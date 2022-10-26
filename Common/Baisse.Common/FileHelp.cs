using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Baisse.Common
{
    public static class FileHelp
    {
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="copypath">复制路径</param>
        /// <param name="b">是否覆盖</param>
        /// <returns></returns>
        public static bool CopyFile(string path, string copypath, bool b)
        {
            try
            {
                File.Copy(path, copypath, b);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除文件夹内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="b">是否删除内容及文件夹</param>
        /// <returns></returns>
        public static bool DeleteFolder(string path, bool b)
        {
            try
            {
                //备份更新文件
                DirectoryInfo directory = new DirectoryInfo(path);
                if (b)
                {
                    directory.Delete();
                }
                else
                {
                    //获取文件
                    var files = directory.GetFiles();
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是否创建成功</returns>
        public static bool CreateFolder(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                //创建文件夹
                if (!directoryInfo.Exists) directoryInfo.Create();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
            return true;
        }
    }
}
