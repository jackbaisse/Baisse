using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Study.DLL;
using Baisse.StudyCommon.Input;
using System.IO;

namespace Baisse.Study.BLL
{
    internal class StudyInfoBLL
    {
        internal static void SelectInfo(StudyCommon.Input.Istudy args)
        {
            StudyInfoDLL.SelectInfo(args);
        }

        internal static void FileUpload(IFile args)
        {
            string filename = args.FileID + "_" + args.FileName;
            string path = AppDomain.CurrentDomain.BaseDirectory + "BaisseFile\\";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //创建文件夹
            if (!directoryInfo.Exists) directoryInfo.Create();

            FileStream fileStream = new FileStream(path + filename, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(args.FileContent);
            fileStream.Close();

        }
    }
}
