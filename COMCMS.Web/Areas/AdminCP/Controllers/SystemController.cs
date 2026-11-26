using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    public class SystemController : AdminBaseController
    {
        private static readonly Regex SafeAttachPatchRegex = new(@"^[a-zA-Z0-9_-]{1,32}$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));
        private readonly IWebHostEnvironment _env;

        public SystemController(IWebHostEnvironment env)
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
        [MyAuthorize("edit", "baseconfig", "JSON")]
        public IActionResult BaseConfig(BaseConfigUpdateModel model)
        {
            Config cfg = Config.GetSystemConfig();
            if (cfg == null)
            {
                tip.Message = "系统配置记录不存在！";
                return Json(tip);
            }
            if (model == null || string.IsNullOrWhiteSpace(model.SiteName))
            {
                tip.Message = "请输入站点名称！";
                return Json(tip);
            }
            if (!string.IsNullOrWhiteSpace(model.SiteLogo) && !IsSafeWebRootAssetPath(model.SiteLogo))
            {
                tip.Message = "站点 LOGO 地址格式不合法！";
                return Json(tip);
            }

            string isResetData = Request.Form["IsResetData"];
            string isCloseSite = Request.Form["IsCloseSite"];

            // 显式白名单更新字段，杜绝 Config 实体批量赋值
            cfg.SiteName = model.SiteName.Trim();
            cfg.SiteUrl = model.SiteUrl?.Trim() ?? string.Empty;
            cfg.SiteLogo = model.SiteLogo?.Trim() ?? string.Empty;
            cfg.SiteEmail = model.SiteEmail?.Trim() ?? string.Empty;
            cfg.SiteTel = model.SiteTel?.Trim() ?? string.Empty;
            cfg.Copyright = model.Copyright ?? string.Empty;
            cfg.IsCloseSite = isCloseSite == "1" || string.Equals(isCloseSite, "true", StringComparison.OrdinalIgnoreCase);
            cfg.CloseReason = model.CloseReason ?? string.Empty;
            cfg.IsResetData = isResetData == "1" ? 1 : 0;
            cfg.Keyword = model.Keyword ?? string.Empty;
            cfg.Description = model.Description ?? string.Empty;
            cfg.IndexTitle = model.IndexTitle ?? string.Empty;
            cfg.OnlineQQ = model.OnlineQQ ?? string.Empty;
            cfg.OfficialName = model.OfficialName ?? string.Empty;
            cfg.OfficialOriginalId = model.OfficialOriginalId ?? string.Empty;
            cfg.WexinAccount = model.WexinAccount ?? string.Empty;
            cfg.Token = model.Token?.Trim() ?? string.Empty;
            cfg.AppId = model.AppId?.Trim() ?? string.Empty;
            cfg.AppSecret = model.AppSecret?.Trim() ?? string.Empty;
            cfg.EncodingAESKey = model.EncodingAESKey?.Trim() ?? string.Empty;
            cfg.WXAppId = model.WXAppId?.Trim() ?? string.Empty;
            cfg.WXAppSecret = model.WXAppSecret?.Trim() ?? string.Empty;
            cfg.MCHId = model.MCHId?.Trim() ?? string.Empty;
            cfg.MCHKey = model.MCHKey?.Trim() ?? string.Empty;
            cfg.DefaultFare = Math.Max(0m, model.DefaultFare);
            cfg.MaxFreeFare = Math.Max(0m, model.MaxFreeFare);
            cfg.LastCacheTime = DateTime.Now;
            cfg.LastUpdateTime = DateTime.Now;
            cfg.Update();

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
        [MyAuthorize("edit", "smptconfig", "JSON")]
        public IActionResult SmtpConfig(SMTPConfigEntity model)
        {
            if (model == null || !Utils.IsValidEmail(model.PostUserName))
            {
                tip.Message = "SMTP帐号格式错误！请输入正确的Email！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.SmtpHost))
            {
                tip.Message = "请输入SMTP服务器地址！";
                return Json(tip);
            }
            if (!int.TryParse(model.SmtpProt, out var port) || port < 1 || port > 65535)
            {
                tip.Message = "请输入正确的端口号！";
                return Json(tip);
            }

            string json = JsonConvert.SerializeObject(model);
            OtherConfig oc = OtherConfig.Find(OtherConfig._.ConfigName == "smtp");
            if (oc == null)
            {
                oc = new OtherConfig
                {
                    ConfigName = "smtp",
                    ConfigValue = json,
                    LastUpdateTime = DateTime.Now
                };
                oc.Insert();
            }
            else
            {
                oc.ConfigValue = json;
                oc.LastUpdateTime = DateTime.Now;
                oc.Update();
            }
            Admin.WriteLogActions("修改SMTP设置;");

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改站点SMTP配置成功";
            return Json(tip);
        }

        /// <summary>
        /// 发送测试
        /// </summary>
        [HttpPost]
        [MyAuthorize("edit", "smptconfig", "JSON")]
        public IActionResult SendTestMail()
        {
            string mail = Request.Form["txtEmail"];
            Config cfg = Config.GetSystemConfig();
            if (string.IsNullOrWhiteSpace(mail) || !Utils.IsValidEmail(mail))
            {
                tip.Message = "请输入正确的邮箱格式！";
                return Json(tip);
            }

            var smtp = cfg.SMTPConfigEntity;
            if (string.IsNullOrEmpty(smtp.SmtpHost) || string.IsNullOrEmpty(smtp.SmtpEmail) || string.IsNullOrEmpty(smtp.SmtpEmailPwd) ||
                !int.TryParse(smtp.SmtpProt, out var port) || port < 1 || port > 65535)
            {
                tip.Message = "系统SMTP配置错误，请先配置后再执行此操作！";
                return Json(tip);
            }

            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(smtp.PostUserName, smtp.SmtpEmail));
                emailMessage.To.Add(new MailboxAddress("", mail));
                emailMessage.Subject = "发送测试邮件";
                emailMessage.Body = new TextPart("html") { Text = $"<p>发送测试邮件，发送时间：{DateTime.Now:yyyy-MM-dd HH:mm}</p>" };
                using (var client = new SmtpClient())
                {
                    client.Connect(smtp.SmtpHost, port, smtp.IsSSL == 1);
                    client.Authenticate(smtp.SmtpEmail, smtp.SmtpEmailPwd);
                    client.Send(new MailKit.SendRequest(emailMessage));
                    client.Disconnect(true);
                }
                tip.Status = JsonTip.SUCCESS;
                tip.Message = "发送成功，请查收！";
                Admin.WriteLogActions($"发送测试Email（to:{mail}）;");
                return Json(tip);
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = $"测试邮件发送失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }
        }
        #endregion

        #region 附件配置
        [MyAuthorize("view", "attachconfig")]
        public IActionResult AttachConfig()
        {
            Config cfg = Config.GetSystemConfig();
            AttachConfigEntity model = cfg.AttachConfigEntity;
            Admin.WriteLogActions("查看附件配置；");
            return View(model);
        }

        [HttpPost]
        [MyAuthorize("edit", "attachconfig", "JSON")]
        public IActionResult AttachConfig(AttachConfigEntity model)
        {
            if (model == null)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }

            var attachPatch = (model.AttachPatch ?? "userfiles").Trim().Trim('/', '\\');
            if (!IsSafeAttachPatch(attachPatch))
            {
                tip.Message = "附件存放目录仅允许 1-32 位字母、数字、下划线或连字符，禁止包含路径分隔符或上级目录！";
                return Json(tip);
            }
            model.AttachPatch = attachPatch;

            if (!string.IsNullOrWhiteSpace(model.WaterMarkImg))
            {
                if (!TryResolveWebRootFile(model.WaterMarkImg, out var imgPath) ||
                    !Utils.IsImgFilename(imgPath) ||
                    !System.IO.File.Exists(imgPath))
                {
                    tip.Message = "水印图片路径非法或不存在，请重新上传！";
                    return Json(tip);
                }
            }

            string json = JsonConvert.SerializeObject(model);
            OtherConfig oc = OtherConfig.Find(OtherConfig._.ConfigName == "attach");
            if (oc == null)
            {
                oc = new OtherConfig
                {
                    ConfigName = "attach",
                    ConfigValue = json,
                    LastUpdateTime = DateTime.Now
                };
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
        [MyAuthorize("view", "robots")]
        public IActionResult Robots()
        {
            string file = Path.Combine(_env.WebRootPath, "robots.txt");
            string content = "";
            if (System.IO.File.Exists(file))
            {
                try
                {
                    content = System.IO.File.ReadAllText(file);
                }
                catch (Exception ex)
                {
                    NewLife.Log.XTrace.WriteException(ex);
                }
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
            try
            {
                System.IO.File.WriteAllText(file, content ?? string.Empty);
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = $"修改Robots.txt失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改Robots.txt成功";
            Admin.WriteLogActions("修改Robots.txt成功;");
            return Json(tip);
        }
        #endregion

        internal static bool IsSafeAttachPatch(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && SafeAttachPatchRegex.IsMatch(value);
        }

        private bool IsSafeWebRootAssetPath(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
            return TryResolveWebRootFile(value, out _);
        }

        private bool TryResolveWebRootFile(string relativeUrl, out string fullPath)
        {
            fullPath = null;
            if (string.IsNullOrWhiteSpace(relativeUrl) || string.IsNullOrWhiteSpace(_env.WebRootPath))
                return false;
            try
            {
                var webRoot = Path.GetFullPath(_env.WebRootPath);
                var normalized = relativeUrl.Trim().TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
                if (Path.IsPathRooted(normalized)) return false;
                var candidate = Path.GetFullPath(Path.Combine(webRoot, normalized));
                var relative = Path.GetRelativePath(webRoot, candidate);
                if (Path.IsPathRooted(relative) || relative == ".." || relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                    return false;
                fullPath = candidate;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public sealed class BaseConfigUpdateModel
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string SiteLogo { get; set; }
        public string SiteEmail { get; set; }
        public string SiteTel { get; set; }
        public string Copyright { get; set; }
        public string CloseReason { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string IndexTitle { get; set; }
        public string OnlineQQ { get; set; }
        public string OfficialName { get; set; }
        public string OfficialOriginalId { get; set; }
        public string WexinAccount { get; set; }
        public string Token { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string EncodingAESKey { get; set; }
        public string WXAppId { get; set; }
        public string WXAppSecret { get; set; }
        public string MCHId { get; set; }
        public string MCHKey { get; set; }
        public decimal DefaultFare { get; set; }
        public decimal MaxFreeFare { get; set; }
    }
}
