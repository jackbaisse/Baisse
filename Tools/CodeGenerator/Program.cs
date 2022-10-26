using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CodeGenerator
{
    class Program
    {
        static string _filePath = AppDomain.CurrentDomain.BaseDirectory + "CodeModel";
        static string _copyFilePath = AppDomain.CurrentDomain.BaseDirectory + "CopyModel";
        static string _nameSpace = "Study";
        static string _copyNameSpace = "Filenamesj";

        static void Main(string[] args)
        {
            //获取配置文件
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var settings = builder.GetSection("AppSettings").Get<AppSettings>();

            if (settings != null)
            {
                //_nameSpace = settings.ServiceSettings.NameSpace;
                //_copyNameSpace = settings.ServiceSettings.CopyNameSpace;
                _filePath = settings.ServiceSettings.FilePath;
                _copyFilePath = settings.ServiceSettings.CopyFilePath;
            }

            FileDirectoryInfo(_filePath, _copyFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="copypath">复制后的路径</param>
        static void FileDirectoryInfo(string path, string copypath)
        {
            DirectoryInfo root = new DirectoryInfo(path);

            //获取文件
            var file = root.GetFiles();

            foreach (var fileInfo in file)
            {
                string files = fileInfo.Name;
                //if (files.Contains(_nameSpace))
                //{
                //    files = files.Replace(_nameSpace, _copyNameSpace);
                //}
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
                //if (item.Name.Contains(files))
                //{
                //    files = files.Replace(_nameSpace, _copyNameSpace);
                //}
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

            //if (fileStream.Contains(_nameSpace))
            //{
            //    fileStream = fileStream.Replace(_nameSpace, _copyNameSpace);
            //}
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
