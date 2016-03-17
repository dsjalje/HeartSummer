using DS.HeartSummer.BLL;
using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        ITimelineService tlService = new TimelineService();
        ISettingsService setService = new SettingsService();
        IStudentInfoService stuInfoService = new StudentInfoService();
        public ActionResult Index()
        {
            return View();
        }
        private List<CustomMenu> CreateMenus()
        {

            List<CustomMenu> cms = new List<CustomMenu>();
            //菜单 1
            CustomMenu cm = new CustomMenu();
            cm.Name = "😙亲心小夏";
            cm.Type = "view";
            cm.Url = "http://www.codefirst.top/";
            cms.Add(cm);
            //菜单 2
            CustomMenu cm2 = new CustomMenu();
            cm2.Name = "\ue405 福利社";
            List<CustomSubMenu> csms = new List<CustomSubMenu>();
            CustomSubMenu csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "搜搜";
            csm.Url = "http://www.soso.com/";
            csms.Add(csm);
            csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "QQ";
            csm.Url = "http://v.qq.com/";
            csms.Add(csm);
            csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "百度3";
            csm.Url = "http://www.baidu.com/";
            csms.Add(csm);
            csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "百度1";
            csm.Url = "http://www.baidu.com/";
            csms.Add(csm);
            csm = new CustomSubMenu();
            csm.Type = "view";
            csm.Name = "百度2";
            csm.Url = "http://www.baidu.com/";
            csms.Add(csm);
            cm2.Sub_button = csms;
            cms.Add(cm2);
            //菜单 3
            CustomMenu cm3 = new CustomMenu();
            cm3.Name = "点点";
            cm3.Type = "click";
            cm3.Key = "V1001";
            cms.Add(cm3);

            return cms;

        }
        public ActionResult WeChat_CustomMenus()
        {
            ViewData["menus"] = CreateMenus();
            return View();
        }
        public ActionResult WeChat_CustomBasic()
        { 
            string event_click_help = setService.LoadEntities(c => c.Name == "event_click_help").FirstOrDefault().value;
            string default_grade_term = setService.LoadEntities(c => c.Name == "default_grade_term").FirstOrDefault().value;
            string default_class_term = setService.LoadEntities(c => c.Name == "default_class_term").FirstOrDefault().value;
            string event_subscribe = setService.LoadEntities(c => c.Name == "event_subscribe").FirstOrDefault().value;
            ViewData["event_click_help"] = event_click_help;
            ViewData["default_grade_term"] = default_grade_term;
            ViewData["default_class_term"] = default_class_term;
            ViewData["event_subscribe"] = event_subscribe;
            return View();
        }
        [HttpPost]
        public ActionResult WeChat_CustomBasic(string event_click_help, string default_class_term, string default_grade_term,string event_subscribe)
        {
          
            if (event_click_help != null)
            {
                var help = setService.LoadEntities(c => c.Name == "event_click_help").FirstOrDefault();
                help.value = event_click_help;
                if (setService.UpdateEntity(help))
                {
                    return Content("修改成功!");
                }
                
            }
            else if (default_class_term != null)
            {
                var classTerm = setService.LoadEntities(c => c.Name == "default_class_term").FirstOrDefault();
                classTerm.value = default_class_term;
                if (setService.UpdateEntity(classTerm))
                {
                    return Content("修改成功!");
                }
            }
            else if (default_grade_term != null)
            {
                var gradeTerm = setService.LoadEntities(c => c.Name == "default_grade_term").FirstOrDefault();
                gradeTerm.value = default_grade_term;
                if (setService.UpdateEntity(gradeTerm))
                {
                    return Content("修改成功!");
                }
            }
            else if (event_subscribe!=null)
            {
                var eventSub = setService.LoadEntities(c => c.Name == "event_subscribe").FirstOrDefault();
                eventSub.value = event_subscribe;
                if (setService.UpdateEntity(eventSub))
                {
                    return Content("修改成功!");
                }
            }
            else
            {
                return null;
            }
            return Content("修改失败！");
        }

        public ActionResult WeChat_CustomToken()
        {

            string token = setService.LoadEntities(c => c.Name == "token").FirstOrDefault().value;
            ViewData["token"] = token;

            return View();
        }
        public ActionResult Wechat_DataMager()
        {
            List<StudentInfo> slist = stuInfoService.LoadEntities(c => true).ToList();

            ViewData["slist"] = slist;

            return View();
        }
        public ActionResult TimeLine()
        {
            int co = tlService.LoadEntities(c => true).Count();
            ViewData["tl"] = (List<Timeline>)tlService.LoadEntities(c => true).OrderByDescending(c => c.id).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult AddTimeLine(Timeline tl)
        {

            if (string.IsNullOrEmpty(tl.title) || string.IsNullOrEmpty(tl.contant))
            {
                return Content("数据为空！");
            }
            else if (tlService.AddTimeLine(tl))
            {
                return Content("0");
            }
            else
            {
                return Content("添加失败");
            }

        }
        public ActionResult _index()
        {
            return View();
        }
    }
}