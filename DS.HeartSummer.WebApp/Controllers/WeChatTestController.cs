using DS.HeartSummer.Common;
using DS.HeartSummer.WebApp.Models.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class WeChatTestController : Controller
    {
        // GET: WeChatTest
        public ActionResult Index()
        {
            string app_Id = "wxa23920af9a26bdf2";
            string redirect_url = "http://www.codefirst.top/wechattest/LoginTest";
            redirect_url = HttpHelper.GetUrlEncoding(redirect_url);
            string response_type = "code";
            string scope = "snsapi_userinfo";
            string state = "STATE";
            string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect", app_Id, redirect_url, response_type, scope, state);


            return Content(url);
        }
        public ActionResult LoginTest(string code, string state)
        {
            string app_Id = "wxa23920af9a26bdf2";
            string secret = "bd681e71a30a2a03ae96702f3f112b57";

            //获取access_token（用户token）
            string json = HttpHelper.CreateHttpRequest("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + app_Id + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code", "get", "");
            UserBaseToken ubt = JsonConvert.DeserializeObject<UserBaseToken>(json, new JsonSerializerSettings());


            //拉取用户信息
            //https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
            string json2 = HttpHelper.CreateHttpRequest("https://api.weixin.qq.com/sns/userinfo?access_token=" + ubt.access_token + "&openid=OPENID&lang=zh_CN", "get", "");

            //UsersBaseData ubd = JsonConvert.DeserializeObject<UsersBaseData>(json, new JsonSerializerSettings());
            //StringBuilder sb = new StringBuilder();
            

            return Content(json2);
        }
        public ActionResult test()
        {
            string jsonText = @"{
   'openid':'OPENID',
   'nickname': 'NICKNAME',
   'sex':'1',
   'province':'PROVINCE',
   'city':'CITY',
   'country':'COUNTRY',
    'headimgurl':'http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46', 
	'privilege':[
	'PRIVILEGE1'
	'PRIVILEGE2'
    ],
    'unionid': 'o6_bmasdasdsad6_2sgVt7hMZOPfL'
}"; 

            UsersBaseData ubd = JsonConvert.DeserializeObject<UsersBaseData>(jsonText, new JsonSerializerSettings());

            return Content("");
        }
    }
}