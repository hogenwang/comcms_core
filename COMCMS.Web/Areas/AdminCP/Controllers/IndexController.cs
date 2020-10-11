using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using System.Diagnostics;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class IndexController : AdminBaseController
    {
        private readonly IWebHostEnvironment _env;
        public IndexController(IWebHostEnvironment env)
        {
            _env = env;
        }
        #region 后台首页
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!Core.Admin.IsAdminLogin())
            {
                return RedirectToAction("Index", "Login");
            }

            Admin admin = Admin.GetMyInfo();
            ViewBag.admin = admin;
            //获取菜单
            List<AdminMenu> list = new List<AdminMenu>();
            //这里需要获取权限，暂时先所有
            if (admin.Roles.IsSuperAdmin == 1)
            {
                list = AdminMenu.GetListTree(0, -1, false, true).ToList();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<AdminMenu>>(admin.Roles.Menus);
            }

            return View(list);
        }
        #endregion

        #region 后台主界面
        public IActionResult Main()
        {
            string remoteip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string host = Request.HttpContext.Request.Host.Host;
            string port = Request.HttpContext.Connection.RemotePort.ToString();

            ViewBag.remoteip = remoteip;
            ViewBag.port = port;
            ViewBag.host = host;
            ViewBag.contentPath = _env.ContentRootPath;
            ViewBag.rootPath = _env.WebRootPath;

            Admin admin = Admin.GetMyInfo();
            ViewBag.admin = admin;

            return View();
        }
        #endregion

        #region 显示没权限
        [AllowAnonymous]
        //显示没有权限
        public IActionResult NotAuthorize()
        {
            return View();
        }
        #endregion
    }
}