using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using COMCMS.Common;
using COMCMS.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Log;
using Newtonsoft.Json;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class IndexController : AdminBaseController
    {
        private readonly IWebHostEnvironment _env;

        static IndexController()
        {
            MachineInfo.RegisterAsync();
        }

        public IndexController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region 后台首页
        public IActionResult Index()
        {
            Admin admin = Admin.GetMyInfo();
            ViewBag.admin = admin;
            List<AdminMenu> list;
            if (admin?.Roles?.IsSuperAdmin == 1)
            {
                list = AdminMenu.GetListTree(0, -1, false, true).ToList();
            }
            else
            {
                list = string.IsNullOrWhiteSpace(admin?.Roles?.Menus)
                    ? new List<AdminMenu>()
                    : (JsonConvert.DeserializeObject<List<AdminMenu>>(admin.Roles.Menus) ?? new List<AdminMenu>());
            }

            return View(list);
        }
        #endregion

        #region 后台主界面
        public IActionResult Main()
        {
            string remoteip = Utils.GetIP();
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
            if (my?.Roles?.IsSuperAdmin != 1)
            {
                tip.Message = "您没有权限执行本操作";
                return Json(tip);
            }

            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                if (OperatingSystem.IsWindows())
                {
                    var p = Process.GetCurrentProcess();
                    SetProcessWorkingSetSize(p.Handle, -1, -1);
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                tip.Message = $"释放内存执行失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }

            tip.Message = "释放内存成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
        #endregion

        #region 获取CPU内存等参数
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult GetMachineInfo()
        {
            Admin my = Admin.GetMyInfo();
            if (my?.Roles?.IsSuperAdmin != 1)
            {
                tip.Message = "您没有权限执行本操作！";
                return Json(tip);
            }
            var mi = MachineInfo.Current ?? new MachineInfo();
            var process = Process.GetCurrentProcess();

            double cpuRate = Math.Round(mi.CpuRate, 2);
            string availableMemory = (mi.AvailableMemory / 1024 / 1024).ToString("n0") + "M";
            decimal memoryUsage = mi.Memory > 0
                ? Math.Round((decimal)mi.AvailableMemory / (decimal)mi.Memory * 100M, 2)
                : 0M;
            string workingSet64 = (process.WorkingSet64 / 1024 / 1024).ToString("n0") + "M";
            string privateMemorySize64 = (process.PrivateMemorySize64 / 1024 / 1024).ToString("n0") + "M";
            string gcMemory = (GC.GetTotalMemory(false) / 1024 / 1024).ToString("n0") + "M";

            tip.Status = JsonTip.SUCCESS;
            tip.Detail = new
            {
                cpuRate,
                availableMemory,
                memoryUsage,
                workingSet64,
                privateMemorySize64,
                gCMemory = gcMemory
            };
            tip.Message = "获取成功";
            return Json(tip);
        }
        #endregion
    }
}
