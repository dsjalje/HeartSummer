﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.HeartSummer.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        } 
    }
}