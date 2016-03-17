using DS.HeartSummer.BLL;
using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using DS.HeartSummer.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DS.HeartSummer.WebApp.Models.Wechat;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.Caching;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class SummerController : WeChatController
    {
        // GET: Summer

        WeChatHelper wh = new WeChatHelper();
        IBLL.IStudentGradesService stuGradService = new BLL.StudentGradesService(); 
        IBLL.IStudentInfoService stuInfoService = new BLL.StudentInfoService();
        IBLL.ITermsService termService = new BLL.TermsService();
        IBLL.IStudentImageTableService stuImgService = new BLL.StudentImageTableService();  
        public ActionResult Index()
        {
            return Content("");
        }

        #region 导入学员信息
        public ActionResult LoginJxxy()
        {
            string sessionId = HttpHelper.GetHeaderByRequest("http://jwxt.fjjxu.edu.cn/_data/index_LOGIN.aspx", "get", "", "Set-Cookie");
            sessionId = sessionId.Substring(18, 24);

            string read = HttpHelper.CreateHttpRequest("http://jwxt.fjjxu.edu.cn/_data/index_LOGIN.aspx", "get", "");
            Regex reg = new Regex(@"(?is)<input[^/>]* type=(?<hidden>\S*)[^/>]* name=(?<__VIEWSTATE>\S*) [^/>]*/>");
            read = reg.Match(read).ToString();

            read = read.Substring(47, read.Length - 51);



            string hiddenState = Uri.EscapeDataString(read);
            CacheHelper.Set("hiddenState", hiddenState);

            Session["sessionId"] = sessionId;
            return View();
        }

        [HttpPost]
        public ActionResult LoginJxxy(string LoginId, string LoginPwd, string ValiCode)
        {
            if (CacheHelper.Exist("hiddenState") && LoginId != "" && LoginPwd != "" && ValiCode != "")
            {
                string vewState = @"__VIEWSTATE=" + CacheHelper.Get("hiddenState") + "&Sel_Type=STU&txt_sdsdfdsfryuiighgdf=" + LoginId + "&txt_dsfdgtjhjuixssdsdf=" + LoginPwd + "&txt_sftfgtrefjdndcfgerg=" + ValiCode.ToUpper() + "&typeName=%D1%A7%C9%FA&sdfdfdhgwerewt=" + HttpHelper.MD5Calc(LoginId + HttpHelper.MD5Calc(LoginPwd).Substring(0, 30).ToUpper() + "13763").Substring(0, 30).ToUpper() + "&cxfdsfdshjhjlk=" + HttpHelper.MD5Calc(HttpHelper.MD5Calc(ValiCode.ToUpper()).Substring(0, 30).ToUpper() + "13763").Substring(0, 30).ToUpper();

                CookieContainer cookies = new CookieContainer();
                cookies.Add(new Uri("http://jwxt.fjjxu.edu.cn/"), new Cookie("ASP.NET_SessionId", Session["sessionId"].ToString()));
                string ret = HttpHelper.FullCreateHttpRequest("http://jwxt.fjjxu.edu.cn/_data/index_LOGIN.aspx", "post", vewState, "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/_data/index_LOGIN.aspx", cookies);


                Regex reg = new Regex("<span id=\"divLogNote\">[^/]*");


                string read = reg.Match(ret).ToString();
                read = read.Substring(40, read.Length - 40);
                //登录成功！
                //<table[^>]*>[\s\S]*?</table> 匹配table
                //开始导入数据
                if (read == "正在加载权限数据...<")
                {

                    //GetStudentClassTable("20151", cookies);
                    //开始成绩导入
                    BatchProCourseData(cookies, LoginId, LoginPwd);
                    //return Content();
                    return Content("0");
                }
                else
                {
                    return Content(read);

                }

            }
            else
            {
                return null;
            }
        }

        private string BatchProCourseData(CookieContainer cookies, string loginId, string loginPwd)
        {
          
            List<Terms> tls = termService.LoadEntities(C => true).ToList();
            foreach (Terms t in tls)
            {

                string data = "sel_xnxq=" + t.Term + "&radCx=1";
                string html = HttpHelper.FullCreateHttpRequest("http://jwxt.fjjxu.edu.cn/xscj/c_ydcjrdjl_rpt.aspx", "post", data, "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/_data/index_LOGIN.aspx", cookies);
                //开始导入

                ProHtmlCourseData(html, loginId, loginPwd, t.Term, cookies);

            }
            GetStudentClassTable("20151", cookies, loginId);
            return "导入成功！";
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="html"></param>
        private string ProHtmlCourseData(string html, string userId, string userPwd, string term, CookieContainer cookies)
        {
            string userName = "";
            try
            {
                //避免传入数据为空
                Regex r = new Regex("姓名[^<]*");//获取姓名
                userName = r.Match(html).ToString();
                userName = userName.Substring(3, userName.Length - 3);

            }
            catch (Exception)
            {
            }
            if (userName != "")
            {
                //获取主要数据段
                Regex r2 = new Regex("<td width=25% align=left[^鑒]*</form>");
                html = r2.Match(html).ToString();

                //去除html标签
                Regex r3 = new Regex("<[^>]*>");

                html = r3.Replace(html, "\n");

                string[] ss = html.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //找到   姓名[^<]*  传入学号 密码  学期
                //<td width=25% align=left[^()]*</form>   一层筛选
                //<[^>]*> 替换html  二层
                int i = 0;
             
                //判断是否已经存在
                StudentInfo stuInfo = new StudentInfo();
                stuInfo.UserName = userName;
                stuInfo.UserId = userId;
                stuInfo.UserPwd = userPwd;
                int stuinfoid = stuInfoService.GetExistOrCreate(stuInfo).Id;

                stuGradService.DeleteEntities(c => c.UserInfoId == stuinfoid && c.Term == term);
                while ((i + 7) < ss.Count())
                {
                    StudentInfo si = stuInfoService.GetExistOrCreate(stuInfo);
                    StudentGrades sgs = new StudentGrades();
                    sgs.Term = term;
                    sgs.Course = ss[i++];
                    sgs.Credit = ss[i++];
                    sgs.CourseType = ss[i++];
                    sgs.Quality = ss[i++];
                    sgs.CheckMode = ss[i++];
                    sgs.PrimiGrade = ss[i++];
                    sgs.ValidGrade = ss[i++];
                    sgs.Mark = ss[i++];
                    si.StudentGrades.Add(sgs);
                    stuInfoService.UpdateEntity(si);
                }
                StudentInfo stuInfoWithTable = stuInfoService.GetExistOrCreate(stuInfo);

                return "导入数据成功！";
            }
            else
            {
                return null;
            }
        }


        public ActionResult LoginJxxyGetValiDate()
        {
            if (Session["sessionId"] != null)
            {
                CookieContainer cookies = new CookieContainer();
                cookies.Add(new Uri("http://jwxt.fjjxu.edu.cn/"), new Cookie("ASP.NET_SessionId", Session["sessionId"].ToString()));

                Image i = HttpHelper.CreateHttpRequestReturnImage("http://jwxt.fjjxu.edu.cn/sys/ValidateCode.aspx", "get", "", cookies);
                byte[] s = HttpHelper.GetByteImage(i);
                return File(s, "image/jpeg");
            }
            else
            {
                return null;
            }

        }


        #endregion
        #region 获取图表
        /// <summary>
        /// 获取考试相关//ok
        /// </summary>
        /// <param name="term"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private string GetStudentExamTable(string term, CookieContainer cookies)
        {
            //string data = "type=1&w=1110&h=240&xnxq=" + term;

            //http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel_rpt.aspx?m=vhnn70MGbc99CRN
            string m = HttpHelper.GetRandonString(15);
            string hidsjyzm = HttpHelper.MD5Calc("13763" + term + m).ToUpper();

            string gethidyzm = HttpHelper.CreateHttpRequest("http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel.aspx", "get", "", cookies);
            //<input type="hidden" name="hidyzm" value="eaeeab3da91596c811ca97a67c3713z">
            Regex reg = new Regex("<input type=\"hidden\" name=\"hidyzm\" value=\"[0-9a-z]*\">");
            gethidyzm = reg.Match(gethidyzm).ToString();

            gethidyzm = gethidyzm.Substring(42, gethidyzm.Length - 44);



            string data = "sel_xnxq=20141&sel_lc=2014106%2C2014-2015%D1%A7%C4%EA%B5%DA%B6%FE%D1%A7%C6%DA%C6%DA%C4%A9%BF%BC%CA%D4&btn_search=%BC%EC%CB%F7";


            string res = HttpHelper.FullCreateHttpRequest("http://jwxt.fjjxu.edu.cn/KSSW/stu_ksap_rpt.aspx", "post", data, "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/KSSW/stu_ksap.aspx", cookies);



            Image i = HttpHelper.CreateHttpRequestReturnImage("http://jwxt.fjjxu.edu.cn/KSSW/stu_ksap_drawimg.aspx?w=1029&h=100&kslc=2014106", "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/KSSW/stu_ksap_rpt.aspx", "get", "", cookies);
            i.Save(@"D:\Users\Administrator\Desktop\1.jpg");
            return "ok";
        }
        private string GetStudentClassTable(string term, CookieContainer cookies, string userId)
        {

            //http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel_rpt.aspx?m=W8SEuHqlrfJNqG4

            string m = HttpHelper.GetRandonString(15);
            string hidsjyzm = HttpHelper.MD5Calc("13763" + term + m).ToUpper();
            string gethidyzm = HttpHelper.CreateHttpRequest("http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel.aspx", "get", "", cookies);
            //<input type="hidden" name="hidyzm" value="eaeeab3da91596c811ca97a67c3713z">
            Regex reg = new Regex("<input type=\"hidden\" name=\"hidyzm\" value=\"[0-9a-z]*\">");
            gethidyzm = reg.Match(gethidyzm).ToString();

            gethidyzm = gethidyzm.Substring(42, gethidyzm.Length - 44);


            //Pri_StuSel_Drawimg\.aspx\?[a-z=0-9&]*
            string data = "Sel_XNXQ=" + term + "&rad=0&px=0&txt_yzm=&hidyzm=" + gethidyzm + "&hidsjyzm=" + hidsjyzm;

            string res = HttpHelper.FullCreateHttpRequest("http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel_rpt.aspx?m=" + m, "post", data, "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel.aspx", cookies);

            Regex reg2 = new Regex(@"Pri_StuSel_Drawimg\.aspx\?[a-z=0-9&]*");
            string regres = reg2.Match(res).ToString();
            try
            {
                Image i = HttpHelper.CreateHttpRequestReturnImage("http://jwxt.fjjxu.edu.cn/znpk/" + regres, "jwxt.fjjxu.edu.cn", "http://jwxt.fjjxu.edu.cn/znpk/Pri_StuSel_rpt.aspx?m=" + m, "get", "", cookies);
                SaveImageByStudentClass(i, term, userId);
            }
            catch (Exception)
            {

            }
            //reg2.Match(res).ToString()

            return "ok";
        }

        /// <summary>
        /// 保存课表
        /// </summary>
        /// <param name="i"></param>
        /// <param name="term"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        private bool SaveImageByStudentClass(Image i, string term, string userId)
        {
          
            //判断是否已经存在  
            var stInfo = stuInfoService.LoadEntities(c => c.UserId == userId).FirstOrDefault(); 
            stuImgService.DeleteEntities(c => c.UserInfoId == stInfo.Id && c.Term == term);
            StudentImageTable sit = new StudentImageTable();
            sit.ClassImage = HttpHelper.GetByteImage(i);
            sit.Term = term;
            sit.Mark = "ClassTable";
            sit.ExamImage = HttpHelper.GetByteImage(i);
            stInfo.StudentImageTable.Add(sit);
            return stuInfoService.UpdateEntity(stInfo);
        }

        #endregion


        public ActionResult AddButton()
        {
            return View();
        }

        /// <summary>
        /// 返回校历
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSchoolCalendar()
        {
            return View();
        }
        /// <summary>
        /// 返回课表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStudentClassTable()
        {
            int userInfoId = Convert.ToInt32(Request["userinfoid"]);
            string term = Request["term"];
            var stuimg = stuImgService.LoadEntities(c => c.UserInfoId == userInfoId && c.Term == term).FirstOrDefault();

            if (stuimg != null)
            {
                return File(stuimg.ClassImage, "Image/jpeg");
            }
            else
            {
                return Content("卧槽、你都没课啦？羡煞我也！");
            }
        }

        /// <summary>
        /// 获取所有学生成绩！
        /// </summary>
        /// <returns></returns> 
        public ActionResult GetSutentAllDate(string userId)
        {
            int id = -1;
            if (int.TryParse(userId,out id))
            {
                IBLL.IStudentGradesService stuGradeService = new BLL.StudentGradesService();
                var gralist = stuGradeService.LoadEntities(c => c.UserInfoId == id);
                List<string> termList = new List<string>();
                foreach (StudentGrades sg in gralist)
                {
                    termList.Add(sg.Term);
                }
                termList = termList.Distinct().ToList();
                List<ShowGrade> sgList = new List<ShowGrade>();
                foreach (string t in termList)
                {
                    ShowGrade sgs = new ShowGrade();
                    sgs.GradeDates = new List<GradeDate>();
                    sgs.Term = t;
                    var gra = gralist.Where(c => c.Term == t);
                    foreach (StudentGrades sg in gra)
                    {
                        GradeDate gd = new GradeDate();
                        gd.GradeName = sg.Course.Substring(8, sg.Course.Length - 8);
                        gd.Credit = sg.Credit;
                        gd.Grade = sg.ValidGrade;
                        sgs.GradeDates.Add(gd);
                    }
                    sgList.Add(sgs);
                }

                ViewData["showData"] = sgList;

            }
            return View();
        }
        public ActionResult ShowStudentTable()
        {
            return View();
        }
    }
}