using DS.HeartSummer.BLL;
using DS.HeartSummer.Common;
using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class WeChatHelper
    {
        private static readonly string appId = "wx58f6ea91b1e30192";
        private static readonly string appSecret = "54797ebca489ee71ef9b8d69bad830ae";
        const string token = "daslss";
        ISettingsService settins = new SettingsService();
        #region 获取Access_Token
        /// <summary>
        /// 获取Access_Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns> 
        public string GetAccessToken()
        {
            string access_token = "";
            var set = settins.LoadEntities(c => c.Name == "access_token" && c.subtime > DateTime.Now).FirstOrDefault();
            if (set != null)
            {
                access_token = set.value;
            }
            else
            {
                access_token = HttpHelper.CreateHttpRequest(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret), "get", "");
                var ecm = (ErrCodeMsg)JsonConvert.DeserializeObject<ErrCodeMsg>(access_token, new JsonSerializerSettings());
                Settings setting = settins.LoadEntities(c => c.Name == "access_token").FirstOrDefault();
                setting.Name = "access_token";
                setting.value = ecm.Access_token;
                setting.subtime = DateTime.Now.AddHours(2);
                settins.UpdateEntity(setting);
                settins.DbSession.SaveChanges();
                access_token = setting.value;
            }
            //"{\"access_token\":\"VpUTN3b1j7-CgjkUl2iH-gUeYDIv139YO98n97GJl-1sabLBn1iTr5_jktYG5eNDhwJUVNf4lST0AlnKds1MCxeJHoZ2FAIRpTd2-Fe2ijKWrYHW3-qQNS4x0ybHicXmDTZhADAVLR\",\"expires_in\":7200}"  
            return access_token;
        }
        #endregion


        #region 获取jsapi_ticket
        public string GetJsapiTicket()
        {
            string jsapi_ticket = "";
            var set = settins.LoadEntities(c => c.Name == "jsapi_ticket" && c.subtime > DateTime.Now).FirstOrDefault();
            if (set != null)
            {
                jsapi_ticket = set.value;
            }
            else
            {
                jsapi_ticket = HttpHelper.CreateHttpRequest(string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", GetAccessToken()), "get", "");

                var ecm = (ErrCodeMsg)JsonConvert.DeserializeObject<ErrCodeMsg>(jsapi_ticket, new JsonSerializerSettings());
                //"{\"errcode\":0,\"errmsg\":\"ok\",\"ticket\":\"kgt8ON7yVITDhtdwci0qee9iLyXlAXkY3rS4mgJTYvNyYAafXdY4FO_DZD1FAJRxmRdzBmWZSRLvwivfR25D5A\",\"expires_in\":7200}"

                Settings setting = settins.LoadEntities(c => c.Name == "jsapi_ticket").FirstOrDefault();
                setting.Name = "jsapi_ticket";
                setting.value = ecm.Ticket;
                setting.subtime = DateTime.Now.AddHours(2);
                settins.UpdateEntity(setting);
                settins.DbSession.SaveChanges();
                jsapi_ticket = setting.value;
            }
            //"{\"access_token\":\"VpUTN3b1j7-CgjkUl2iH-gUeYDIv139YO98n97GJl-1sabLBn1iTr5_jktYG5eNDhwJUVNf4lST0AlnKds1MCxeJHoZ2FAIRpTd2-Fe2ijKWrYHW3-qQNS4x0ybHicXmDTZhADAVLR\",\"expires_in\":7200}"  
            return jsapi_ticket;
        }
        public WeChatJssdk GetJssdk(string u)
        {
            WeChatHelper wh = new WeChatHelper();
            WeChatJssdk wj = new WeChatJssdk();
            wj.Noncestr = Guid.NewGuid().ToString();
            wj.Timestamp = wh.GetTime().ToString();
            wj.Jsapi_ticket = GetJsapiTicket(); wj.Url = u;
            string jsapi_ticket = "jsapi_ticket=" + wj.Jsapi_ticket;
            string noncestr = "noncestr=" + wj.Noncestr;
            string timestamp = "timestamp=" + wj.Timestamp;
            string url = "url=" + u.Split('#')[0];

            string[] temp = { jsapi_ticket, noncestr, timestamp, url };
            Array.Sort(temp);

            string string1 = string.Join("&", temp);
            string signature = wh.SHA1(string1).ToLower();



            wj.String1 = string1;
            wj.Signature = signature;
            wj.AppId = appId;
            return wj;
        }
        #endregion

        #region SHA1加密
        public string SHA1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
        #endregion


        #region 创建XML
        /// <summary>
        /// 创建XMl
        /// </summary>
        /// <param name="backString"></param>
        /// <param name="fromUserName"></param>
        /// <param name="toUserName"></param>
        /// <returns></returns>
        public string CreateXml(string backString, string fromUserName, string toUserName)
        {
            string callBack = "";
            callBack = "<xml>" +
                             "<ToUserName><![CDATA[" + fromUserName + "]]></ToUserName>" +
                             "<FromUserName><![CDATA[" + toUserName + "]]></FromUserName>" +
                             "<CreateTime>" + GetTime() + "</CreateTime>" +
                             "<MsgType><![CDATA[text]]></MsgType>" +
                             "<Content><![CDATA[" + backString + "]]></Content>" +
                             "</xml>";
            return callBack;
        }
        #endregion


        #region 获取时间
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public long GetTime()
        {

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)ts.TotalMilliseconds / 1000;
        }
        #endregion



    }


}