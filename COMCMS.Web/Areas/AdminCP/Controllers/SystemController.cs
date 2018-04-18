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
using COMCMS.Core.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class SystemController : AdminBaseController
    {
        private IHostingEnvironment _env;
        public SystemController(IHostingEnvironment env)
        {
            _env = env;
        }

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

        #region SMTP 配置
        [MyAuthorize("view", "smptconfig")]
        public IActionResult SmtpConfig()
        {
            Config cfg = Config.GetSystemConfig();
            SMTPConfigEntity smtp = cfg.SMTPConfigEntity;
            Admin.WriteLogActions("查看SMTP配置；");
            return View(smtp);
        }

        [HttpPost]
        [MyAuthorize( "view", "smptconfig", "JSON")]
        public IActionResult SmtpConfig(SMTPConfigEntity model)
        {
            //验证
            if (!Utils.IsValidEmail(model.SmtpEmail))
            {
                tip.Message = "SMTP帐号格式错误！请输入正确的Email！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(model.SmtpHost))
            {
                tip.Message = "请输入SMTP服务器地址！";
                return Json(tip);
            }
            if (!Utils.IsInt(model.SmtpProt))
            {
                tip.Message = "请输入正确的端口号！";
                return Json(tip);
            }
            string json = JsonConvert.SerializeObject(model);

            OtherConfig oc = OtherConfig.Find(OtherConfig._.ConfigName == "smtp");

            if (oc == null)//如果不存在
            {
                oc = new OtherConfig();
                oc.ConfigName = "smtp";
                oc.ConfigValue = JsonConvert.SerializeObject(model);
                oc.LastUpdateTime = DateTime.Now;
                oc.Insert();
            }
            else
            {
                oc.ConfigValue = JsonConvert.SerializeObject(model);
                oc.LastUpdateTime = DateTime.Now;
                oc.Update();
            }
            Admin.WriteLogActions("修改SMTP设置;");

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改站点SMTP配置成功";

            return Json(tip);
        }

        #endregion

        #region 附件配置
        [MyAuthorize( "view", "attachconfig")]
        public IActionResult AttachConfig()
        {
            Config cfg = Config.GetSystemConfig();
            AttachConfigEntity model = cfg.AttachConfigEntity;
            Admin.WriteLogActions("查看附件配置；");
            return View(model);
        }

        [HttpPost]
        [MyAuthorize("view",  "attachconfig",  "JSON")]
        public IActionResult AttachConfig(AttachConfigEntity model)
        {
            if (!string.IsNullOrWhiteSpace(model.WaterMarkImg))
            {
                string imgPath = _env.WebRootPath + model.WaterMarkImg.Replace("/", "\\");
                NewLife.Log.XTrace.WriteLine(_env.WebRootPath + "|" + Path.Combine(_env.WebRootPath, model.WaterMarkImg) + "|" + imgPath);
                if (!System.IO.File.Exists(imgPath))
                {
                    tip.Message = "水印图片不存在，请重新上传！";
                    return Json(tip);
                }
            }
            string json = JsonConvert.SerializeObject(model);

            OtherConfig oc = OtherConfig.Find(OtherConfig._.ConfigName == "attach");

            if (oc == null)//如果不存在
            {
                oc = new OtherConfig();
                oc.ConfigName = "attach";
                oc.ConfigValue = json;
                oc.LastUpdateTime = DateTime.Now;
                oc.Insert();
            }
            else
            {
                oc.ConfigValue = json;
                oc.LastUpdateTime = DateTime.Now;
                oc.Update();
            }
            Admin.WriteLogActions("修改附件配置;");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改附件配置成功";
            return Json(tip);
        }
        #endregion

        #region robots设置
        [HttpGet]
        [MyAuthorize("view","robots" )]
        public IActionResult Robots()
        {
            //获取文件内容
            string file = Path.Combine(_env.WebRootPath, "robots.txt");
            string content = "";
            if (System.IO.File.Exists(file))
            {
                content = System.IO.File.ReadAllText(file);
            }
            ViewBag.content = content;
            Admin.WriteLogActions("查看Robots.txt;");
            return View();
        }
        [HttpPost]
        [MyAuthorize("edit", "robots", "JSON")]
        public IActionResult Robots(string content)
        {
            string file = Path.Combine(_env.WebRootPath, "robots.txt");
            if (System.IO.File.Exists(file))
            {
                //写入覆盖
                System.IO.File.WriteAllText(file, content);
            }
            else
            {
                System.IO.File.WriteAllText(file, content);
            }
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改Robots.txt成功";
            Admin.WriteLogActions("修改Robots.txt成功;");
            return Json(tip);
        }
        #endregion
    }
}