using DS.HeartSummer.BLL;
using DS.HeartSummer.Common;
using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using DS.HeartSummer.WebApp.Models;
using DS.HeartSummer.WebApp.Models.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class WeChatAPIController : Controller
    {
        // GET: WeChatAPI
        WeChatHelper wh = new WeChatHelper();

        #region 服务器效验
        [HttpGet]
        public ActionResult Index(EncryptPostModel model)
        {
            string[] temp = { model.Token, model.Timestamp, model.Nonce };
            Array.Sort(temp);
            string strtemp = string.Join("", temp);
            string saltemp = wh.SHA1(strtemp);
            if (saltemp != null && saltemp.ToLower().Equals(model.Signature))
            {
                //返回echostr参数内容 
                return Content(model.Echostr);
            }
            return Content("");
        }

        #endregion 
        [HttpPost]
        public ActionResult Index()
        {
            Stream stre = Request.InputStream;

            string str = XmlToEntity(XDocument.Load(stre));
            return Content(str);
        }

        #region By Message 返回Xml信息
        public string GetNewsXml(ResponseMessage msg)
        {
            /*
            <xml>
            <ToUserName><![CDATA[toUser]]></ToUserName>
            <FromUserName><![CDATA[fromUser]]></FromUserName>
            <CreateTime>12345678</CreateTime>
            <MsgType><![CDATA[news]]></MsgType>
            <ArticleCount>2</ArticleCount>
            <Articles>
            <item>
            <Title><![CDATA[title1]]></Title> 
            <Description><![CDATA[description1]]></Description>
            <PicUrl><![CDATA[picurl]]></PicUrl>
            <Url><![CDATA[url]]></Url>
            </item>
            </Articles>
            </xml> 
            */
            msg.MsgType = ResponseMsgType.News;
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", msg.FromUserName);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", msg.ToUserName);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", msg.CreateTime);
            sb.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", msg.MsgType);
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", msg.ArticleCount);
            sb.Append("<Articles>");
            for (int i = 0; i < msg.ArticleCount; i++)
            {
                sb.Append("<item>");
                sb.AppendFormat("<Title><![CDATA[{0}]]></Title>", msg.Articles[i].Title);
                sb.AppendFormat("<Description><![CDATA[{0}]]></Description>", msg.Articles[i].Description);
                sb.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", msg.Articles[i].PicUrl);
                sb.AppendFormat("<Url><![CDATA[{0}]]></Url>", msg.Articles[i].Url);
                sb.Append("<item>");
            }
            sb.Append("</Articles>");
            sb.Append("</xml>");


            //"<xml>" +
            //             "<ToUserName><![CDATA[" + fromUserName + "]]></ToUserName>" +
            //             "<FromUserName><![CDATA[" + toUserName + "]]></FromUserName>" +
            //             "<CreateTime>" + GetTime() + "</CreateTime>" +
            //             "<MsgType><![CDATA[text]]></MsgType>" +
            //             "<Content><![CDATA[" + backString + "]]></Content>" +
            //             "</xml>";

            return sb.ToString();
        }

        public string GetTextXml(ResponseMessage msg)
        {
            StringBuilder sb = new StringBuilder();
            msg.MsgType = ResponseMsgType.Text;
            sb.Append("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", msg.FromUserName);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", msg.ToUserName);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", msg.CreateTime);
            sb.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", msg.MsgType);
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", msg.Content);
            sb.Append("</xml>");

            return sb.ToString(); ;
        }
        #endregion 
        #region 自定义菜单
        public ActionResult ChangeButton()
        {

            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(new { button = CreateMenus() }, Newtonsoft.Json.Formatting.Indented, jSetting);

            string chr = HttpHelper.CreateHttpRequest(string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", wh.GetAccessToken()), "post", json.ToLower());

            var ecm = (ErrCodeMsg)JsonConvert.DeserializeObject<ErrCodeMsg>(chr, new JsonSerializerSettings());

            return Content(ecm.Errcode.ToString());
        }

        private List<CustomMenu> CreateMenus()
        {

            List<CustomMenu> cms = new List<CustomMenu>();
            //菜单 1
            CustomMenu cm = new CustomMenu();
            cm.Name = "🙅亲心小夏";
            cm.Type = "view";
            cm.Url = "http://www.codefirst.top/";
            cms.Add(cm);
            //菜单 2
            CustomMenu cm2 = new CustomMenu();
            cm2.Name = "👙福利社";
            List<CustomSubMenu> csms2 = new List<CustomSubMenu>();
            CustomSubMenu csm2 = new CustomSubMenu();
            //csm2.Type = "view";
            //csm2.Name = "搜搜";
            //csm2.Url = "http://www.soso.com/";
            //csms2.Add(csm2);
            //csm2 = new CustomSubMenu();
            //csm2.Type = "view";
            //csm2.Name = "QQ";
            //csm2.Url = "http://v.qq.com/";
            //csms2.Add(csm2);
            //csm2 = new CustomSubMenu();
            //csm2.Type = "view";
            //csm2.Name = "百度3";
            //csm2.Url = "http://www.baidu.com/";
            //csms2.Add(csm2);
            //csm2 = new CustomSubMenu();
            csm2.Type = "view";
            csm2.Name = "🚀一键回校";
            csm2.Url = "http://www.codefirst.top/MyTest/BaseIndex";
            csms2.Add(csm2);
            csm2 = new CustomSubMenu();
            csm2.Type = "click";
            csm2.Name = "🌹帮你表白";
            csm2.Key = "event_key_defined";
            csms2.Add(csm2);
            cm2.Sub_button = csms2;
            cms.Add(cm2);
            //菜单 3
            CustomMenu cm3 = new CustomMenu();
            cm3.Name = "💯教务帮助";
            List<CustomSubMenu> csms = new List<CustomSubMenu>();
            CustomSubMenu csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "☁导入成绩";
            csm.Url = "http://www.codefirst.top/Summer/LoginJxxy";
            csms.Add(csm);
            csm = new CustomSubMenu();
            csm.Type = "click";
            csm.Name = "🌂使用帮助";
            csm.Key = "event_click_help";
            csms.Add(csm);
            csm = new CustomSubMenu();
            //csm.Type = "view";
            //csm.Name = "百度3";
            //csm.Url = "http://www.baidu.com/";
            //csms.Add(csm);
            //csm = new CustomSubMenu();
            //csm.Type = "click";
            //csm.Name = "click1";
            //csm.Key = "click1";
            //csms.Add(csm);
            //csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "🌟校历🌟";
            csm.Url = "http://www.codefirst.top/Summer/GetSchoolCalendar";
            csms.Add(csm);
            cm3.Sub_button = csms;
            cms.Add(cm3);
            return cms;
        }
        #endregion 

        /// <summary>
        /// Xml转为实体
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string XmlToEntity(XDocument doc)
        {
            RequestMessage reqm = new RequestMessage();

            try
            {
                reqm.MsgType = (RequestMsgType)Enum.Parse(typeof(RequestMsgType), doc.Root.Element("MsgType").Value, true);
            }
            catch (Exception)
            {
                return null;
            }
            reqm.FromUserName = doc.Root.Element("FromUserName").Value;
            reqm.ToUserName = doc.Root.Element("ToUserName").Value;
            reqm.CreateTime = doc.Root.Element("CreateTime").Value;
            //查同
            if (reqm.MsgType == RequestMsgType.Event)
            {

                if (IsContainsEvent(reqm.FromUserName, reqm.CreateTime))
                {
                    return null;
                }
                //处理事件
                else
                {
                    ResponseMessage resm = new ResponseMessage();
                    resm.FromUserName = reqm.FromUserName;
                    resm.ToUserName = reqm.ToUserName;
                    reqm.Event = (Event)Enum.Parse(typeof(Event), doc.Root.Element("Event").Value, true);
                    switch (reqm.Event)
                    {
                        case Event.subscribe:
                            SettingsService setService = new SettingsService();

                            //订阅  
                            resm.Content = setService.LoadEntities(c => c.Name == "event_subscribe").FirstOrDefault().value;
                            return GetTextXml(resm);
                        case Event.CLICK:
                            reqm.EventKey = doc.Root.Element("EventKey").Value;
                            switch (reqm.EventKey)
                            {
                                case "event_key_defined":

                                    resm.ArticleCount = 1;
                                    Item i = new Item();
                                    i.Title = "要玩就玩大的,小夏偶吧任你控制！";
                                    i.Description = "想要控制CodeFirst哥，先来做一个著名的心理测试，看看你的控制欲指数是多少！下面这些事项，如果有一项是经常梦见的就要";
                                    i.PicUrl = @"http://www.codefirst.top/content/img/defined.jpg";
                                    i.Url = "http://mp.weixin.qq.com/s?__biz=MzI2ODE1MTk0Mw==&mid=403821935&idx=1&sn=7c3f45cb0de8047e0d5574b5d1fc0a97&scene=0#wechat_redirect";
                                    resm.Articles = new Item[] { i };
                                    return GetNewsXml(resm);
                                case "event_click_help":
                                    ISettingsService settins = new SettingsService();
                                    Settings set = settins.LoadEntities(c => c.Name == "event_click_help").FirstOrDefault();
                                    resm.Content = set.value;
                                    return GetTextXml(resm);

                                default:
                                    return "";
                            }

                        default:
                            return "";
                    }
                }
            }
            else
            {

                if (reqm.MsgType==RequestMsgType.Text)
                {
                    reqm.MsgId = Convert.ToInt64(doc.Root.Element("MsgId").Value);
                    reqm.Content = doc.Root.Element("Content").Value;
                    if (IsContainsMsgId(reqm.MsgId))
                    {
                        return null;
                    }
                    //处理消息
                    else
                    {
                        return ProTextMessage(reqm);
                    }
                }
                else
                {

                    return "";
                }
            }
        }

        #region 查同

        private static List<Int64> msgs = new List<long>();
        Dictionary<string, string> evts = new Dictionary<string, string>();
        /// <summary>
        /// 查同
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns>true 包含</returns>
        public bool IsContainsMsgId(Int64 msgId)
        {

            if (msgs.Contains(msgId))
            {
                //包含
                return true;
            }
            else
            {
                msgs.Add(msgId);
                if (msgs.Count > 1000)
                {
                    msgs.Clear();
                }
                return false;
            }
        }
        /// <summary>
        /// 事件查同
        /// </summary>
        /// <param name="fromUserName"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        private bool IsContainsEvent(string fromUserName, string createTime)
        {

            if (evts.ContainsKey(fromUserName) && evts[fromUserName] == createTime)
            {
                return true;
            }
            else
            {
                evts.Add(fromUserName, createTime);
                return false;
            }
        }
        #endregion  
        /// <summary>
        /// 处理文本信息
        /// </summary>
        /// <param name="reqm"></param>
        /// <returns></returns>
        private string ProTextMessage(RequestMessage reqm)
        {
            ResponseMessage resm = new ResponseMessage();
            resm.FromUserName = reqm.FromUserName;
            resm.ToUserName = reqm.ToUserName;

            string def = "";
            string userName = "";
            string userId = "";
            //讯息查询
            //成绩
            if (ProStudentInfo(reqm.Content, out def, out userId))
            {
                if (def == "")
                {
                    resm.Content = "没有找到您的信息？快快点击 教务帮助->倒入成绩!";
                }
                else
                {
                    resm.Content = def;
                    resm.Content += "\r\n" + "<a href='http://www.codefirst.top/summer/GetSutentAllDate?userId=" + userId + "' >点击这可以查看所有成绩哦！</a>\r\n";
                }
                return GetTextXml(resm);
            }
            //课表
            if (ProStudentClass(reqm.Content, out def, out userName))
            {
                if (def == "")
                {
                    resm.Content = "没有找到您的信息？快快点击 教务帮助->倒入成绩!";
                    return GetTextXml(resm);
                }
                else
                { 
                    resm.ArticleCount = 1;
                    Item i = new Item();
                    i.Title = userName + " 的课表";
                    i.Description = "不经历一番路程、怎知你态度虔诚。";
                    i.PicUrl = @"http://www.codefirst.top/Content/img/亲心小夏_20160313_111245.jpg";
                    i.Url = "http://www.codefirst.top/summer/getstudentclasstable?" + def;
                    resm.Articles = new Item[] { i };
                    return GetNewsXml(resm);
                } 
            }

            if (ProDefined(reqm.Content, reqm.FromUserName))
            {
                //自定义成功！

                resm.Content = "小夏我被你征服啦！快来回复实验一下吧！";
                return GetTextXml(resm);
            }

            if (GetDefinedDate(reqm.Content, out def))
            {
                resm.Content = def;
                return GetTextXml(resm);
            }
            if (reqm.Content == "校历")
            {
                resm.ArticleCount = 1;
                Item i = new Item();
                i.Title = "校历";
                i.Description = "不经历一番路程、怎知你态度虔诚。";
                i.PicUrl = @"http://www.codefirst.top/Models/xl.jpg";
                i.Url = "http://www.codefirst.top/Mytest";
                resm.Articles = new Item[] { i };
                return GetNewsXml(resm);
            }
            else
            {
                resm.Content = GetSimsiSay(reqm);
                return GetTextXml(resm);
            }
        }

        private bool ProStudentClass(string content, out string str, out string strname)
        {
            bool isexistCJ = content.Contains("课表#");
            str = "";
            strname = "";
            if (isexistCJ)
            {
                string[] cons = content.Split('#');
                if (cons[1].Length < 2)
                {
                    return false;
                }
                else
                {
                    string userName = cons[1].Trim();

                    IBLL.IStudentGradeService sgService = new BLL.StudentGradeService();
                    IBLL.IStudentInfoService stInfoService = new BLL.StudentInfoService();
                    IBLL.IStudentImageTableService stImage = new BLL.StudentImageTableService();
                    IBLL.ISettingsService stService = new BLL.SettingsService();

                    string term = stService.LoadEntities(C => C.Name == "default_class_term").FirstOrDefault().value;

                    var stug = stInfoService.LoadEntities(c => c.UserName == userName || c.UserId == userName).FirstOrDefault();
                    if (stug != null)
                    {
                        strname = userName;
                        str = "userinfoid=" + stug.Id + "&term=" + term;
                    }
                    else
                    {
                        str = "";
                    }
                    return true;
                }
            }

            return false;
        }

        private bool ProStudentInfo(string content, out string str, out string userId)
        {
            bool isexistCJ = content.Contains("成绩#");
            str = "";
            userId = "";
            if (isexistCJ)
            {
                string[] cons = content.Split('#');



                if (cons[1].Length < 2)
                {
                    return false;
                }
                else
                {
                    string userName = cons[1].Trim();

                    IBLL.IStudentGradeService sgService = new BLL.StudentGradeService();
                    IBLL.IStudentInfoService stInfoService = new BLL.StudentInfoService();
                    IBLL.ISettingsService stService = new BLL.SettingsService();

                    string term = stService.LoadEntities(C => C.Name == "default_grade_term").FirstOrDefault().value;

                    var stug = stInfoService.LoadEntities(c => c.UserName == userName || c.UserId == userName).FirstOrDefault();
                    if (stug != null)
                    {
                        var sgss = stug.StudentGrades.Where(c => c.Term == term);
                        //var stug = sgService.LoadEntities(c => c.Term == term && c.UserName == userName || c.UserId == userName).ToList();
                        str += term + "\n\r";
                        foreach (StudentGrades sg in sgss)
                        {
                            str += sg.Course.Substring(8, sg.Course.Length - 8) + " " + sg.ValidGrade + "\n\r";
                        }
                        userId = stug.Id.ToString();
                    }
                    else
                    {
                        userId = "";
                        str = "";
                        //格式错误！

                    }
                    return true;
                }
            }

            return false;
        }

        #region Simsi接口
        /// <summary>
        /// Simsi接口
        /// </summary>
        /// <param name="reqm"></param>
        /// <returns></returns>
        private string GetSimsiSay(RequestMessage reqm)
        {
            //SimsiSay
            string url = string.Format("http://sandbox.api.simsimi.com/request.p?key={0}&lc=ch&ft=1.0&text={1}", "c704a91d-3cb2-42f1-9d8e-59ae8e1bc94d", reqm.Content);
            string js = HttpHelper.CreateHttpRequest(url, "get", "");
            var ecm = (ErrCodeMsg)JsonConvert.DeserializeObject<ErrCodeMsg>(js, new JsonSerializerSettings());
            if (ecm.Result == "100")
            {
                return ecm.Response;
            }
            else
            {
                return null;
            }

        }
        #endregion
        #region 用户自定义回复
        /// <summary>
        /// 用户自定义
        /// </summary>
        /// <param name="userSay">用户说的话</param>
        /// <param name="fromUserName">返回的 “问”</param>
        /// <param name="SayAnswer">返回的 “答”</param>
        /// <param name="flag">0-普通句子  1-自定义成功  2-自定义失败</param>
        /// <returns></returns>
        public bool ProDefined(string userSay, string fromUserName)
        {
            bool wen = userSay.Contains("问");
            bool da = userSay.Contains("#答");
            if (wen && da)
            {
                string ask = "";
                string back = "";
                string[] ansandq = userSay.Split('#');
                foreach (string str in ansandq)
                {
                    if (str.Length <= 2)
                    {
                        return false;
                    }
                }
                ask = (ansandq[0].Substring(2, ansandq[0].Length - 2));
                ask.Trim();
                back = (ansandq[1].Substring(2, ansandq[1].Length - 2));
                back.Trim();
                SaveDefined(ask, back, fromUserName);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存Defined
        /// </summary>
        /// <param name="Ask">问</param>
        /// <param name="back">回</param>
        /// <param name="fromUserName"></param>
        /// <returns>false-存在相同//true-添加成功！</returns>
        public bool SaveDefined(string Ask, string back, string fromUserName)
        {
            //判断是否存在
            IBLL.IDefinedsService defService = new BLL.DefinedsService();
            if (defService.LoadEntities(c => c.Ask == Ask && c.Back == back).Count() > 0)
            {
                return false;
            }
            else
            {
                Defineds df = new Defineds();
                df.FromUserName = fromUserName;
                df.Ask = Ask;
                df.Back = back;
                df.subtime = DateTime.Now;
                defService.AddEntity(df);
                defService.DbSession.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 通过数据库 回答
        /// </summary>
        /// <param name="sayUser"></param>
        /// <returns></returns>
        public bool GetDefinedDate(string sayUser, out string def)
        {
            IBLL.IDefinedsService defService = new BLL.DefinedsService();
            List<Defineds> defList = defService.LoadEntities(c => c.Ask.Contains(sayUser)).ToList();
            if (defList.Count() > 0)
            {
                Random r = new Random();
                def = defList[r.Next(0, defList.Count())].Back;
                return true;
            }
            def = "";
            return false;
        }
        #endregion
    }
}


