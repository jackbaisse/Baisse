using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Baisse.Common
{
    /// <summary>
    /// 通用方法
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isnull">是否序列化为null(默认为false)</param>
        /// <returns></returns>
        public static string ToJson(object obj, bool isnull = false)
        {
            if (!isnull)
            {
                JsonSerializerSettings jsonSerializer = new JsonSerializerSettings();
                jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
                return JsonConvert.SerializeObject(obj, jsonSerializer);
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
            }

        }
        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);//字符串转成字节
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string key = "7694C81DA6D0487D")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            //byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = new byte[16];
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string key = "7694C81DA6D0487D")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            //byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            //rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// webapi get/post通用方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">json</param>
        /// <param name="Method">请求类型</param>
        /// <returns></returns>
        public static string HttpRequest(string url, string postData, string Method)
        {
            try
            {
                //拼接服务器地址
                //请求路径
                //定义request并设置request的路径
                //Log.InfoLog("调用接口 url:" + url + " postData:" + postData + " Method:" + Method);
                HttpWebRequest request;
                if (Method == "GET")
                {
                    request = (HttpWebRequest)WebRequest.Create(url + postData);
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                }
                request.CookieContainer = new CookieContainer();
                //request.Headers.Add("Authorization", "Bearer " + PublicStaticObject.Token);
                request.Method = Method;
                if (!string.IsNullOrEmpty(postData))
                {
                    //设置参数的编码格式，解决中文乱码
                    if (Method != "GET")
                    {
                        byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                        //设置request的MIME类型及内容长度
                        request.ContentType = "application/json;charset=utf-8";
                        //request.Headers.Add("appKey", "EMR");
                        //request.Headers.Add("appSecret", "7c90fc8c-abf2-49ac-ae9a-09a5cb4d5d90");
                        //request.Headers.Add("Content-Type", "application/json;charset=utf-8");
                        request.ContentLength = postBytes.Length;
                        //打开request字符流
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(postBytes, 0, postBytes.Length);
                        dataStream.Close();
                    }
                }
                string htmlStr = string.Empty;
                //定义response为前面的request响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //定义response字符流
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    htmlStr = reader.ReadToEnd();
                }
                //CheckRequestResult(url, htmlStr, Method);
                //Log.InfoLog(url+"接口返回:"+htmlStr);
                return htmlStr;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
                //Log.ErrorLog("调用WebApi失败" + ex.Message);
                //return "";
            }
        }

        /// <summary>
        /// 扫码支付webapi get/post通用方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">json</param>
        /// <param name="Method">请求类型</param>
        /// <returns></returns>
        public static string HttpRequest(string url, string postData, string Method, string contentType = "application/x-www-form-urlencoded")
        {
            try
            {
                //拼接服务器地址
                //请求路径
                //定义request并设置request的路径
                //Log.InfoLog("调用接口 url:" + url + " postData:" + postData + " Method:" + Method);
                HttpWebRequest request;
                if (Method == "GET")
                {
                    request = (HttpWebRequest)WebRequest.Create(url + postData);
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                }
                request.CookieContainer = new CookieContainer();
                //request.Headers.Add("Authorization", "Bearer " + PublicStaticObject.Token);
                request.Method = Method;
                if (!string.IsNullOrEmpty(postData))
                {
                    //设置参数的编码格式，解决中文乱码
                    if (Method != "GET")
                    {
                        byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                        //设置request的MIME类型及内容长度
                        request.ContentType = contentType;
                        //request.ContentType =  "application/json;charset=utf-8";
                        //request.Headers.Add("appKey", "EMR");
                        //request.Headers.Add("appSecret", "7c90fc8c-abf2-49ac-ae9a-09a5cb4d5d90");
                        //request.Headers.Add("Content-Type", "application/json;charset=utf-8");
                        request.ContentLength = postBytes.Length;
                        //打开request字符流
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(postBytes, 0, postBytes.Length);
                        dataStream.Close();
                    }
                }
                string htmlStr = string.Empty;
                //定义response为前面的request响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //定义response字符流
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    htmlStr = reader.ReadToEnd();
                }
                //CheckRequestResult(url, htmlStr, Method);
                //Log.InfoLog(url+"接口返回:"+htmlStr);
                return htmlStr;
            }
            catch (Exception ex)
            {
                //Log.ErrorLog("调用WebApi失败" + ex.Message);
                //return "";
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 开发者调试日志
        /// </summary>
        /// <param name="text"></param>
        public static void InfoLog(string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = System.IO.Path.Combine(path, "AppLog\\InfoLog\\");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            DirectoryInfo folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles())
            {
                DateTime dt = file.CreationTime;
                if (dt < DateTime.Today)
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch { }
                }
            }
            string fileFullName = System.IO.Path.Combine(path
            , string.Format("{0}.txt", "Info" + DateTime.Now.ToString("yyyyMMdd")));
            using (StreamWriter output = System.IO.File.AppendText(fileFullName))
            {
                output.WriteLine(text + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                output.Close();
            }
        }
    }
}
