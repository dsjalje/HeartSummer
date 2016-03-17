using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.Common
{
    public class HttpHelper
    {
        public static Image CreateHttpRequestReturnImage(string url, string host, string referer, string method, string data, CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.Host = host;
            req.Accept = "image/webp,image/*,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            req.ServicePoint.Expect100Continue = false;
            req.Referer = referer;
            req.Headers.Add("Cache-Control", "max-age=0");
            req.Headers.Add("Upgrade-Insecure-Requests", "1");
            req.Headers.Add("DNT", "1");
            req.Headers.Add("Accept-Encoding", "gzip, deflate");
            req.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            req.CookieContainer = cookies;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                Stream resStream = resp.GetResponseStream();
                Image i = Image.FromStream(resStream);
                return i;
            }
        }

        public static Image CreateHttpRequestReturnImage(string url, string method, string data, CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.CookieContainer = cookies;

            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                Stream resStream = resp.GetResponseStream();
                Image i = Image.FromStream(resStream);
                return i;
            }
        }
        public static string FullCreateHttpRequest(string url, string method, string data, string host, string referer, CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.Host = host;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            req.ServicePoint.Expect100Continue = false;
            req.Referer = referer;
            req.Headers.Add("Cache-Control", "max-age=0");
            req.Headers.Add("Upgrade-Insecure-Requests", "1");
            req.Headers.Add("DNT", "1");
            req.Headers.Add("Accept-Encoding", "gzip, deflate");
            req.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            req.CookieContainer = cookies;
            //req.Headers.Add("Cookie", "ASP.NET_SessionId=" + sessionId); 

            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.Default);

                return sr.ReadToEnd();
            }
        }
        public static string GetHeaderByRequest(string url, string method, string data, string header)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;

            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {

                return resp.GetResponseHeader(header);
            }
        }
        public static string CreateHttpRequest(string url, string method, string data)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;

            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

                return sr.ReadToEnd();
            }
        }
        public static string CreateHttpRequest(string url, string method, string data, CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.CookieContainer = cookies;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] bspostData = Encoding.UTF8.GetBytes(data);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bspostData, 0, bspostData.Length);
                    reqStream.Close();
                }

            }
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

                return sr.ReadToEnd();
            }
        }
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlEncoding(string url)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }
        /// <summary>
        /// URLdeCode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlDeEncoding(string url)
        {
            return System.Web.HttpUtility.UrlDecode(url,Encoding.Default);
        }

        public static string MD5Calc(string str)
        {
            string s = "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] buffer = md5.ComputeHash(bytes);
            for (int i = 0; i < buffer.Length; i++)
            {
                s += buffer[i].ToString("X2");
            }
            return s;
        }

        /// <summary>
        /// 获取随即字符串（包含字符大写字母）
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandonString(int count)
        {
            int number;
            string checkCode = String.Empty;     //存放随机码的字符串 

            System.Random random = new Random();

            for (int i = 0; i < count; i++) //产生4位校验码 
            {
                number = random.Next();
                number = number % 36;
                if (number < 10)
                {
                    number += 48;    //数字0-9编码在48-57 
                }
                else
                {
                    number += 55;    //字母A-Z编码在65-90 
                }

                checkCode += ((char)number).ToString();
            }
            return checkCode;
        }

        //将image转化为二进制 
        public static byte[] GetByteImage(Image img)

        {

            byte[] bt = null;

            if (!img.Equals(null))
            {
                using (MemoryStream mostream = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);

                    bmp.Save(mostream, System.Drawing.Imaging.ImageFormat.Jpeg);//将图像以指定的格式存入缓存内存流

                    bt = new byte[mostream.Length];

                    mostream.Position = 0;//设置留的初始位置

                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));

                }

            }

            return bt;

        }

        /// <summary>
        /// 二进制转为Image
        /// </summary>
        /// <param name="mybyte"></param>
        /// <returns></returns>
        public static Image GetImageByByte(byte[] mybyte)
        {
            Image image;
            MemoryStream mymemorystream = new MemoryStream(mybyte, 0, mybyte.Length);
            image = Image.FromStream(mymemorystream);
            return image;
        }
    }
}
