using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 在Action执行之前
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (Session["Users"]==null)
            {
                Response.Redirect("~/users/login");
            } 
            
        }

        ///// <summary>
        ///// 在Action执行之后
        ///// </summary>
        ///// <param name="filterContext"></param>
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{

        //} 

        ///// <summary>
        ///// 在Result执行之后 
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{ 

        //}

        ///// <summary>
        ///// 在Result执行之前
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{ 

        //}


    }
}