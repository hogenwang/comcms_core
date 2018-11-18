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
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using COMCMS.Web.Common;

namespace COMCMS.Web.Controllers
{
    public class HomeController : HomeBaseController
    {
        #region 首页
        public IActionResult Index()
        {
            ViewBag.cfg = cfg;
            ViewBag.about = Article.FindById(1);//关于我们
            return View();
        }
        #endregion


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
