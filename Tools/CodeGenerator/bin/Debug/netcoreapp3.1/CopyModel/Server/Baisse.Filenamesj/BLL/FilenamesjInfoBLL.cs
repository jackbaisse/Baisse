using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Filenamesj.DLL;
using Baisse.FilenamesjCommon.Input;
using System.IO;

namespace Baisse.Filenamesj.BLL
{
    internal class FilenamesjInfoBLL
    {
        internal static void SelectInfo(FilenamesjCommon.Input.Istudy args)
        {
            FilenamesjInfoDLL.SelectInfo(args);
        }

        internal static void FileUpload(IFile args)
        {
            string filename = args.FileID + "_" + args.FileName;
            string path = AppDomain.CurrentDomain.BaseDirectory + "BaisseFile\\";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //创建文件夹
            if (!directoryInfo.Exists) directoryInfo.Create();

            using (FileStream fsWrite = new FileStream(path + filename, FileMode.Append, FileAccess.Write))//处理完成再追加
            {
                fsWrite.Write(args.FileContent, 0, int.Parse(args.FileLength));
            }

            //if (args.FileSerialNo == "0")
            //{
            //    using (FileStream fsWrite = new FileStream(path + filename, FileMode.OpenOrCreate, FileAccess.Write))//处理完成再追加
            //    {
            //        fsWrite.Write(args.FileContent);
            //    }
            //}
            //else
            //{
            //    using (FileStream fsWrite = new FileStream(path + filename, FileMode.Append, FileAccess.Write))//处理完成再追加
            //    {
            //        fsWrite.Write(args.FileContent);
            //    }
            //}
        }
    }
}
