using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PressureTest
{
    class Program
    {
        public static long _totalTimeCost = 0;
        public static int _endedConnectionCount = 0;
        public static int _failedConnectionCount = 0;
        public static int _connectionCount = 0;

        static void Main(string[] args)
        {
            //获取配置文件
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var settings = builder.GetSection("AppSettings").Get<AppSettings>();

            SendRequest();

            //string content = "{\"businessType\":\"0\",\"hospOrderId\":\"78e21f26-bd7d-4d36-9160-dd52cc0da6d1\",\"payType\":null,\"amount\":0.5,\"patientId\":\"318\",\"medicalNo\":\"U202012749\",\"admissionNo\":\"U202012749\",\"admissionId\":null,\"certificateType\":1,\"certificateNo\":\"542301200103052011\",\"cardType\":2,\"cardNo\":\"U202012749\",\"name\":\"巴桑石达\",\"birthday\":\"2001/3/5 0:00:00\",\"sex\":\"1\",\"orderType\":\"0\"}";

            var connectionCount = 5;
            var requestThread = new Thread(() => StartRequest(connectionCount))
            {
                IsBackground = true
            };
            requestThread.Start();
            Console.ReadLine();
        }
        static void StartRequest(int connectionCount)
        {
            Reset();

            for (int i = 0; i < connectionCount; i++)
            {
                Console.WriteLine(i.ToString());
                ThreadPool.QueueUserWorkItem(x => SendRequest());
            }
        }

        private static void SendRequest()
        {
            try
            {
                var url = "http://192.168.110.126:8001/his/transaction/scanOrder";
                var content = "{\"businessType\": \"2\", 	\"hospOrderId\": \"0ba9f26c-4425-4ec0-b8fe-d3029db0ecfc\", 	\"payType\": \"\", 	\"amount\": 6, 	\"patientId\": \"abb91a8e-b885-4950-8e69-4648e80ff5ba\", 	\"medicalNo\": \"\", 	\"admissionNo\": \"\", 	\"admissionId\": \"\", 	\"certificateType\": 1, 	\"certificateNo\": \"\", 	\"cardType\": 2, 	\"cardNo\": \"\", 	\"name\": \"贺诗桐                                            \", 	\"birthday\": \"\", 	\"sex\": \"1  \", 	\"orderType\": \"0\" }";
                var result = HttpRequestPlus(url, content, "post");
                SuccessConnection();
            }
            catch (Exception e)
            {
                FaileConnection();
            }
        }

        private static void FaileConnection()
        {
            Interlocked.Increment(ref _failedConnectionCount);
            Console.WriteLine("失败个数：" + _failedConnectionCount);
        }

        private static void SuccessConnection()
        {
            Interlocked.Increment(ref _endedConnectionCount);
            Console.WriteLine("成功个数：" + _endedConnectionCount);
        }

        static void Reset()
        {
            _failedConnectionCount = 0;
            _endedConnectionCount = 0;
            _totalTimeCost = 0;
        }

        /// <summary>
        /// 扫码支付webapi get/post通用方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">json</param>
        /// <param name="Method">请求类型</param>
        /// <returns></returns>
        public static string HttpRequestPlus(string url, string postData, string Method)
        {
            try
            {

                //拼接服务器地址
                //请求路径
                //定义request并设置request的路径
                //Log.InfoLog("调用接口 url:" + url + " postData:" + postData + " Method:" + Method);
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls| (SecurityProtocolType)0x300 | (SecurityProtocolType)0xC00 | (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //ServicePointManager.
                HttpWebRequest request;
                if (Method == "GET")
                {
                    request = (HttpWebRequest)WebRequest.Create(url + postData);
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                }
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
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
                throw;
            }
        }
    }

}
