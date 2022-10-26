using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Study.DLL;
using Baisse.StudyCommon.Input;
using System.IO;
using Baisse.StudyCommon.Output;
using System.Linq;
using Baisse.Study.Common;
using Microsoft.Extensions.Logging;

namespace Baisse.Study.BLL
{
    internal class StudyInfoBLL
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="args"></param>
        internal static void FileUpload(IFileUpload args)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\" + args.serviceConfig.ServiceName + "\\";

                Baisse.Common.FileHelp.CreateFolder(path);

                //DirectoryInfo directoryInfo = new DirectoryInfo(path);
                ////创建文件夹
                //if (!directoryInfo.Exists) directoryInfo.Create();

                using (FileStream fsWrite = new FileStream(path + args.FileName, FileMode.Append, FileAccess.Write))//处理完成再追加
                {
                    fsWrite.Write(args.FileContent, 0, int.Parse(args.FileLength));
                }
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static OFileDownload FileDownload(IFileDownload args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\" + args.FileName;

            string fileStream = File.ReadAllText(path);

            var buffcs = Encoding.UTF8.GetBytes(fileStream);

            return new OFileDownload()
            {
                FileID = args.FileID,
                Content = buffcs
            };
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static OSeeFile SeeFile(ISeeFile args)
        {
            OSeeFile result = new OSeeFile();
            string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\" + args.serviceConfig.ServiceName + "\\";
            DirectoryInfo root = new DirectoryInfo(path);
            //获取文件
            var files = root.GetFiles();
            List<OSeeFileList> listof = new List<OSeeFileList>();
            //没有条件
            if (args.ListFileID == null || args.ListFileID.Count <= 0)
            {
                foreach (var file in files)
                {
                    listof.Add(new OSeeFileList { FileID = file.Name });//获取文件名
                }
            }
            else
            {
                foreach (var file in args.ListFileID)
                {
                    var afile = files.Where(x => x.Name == file.FileID);
                    if (afile != null)
                    {
                        result.ListFileid.Add(new OSeeFileList { FileID = file.FileID });
                    }
                }
            }
            result.ListFileid = listof;
            return result;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static void UpdateFile(IUpdateFile args)
        {
            var _logger = ServiceHelp.GetInstance<StudyInfoBLL>();//日志对象

            if (args.serviceConfig == null || args.ListFileID == null) return;

            string basepath = AppDomain.CurrentDomain.BaseDirectory;
            string updatefile = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\" + args.serviceConfig.ServiceName + "\\";//获取更新服务包路径
            string updatefile_bak = AppDomain.CurrentDomain.BaseDirectory + "UpdateFileBak\\" + args.serviceConfig.ServiceName + "\\";//备份更新服务包路径
            string baissefile = AppDomain.CurrentDomain.BaseDirectory + "BaisseFile\\";//临时文件夹
            string baissefile_bak = AppDomain.CurrentDomain.BaseDirectory + "BaisseFileBak\\";//备份临时文件夹

            //创建文件夹
            Baisse.Common.FileHelp.CreateFolder(updatefile);
            Baisse.Common.FileHelp.CreateFolder(updatefile_bak);

            //删除文件夹内容
            Baisse.Common.FileHelp.DeleteFolder(baissefile, false);
            Baisse.Common.FileHelp.DeleteFolder(baissefile_bak, false);


            //解压更新文件
            if (args.ListFileID != null && args.ListFileID.Count > 0)
            {
                foreach (var iFile in args.ListFileID)
                {
                    string filepath = updatefile + iFile.FileName;
                    //解压更新文件
                    Baisse.Common.CompressHelp.DeCompressionFile(filepath, baissefile);
                    //备份更新文件
                    DirectoryInfo root = new DirectoryInfo(baissefile);
                    //获取文件
                    var files = root.GetFiles();
                    foreach (var file in files)
                    {
                        //服务程序目录文件
                        string ipath = args.serviceConfig.Address + "\\" + file.Name;
                        if (File.Exists(ipath))
                        {
                            //复制文件
                            File.Copy(ipath, baissefile_bak + file.Name, true);
                        }
                        else
                        {
                            _logger.LogInformation("新增文件：" + file.Name);
                        }
                    }
                    //压缩备份文件
                    Baisse.Common.CompressHelp.CompressionFile(baissefile_bak, updatefile_bak + iFile.FileName);

                    try
                    {
                        //备份成功更新文件
                        DirectoryInfo directoryInfo = new DirectoryInfo(baissefile);
                        //获取文件
                        var updatefiles = directoryInfo.GetFiles();
                        foreach (var file in updatefiles)
                        {
                            //服务程序目录文件
                            string ipath = args.serviceConfig.Address + "\\" + file.Name;
                            //复制文件
                            File.Copy(file.FullName, ipath, true);
                        }
                    }
                    catch (Exception e)
                    {
                        //更新失败还原操作
                        throw e.InnerException;
                    }

                }
            }
            ////备份更新文件
            //DirectoryInfo root = new DirectoryInfo(path);
            ////获取文件
            //var files = root.GetFiles();

            //foreach (var file in files)
            //{
            //    string copyPath = basepath + file.Name;

            //    File.Copy(copyPath)
            //}



            //List<OFile> listof = new List<OFile>();
            ////没有条件
            //if (args.ListFileID == null || args.ListFileID.Count <= 0)
            //{
            //    foreach (var file in files)
            //    {
            //        listof.Add(new OFile { FileID = file.Name });//获取文件名
            //    }
            //}
            //else
            //{
            //    foreach (var file in args.ListFileID)
            //    {
            //        var afile = files.Where(x => x.Name == file.FileID);
            //        if (afile != null)
            //        {
            //            result.ListFileid.Add(new OFile { FileID = file.FileID });
            //        }
            //    }
            //}
            //result.ListFileid = listof;
            //return result;
        }
    }
}
