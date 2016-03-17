using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            if (Request.Cookies["users"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("login", "users");
            }
        }
        public ActionResult de()
        {
            return View();
        }
    }
}