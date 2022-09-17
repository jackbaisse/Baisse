using System;
using System.IO;
using System.Text;

namespace CodeGenerator
{
    class Program
    {
        static string _path = AppDomain.CurrentDomain.BaseDirectory + "CodeModel";
        static string _copypath = AppDomain.CurrentDomain.BaseDirectory + "CopyModel";

        static string _spacename = "Study";
        static string _copyspacename = "Filenamesj";

        //static StringBuilder sbpath = new StringBuilder();

        static void Main(string[] args)
        {
            //sbpath.Append(copypath);
            FileDirectoryInfo(_path, _copypath);

            //DirectoryInfo root = new DirectoryInfo(path);
            //DirectoryInfo[] dics = root.GetDirectories();

            //foreach (DirectoryInfo item in dics)
            //{
            //    DirectoryInfo[] infos = item.GetDirectories();
            //    foreach (DirectoryInfo item1 in infos)
            //    {
            //        var a = item1.GetDirectories();
            //        var b = item1.GetFiles();
            //    }
            //}
        }

        static void FileDirectoryInfo(string path, string copypath)
        {
            DirectoryInfo root = new DirectoryInfo(path);

            //获取文件
            var file = root.GetFiles();

            foreach (var fileInfo in file)
            {

                string files = fileInfo.Name;
                if (files.Contains(_spacename))
                {
                    files = files.Replace(_spacename, _copyspacename);
                }
                string pathname = Path.Combine(copypath, files);

                switch (Path.GetExtension(fileInfo.FullName))
                {
                    case ".cs":
                        WriteMethodPlus(fileInfo.FullName, pathname);
                        break;
                    case ".csproj":
                        WriteMethodPlus(fileInfo.FullName, pathname);
                        break;
                    default:
                        WriteMethod(fileInfo.FullName, pathname);
                        break;
                }
            }


            //获取路径文件夹
            var dics = root.GetDirectories();

            foreach (var item in dics)
            {
                //过滤bin和obj文件
                switch (item.Name.ToLower())
                {
                    case "obj":
                        continue;
                    case "bin":
                        continue;
                    default:
                        break;
                }

                ///替换文件名
                string files = item.Name;
                if (item.Name.Contains(files))
                {
                    files = files.Replace(_spacename, _copyspacename);
                }
                string direcpath = Path.Combine(copypath, files);

                //创建文件夹
                WriteDirectory(direcpath);

                //递归
                FileDirectoryInfo(item.FullName, direcpath);
            }

        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="writepath"></param>
        static void WriteMethod(string filepath, string writepath)
        {
            //文件分块传输
            int length = 0;
            byte[] buff = new byte[1024 * 1024];
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fileStream))
                {
                    while ((length = fileStream.Read(buff, 0, buff.Length)) > 0)
                    {

                        using (FileStream stream = new FileStream(writepath, FileMode.Append, FileAccess.Write))
                        {
                            stream.Write(buff, 0, length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="writepath"></param>
        static void WriteMethodPlus(string filepath, string writepath)
        {

            string fileStream = File.ReadAllText(filepath);

            if (fileStream.Contains(_spacename))
            {
                fileStream = fileStream.Replace(_spacename, _copyspacename);
            }
            var buffcs = Encoding.UTF8.GetBytes(fileStream);
            using (FileStream stream = new FileStream(writepath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Write(buffcs);
            }
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>

        static void WriteDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //创建文件夹
            if (!directoryInfo.Exists) directoryInfo.Create();
        }
    }
}
