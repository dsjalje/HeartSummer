using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class WeChatController : Controller
    {
        /// <summary>
        /// 在Action执行之前
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string useragent = Request.UserAgent;

            if (!useragent.Contains("MicroMessenger"))
            {
                Response.Write("请使用微信浏览器！");
                Response.End();
            }

        }

    }
}