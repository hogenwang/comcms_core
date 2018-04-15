using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Web.Models;
using COMCMS.Common;
using COMCMS.Core;
using XCode;

namespace COMCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            CookiesHelper.WriteCookie("abc", Utils.GetRandomChar(20), 10);
            //sessiong
            SessionHelper.WriteSession("bcd", Utils.GetRandomChar(20));
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            Config cfg = Config.FindById(1);
            ViewBag.siteName = cfg.SiteName;

            ViewBag.c = CookiesHelper.GetCookie("abc");
            ViewBag.d = Utils.SIGNSALT;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
