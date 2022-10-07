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
using NewLife.Log;
using NewLife;
using System.Runtime.InteropServices;

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

        #region 释放进程内存
        [HttpPost]
        public IActionResult DoMemoryFree()
        {
            Admin my = Admin.GetMyInfo();
            if (my.Roles.IsSuperAdmin == 1)
            {
                try
                {
                    GC.Collect();

                    // 释放当前进程所占用的内存
                    var p = Process.GetCurrentProcess();
                    SetProcessWorkingSetSize(p.Handle, -1, -1);
                }
                catch (Exception ex)
                {
                    XTrace.WriteException(ex);
                    tip.Message = "释放内存执行失败！" + ex.Message;
                    return Json(tip);
                }

                tip.Message = "释放内存成功！";
                tip.Status = JsonTip.SUCCESS;
                return Json(tip);
            }
            else
            {
                tip.Message = "您没有权限执行本操作";
                return Json(tip);
            }
        }

        [DllImport("kernel32.dll")]
        private static extern Boolean SetProcessWorkingSetSize(IntPtr proc, Int32 min, Int32 max);
        #endregion

        #region 获取CPU内存等参数
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult GetMachineInfo()
        {
            Admin my = Admin.GetMyInfo();
            if (my.Roles.IsSuperAdmin == 0)
            {
                tip.Message = "您没有权限执行本操作！";
                return Json(tip);
            }
            var mi = MachineInfo.Current ?? new MachineInfo();
            var process = Process.GetCurrentProcess();

            double CpuRate = Math.Round(mi.CpuRate, 2);
            string AvailableMemory = (mi.AvailableMemory / 1024 / 1024).ToString("n0") + "M";
            decimal memoryUsage = Math.Round((decimal)mi.AvailableMemory / (decimal)mi.Memory * 100M, 2);
            string WorkingSet64 = (process.WorkingSet64 / 1024 / 1024).ToString("n0") + "M";
            string PrivateMemorySize64 = (process.PrivateMemorySize64 / 1024 / 1024).ToString("n0") + "M";
            string GCMemory = (GC.GetTotalMemory(false) / 1024 / 1024).ToString("n0") + "M";

            tip.Status = JsonTip.SUCCESS;
            tip.Detail = new
            {
                cpuRate = CpuRate,
                availableMemory = AvailableMemory,
                memoryUsage = memoryUsage,
                workingSet64 = WorkingSet64,
                privateMemorySize64 = PrivateMemorySize64,
                gCMemory = GCMemory
            };

            tip.Message = "获取成功";
            return Json(tip);
        }
        #endregion
    }
}