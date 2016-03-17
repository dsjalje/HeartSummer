using DS.HeartSummer.Common;
using DS.HeartSummer.Model;
using DS.HeartSummer.WebApp.Models.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class MyTestController : Controller
    {
        // GET: MyTest
        public ActionResult BaseIndex()
        {

        
            return View(); 
        }

        private string ProHtmlCourseData(string html)
        {
            Regex r = new Regex("姓名[^<]*");//获取姓名
            string userName = r.Match(html).ToString();
            userName = userName.Substring(3, userName.Length - 3);

            //获取主要数据段
            Regex r2 = new Regex("<td width=25% align=left[^()]*</form>");
            html = r2.Match(html).ToString();

            //去除html标签
            Regex r3 = new Regex("<[^>]*>");

            html = r3.Replace(html, "  ");
            string str = @" [382438]自然与生活（共享）   1.0   任选课/公共课   初修   考查      92.00   92.00   2015-2016学年第一学期     [080444]软件项目开发实训   6.0   必修课/公共课   初修   考试      74.00   74.00   2015-2016学年第一学期     [080443]Flash动画设计   2.0   限选课/专业课   初修   考查      87.00   87.00   2015-2016学年第一学期     [080445]职业技能训练   4.0   必修课/公共课   初修   考试      88.00   88.00   2015-2016学年第一学期     [080441]软件测试技术   2.0   限选课/专业课   初修   考查      83.00   83.00   2015-2016学年第一学期     [080474]Ajax程序设计   4.0   必修课/公共课   初修   考试      86.00   86.00   2015-2016学年第一学期       ";
            string[] ss = html.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //找到   姓名[^<]*  传入学号 密码  学期
            //<td width=25% align=left[^()]*</form>   一层筛选
            //<[^>]*> 替换html  二层
            int i = 0;
            IBLL.IStudentGradeService service = new BLL.StudentGradeService();

            //判断是否已经存在

            service.DeleteEntities(c => c.UserName == userName && c.Term == "201501");

            while (i < ss.Count())
            {
                StudentGrade sg = new StudentGrade();
                sg.UserName = userName;
                sg.UserId = "1641308012381";
                sg.UserPwd = "514762";
                sg.Term = "201501";
                sg.Course = ss[i++];
                sg.Credit = ss[i++];
                sg.CourseType = ss[i++];
                sg.Quality = ss[i++];
                sg.CheckMode = ss[i++];
                sg.PrimiGrade = ss[i++];
                sg.ValidGrade = ss[i++];
                sg.Mark = ss[i++];
                service.AddEntity(sg);
            }
            service.DbSession.SaveChanges();
            return "导入数据成功！";
        }




        public ActionResult Index()
        {
            string userAgent = Request.UserAgent;

            return Content(userAgent);
        }
        public ActionResult jssdk()
        {
            WeChatHelper wh = new WeChatHelper();
            string jsapi_ticket = "jsapi_ticket=" + "kgt8ON7yVITDhtdwci0qee9iLyXlAXkY3rS4mgJTYvNyYAafXdY4FO_DZD1FAJRxmRdzBmWZSRLvwivfR25D5A";
            string noncestr = "noncestr=" + Guid.NewGuid().ToString();
            string timestamp = "timestamp=" + wh.GetTime().ToString();
            string url = "url=" + "www.codefirst.top";

            string[] temp = { jsapi_ticket, noncestr, timestamp, url };
            Array.Sort(temp);

            string string1 = string.Join("&", temp);
            string signature = wh.SHA1(string1).ToLower();

            Response.Write("jsapi_ticket=" + jsapi_ticket + "<br />");
            Response.Write("noncestr=" + noncestr + "<br />");
            Response.Write("timestamp=" + timestamp + "<br />");
            Response.Write("url=" + url + "<br />");
            Response.Write("string1=" + string1 + "<br />");
            Response.Write("signature=" + signature + "<br />");



            return Content("s");
        }

        public ActionResult ValiDate()
        {
            string s1 = "111";
            string s2 = "222";
            string s3 = "333";

            Response.Write(IsContainsEvent(s1, s2) ? "1" : "2");
            Response.Write(IsContainsEvent(s1, s2) ? "1" : "2");
            Response.Write(IsContainsEvent(s1, s2) ? "1" : "2");

            return Content("");
        }
        #region 查同

        private static List<Int64> msgs = new List<long>();
        //private static List<string[]> evts = new List<string[]>();
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
    }
}