using Huach.Framework.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huach.Framework.Helper
{
    public static class HttpUtil
    {
        private static readonly Logger _logHelper = Logger.CreateLogger(typeof(HttpUtil));
        private static ConcurrentQueue<HttpPostInfo> httpQueue = new ConcurrentQueue<HttpPostInfo>();
        static HttpUtil()
        {
            Task.Factory.StartNew(() =>
            {
                //开启线程 执行队列信息
                while (httpQueue.TryDequeue(out HttpPostInfo queue))
                {
                    //等待10秒，
                    Thread.Sleep(10000);

                    var isSuccess = TryPost(queue.Url, out string result, queue.Parameter, queue.Hearders);
                    if (!isSuccess && queue.FaildNum < queue.MaxFaildNum)
                    {
                        queue.FaildNum += 1;
                        httpQueue.Enqueue(queue);//重新加入队列
                    }
                    queue.OnComplete?.Invoke(isSuccess, result, queue);
                }
            });
        }
        public static string Post(string url, Dictionary<string, string> parameter = null, Dictionary<string, string> hearders = null)
        {
            string paramData = DictionaryToParams(parameter);
            if (TryPost(url, out string result, paramData, hearders))
            {
                return result;
            }
            return null;
        }

        public static string Post<T>(string url, T paramsObject, Dictionary<string, string> hearders = null)
        {
            string paramData = ObjectToParams(paramsObject);
            if (TryPost(url, out string result, paramData, hearders))
            {
                return result;
            }
            return null;
        }
        public static bool TryPost(string url, out string result, Dictionary<string, string> parameter = null, Dictionary<string, string> hearders = null)
        {
            string paramData = DictionaryToParams(parameter);
            return TryPost(url, out result, paramData, hearders);
        }
        public static bool TryPost<ParameT, ResultT>(string url, out ResultT result, ParameT parameter, Dictionary<string, string> hearders = null)
        {
            string paramData = ObjectToParams(parameter);
            if (TryPost(url, out string strResult, paramData, hearders))
            {
                return JsonHelper.TryDeserialize(strResult, out result);
            };
            result = default(ResultT);
            return false;
        }
        public static bool TryPost<ResultT>(string url, out ResultT result, Dictionary<string, string> parameter = null, Dictionary<string, string> hearders = null)
        {
            string paramData = DictionaryToParams(parameter);
            if (TryPost(url, out string strResult, paramData, hearders))
            {
                return JsonHelper.TryDeserialize(strResult, out result);
            };
            result = default(ResultT);
            return false;
        }
        /// <summary>
        /// 将class对象转换为url参数 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string ObjectToParams<T>(T data)
        {
            string paramString = string.Empty;
            if (data != null)
            {
                Type type = data.GetType();
                PropertyInfo[] pInfoList = type.GetProperties();
                paramString = string.Join("&", pInfoList.Select(a => $"{a.Name}={a.GetValue(data).ToString()}"));
            }
            return paramString;
        }

        /// <summary>
        /// 字典参数转换为url参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string DictionaryToParams(Dictionary<string, string> param)
        {
            string paramString = string.Empty;
            if (param != null)
            {
                paramString = string.Join("&", param.Select(a => $"{a.Key}={a.Value}"));
            }
            return paramString;
        }

        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="result">返回结果</param>
        /// <param name="paramData">参数</param>
        /// <param name="hearders">请求头</param>
        /// <returns></returns>
        private static bool TryPost(string postUrl, out string result, string paramData, Dictionary<string, string> hearders = null)
        {
            try
            {
                if (!postUrl.StartsWith("http://") && !postUrl.StartsWith("https://"))
                    throw new Exception("postUrl错误");

                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Timeout = 30 * 1000;//30秒
                webReq.UserAgent = "Mozilla/4.0 (compatible;)";
                if (hearders != null)
                {
                    foreach (var item in hearders)
                    {
                        webReq.Headers.Add(item.Key, item.Value);
                    }
                }
                byte[] byteArray = Encoding.Default.GetBytes(paramData); //转化 
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                _logHelper.Error(JsonConvert.SerializeObject(new
                {
                    postUrl,
                    paramData,
                    hearders,
                    ex.Message
                }));
                result = null;
                return false;
            }
        }
        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <param name="hearders"></param>
        /// <param name="onComplete">队列执行完成时执行回调</param>
        /// <param name="maxFaildNum">最大重新执行次数</param>
        public static void Enqueue(string url, Dictionary<string, string> parameter = null, Dictionary<string, string> hearders = null, Action<bool, string, HttpPostInfo> onComplete = null, int maxFaildNum = 5)
        {
            httpQueue.Enqueue(new HttpPostInfo
            {
                FaildNum = 0,
                Hearders = hearders,
                Parameter = parameter,
                Url = url,
                OnComplete = onComplete,
                MaxFaildNum = 5
            });
        }

        public class HttpPostInfo
        {
            public string Url { get; set; }
            public Dictionary<string, string> Parameter { get; set; }
            public Dictionary<string, string> Hearders { get; set; }

            public int MaxFaildNum { get; set; }

            public int FaildNum { get; set; }
            /// <summary>
            /// 队列执行完成时执行回调
            /// 如果执行失败了返回false,请求将再次加入队列。直到失败次数FaildNum变成MaxFaildNum
            /// </summary>
            public Action<bool, string, HttpPostInfo> OnComplete { get; set; }

        }
    }
}
