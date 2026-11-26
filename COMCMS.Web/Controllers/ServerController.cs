using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Common;

namespace COMCMS.Web.Controllers
{
    /// <summary>
    /// 服务端处理
    /// </summary>
    public class ServerController : HomeBaseController
    {
        #region 提交前台底部留言
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [EnableRateLimiting("guestbook")]
        public IActionResult DoPostMessage(string name, string phone, string content)
        {
            var cleanName = Utils.NoHTML(name ?? string.Empty).Trim();
            var cleanPhone = (phone ?? string.Empty).Trim();
            var cleanContent = Utils.NoHTML(content ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(cleanName) || cleanName.Length > 50)
            {
                tip.Message = "请填写有效的姓名（不超过50字）！";
                return Json(tip);
            }
            if (!Utils.IsTel(cleanPhone))
            {
                tip.Message = "请填写正确的手机号码！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(cleanContent) || cleanContent.Length > 2000)
            {
                tip.Message = "请填写有效的留言内容（不超过2000字）！";
                return Json(tip);
            }

            Guestbook entity = new Guestbook
            {
                AddTime = DateTime.Now,
                IP = Utils.GetIP(),
                KId = 1,
                Nickname = cleanName,
                Content = cleanContent,
                Tel = cleanPhone
            };
            entity.Insert();

            tip.Message = "留言成功，感谢您的留言！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion
    }
}
