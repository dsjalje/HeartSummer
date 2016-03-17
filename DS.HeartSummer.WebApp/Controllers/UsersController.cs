using DS.HeartSummer.BLL;
using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: User
        IBLL.IUsersService userService = new UsersService();
        public ActionResult Index()
        {
            return Redirect("~/users/login");
        }
        public ActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            if (CheckValidate())
            {
                string msg = string.Empty;
                if (this.userService.AddEntity(user, out msg) != null)
                {
                    //成功了
                    return Content("1");
                    //return RedirectToAction("Index", new { Msg = Server.UrlEncode(msg), txt = Server.UrlEncode("首页"), Url = "/Home/Index" });
                }
                else
                {
                    //失败
                    //ViewData["msg"] = msg;
                    return Content("-1");
                    //return View("Register");
                }
            }
            else
            {
                //ViewData["msg"] = "<script>code fails</script>";
                return Content("0");
                //return View("Register");
            }
        }
        /// <summary>
        /// 效验验证码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool CheckValidate()
        {
            //判断空值
            if (Session["validateCode"] != null)
            {
                string sysCode = Session["validateCode"].ToString();
                string txtCode = Request["validateCode"];
                if (sysCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    //清除验证码、避免漏洞
                    Session["validateCode"] = null;
                    return true;
                }
            }
            return false;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users u)
        {
            if (!string.IsNullOrEmpty(u.LoginId) || !string.IsNullOrEmpty(u.LoginPwd))
            {

                Users sysu = userService.LoadEntities(c => c.LoginId == u.LoginId).FirstOrDefault();
                if (sysu!=null&&sysu.LoginPwd==u.LoginPwd)
                {
                    //成功
                    Session["Users"] = sysu;
                    return Content("0");
                }
                else
                {
                    //失败
                    return Content("用户名或密码错误！！");
                }
            }
            //空值
            else
            {
                return Content("数据不能为空！");
            }

        }
        public ActionResult ValidateCode()
        {
            Common.ValidateCode_pj vc = new DS.HeartSummer.Common.ValidateCode_pj();
            string code = vc.CreateRandomCode(7);
            byte[] s = vc.CreateImage(code);
            Session["validateCode"] = code;
            return File(s, "image/gif");
        }

        public ActionResult Exit()
        {
            if (Session["Users"]!=null)
            {
                Session.Clear();
            }
            return RedirectToAction("/");
        }
    }
}