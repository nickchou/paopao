using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaoPao.Comm
{
    /// <summary>
    /// Http请求帮助类
    /// </summary>
    public class HttpUtility
    {
        //
        //private static readonly string defaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private const string DefaultUserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

        private CookieContainer _cookieContainer = new CookieContainer();

        public CookieContainer CookieContainer
        {
            set { _cookieContainer = value; }
            get { return _cookieContainer; }
        }

        #region Get请求

        public string CreateGet(string url)
        {
            return CreateGet(url, Encoding.UTF8, null, null);
        }
        public string CreateGet(string url, Encoding coding)
        {
            return CreateGet(url, coding, null, null);
        }
        public string CreateGet(string url, Encoding coding, string userAgent, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = "application/x-www-form-urlencoded";
                //Cookie
                if (cookies != null)
                    CookieContainer.Add(cookies);
                request.CookieContainer = CookieContainer;
                //userAgent
                if (!string.IsNullOrEmpty(userAgent))
                    request.UserAgent = userAgent;
                else
                    request.UserAgent = DefaultUserAgent;
                // 提交请求数据
                //if (!string.IsNullOrEmpty(para))
                //{
                //    byte[] data = Encoding.ASCII.GetBytes(para);
                //    using (Stream stream = request.GetRequestStream())
                //    {
                //        stream.Write(data, 0, data.Length);
                //    }
                //}
                // 接收返回的页面
                request.Timeout = 30000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, coding);
                string srcString = reader.ReadToEnd();
                return srcString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Post请求

        public string CreatePost(string url, string param)
        {
            return CreatePost(url, Encoding.UTF8, param, null, null);
        }
        public string CreatePost(string url, string param, Encoding coding)
        {
            return CreatePost(url, coding, param, null, null);
        }
        public string CreatePost(string url, Encoding coding, string param, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            //Cookie
            if (cookies != null)
                CookieContainer.Add(cookies);
            request.CookieContainer = CookieContainer;
            //userAgent
            if (!string.IsNullOrEmpty(userAgent))
                request.UserAgent = userAgent;
            else
                request.UserAgent = DefaultUserAgent;
            // 提交请求数据
            if (!string.IsNullOrEmpty(param))
            {
                byte[] data = Encoding.GetEncoding("gb2312").GetBytes(param);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            // 接收返回的页面
            response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, coding);
            string srcString = reader.ReadToEnd();
            return srcString;
        }
        #endregion
    }
}
