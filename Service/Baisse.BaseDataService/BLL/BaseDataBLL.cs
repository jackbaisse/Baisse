using System;
using System.Collections.Generic;
using System.Text;
using Baisse.BaseDataService.DLL;
using System.IO;
using System.Linq;
using Baisse.BaseDataCommon.Output;
using Baisse.BaseDataCommon.Input;

namespace Baisse.BaseDataService.BLL
{
    internal class BaseDataBLL
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="args"></param>
        internal static void FileUpload(IFile args)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\";

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
        internal static OFile FileDownload(IFile args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\" + args.FileName;

            string fileStream = File.ReadAllText(path);

            var buffcs = Encoding.UTF8.GetBytes(fileStream);

            return new OFile()
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
            string path = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\";
            DirectoryInfo root = new DirectoryInfo(path);
            //获取文件
            var files = root.GetFiles();
            List<OFile> listof = new List<OFile>();
            //没有条件
            if (args.ListFileID == null || args.ListFileID.Count <= 0)
            {
                foreach (var file in files)
                {
                    listof.Add(new OFile { FileID = file.Name });//获取文件名
                }
            }
            else
            {
                foreach (var file in args.ListFileID)
                {
                    var afile = files.Where(x => x.Name == file.FileID);
                    if (afile != null)
                    {
                        result.ListFileid.Add(new OFile { FileID = file.FileID });
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
        internal static void UpdateFile(ISeeFile args)
        {
            OSeeFile result = new OSeeFile();
            string basepath = AppDomain.CurrentDomain.BaseDirectory;
            string updatefile = AppDomain.CurrentDomain.BaseDirectory + "UpdateFile\\";
            string updatefile_bak = AppDomain.CurrentDomain.BaseDirectory + "UpdateFileBak\\";
            string baissefile = AppDomain.CurrentDomain.BaseDirectory + "BaisseFile\\";
            string baissefile_bak = AppDomain.CurrentDomain.BaseDirectory + "BaisseFileBak\\";

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
                        //根目录程序文件
                        string ipath = basepath + file.Name;
                        //复制文件
                        File.Copy(ipath, baissefile_bak + file.Name, true);
                    }
                    //压缩备份文件
                    Baisse.Common.CompressHelp.CompressionFile(baissefile_bak, updatefile_bak + iFile.FileName);
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
