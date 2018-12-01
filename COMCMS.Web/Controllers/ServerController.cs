using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using XCode;

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
        public IActionResult DoPostMessage(string name,string phone,string content)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                tip.Message = "请填写您的姓名！";
                return Json(tip);
            }
            if (!Utils.IsTel(phone))
            {
                tip.Message = "请填写正确的手机号码！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                tip.Message = "请填写您的留言内容！";
                return Json(tip);
            }

            Guestbook entity = new Guestbook()
            {
                AddTime = DateTime.Now,
                IP = Utils.GetIP(),
                KId = 1,
                Nickname = name,
                Content = Utils.NoHTML(content),
                Tel = phone
            };
            entity.Insert();

            tip.Message = "留言成功，感谢您的留言！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion
    }
}