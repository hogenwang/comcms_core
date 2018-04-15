using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class SystemController : AdminBaseController
    {
        #region 系统基本设置
        [MyAuthorize("view", "baseconfig")]
        public IActionResult BaseConfig()
        {
            Config cfg = Config.GetSystemConfig();
            Core.Admin.WriteLogActions("查看基本配置；");
            return View(cfg);
        }

        [HttpPost]
        [MyAuthorize("view", "baseconfig", "JSON")]
        public IActionResult BaseConfig(Config model)
        {
            Config cfg = Config.GetSystemConfig();
            if (string.IsNullOrEmpty(model.SiteName))
            {
                tip.Message = "请输入站点名称！";
                return Json(tip);
            }
            string IsResetData = Request.Form["IsResetData"];
            if (IsResetData != "1") IsResetData = "0";

            model.Id = cfg.Id;
            model.LastCacheTime = DateTime.Now;
            model.LastUpdateTime = DateTime.Now;
            model.IsResetData = int.Parse(IsResetData);
            model.Update();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改站点基本配置成功";

            Core.Admin.WriteLogActions("修改站点基本配置；");

            return Json(tip);
        }

        #endregion
    }
}