using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Core;
using XCode;
using NewLife.Log;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Common
{
    /// <summary>
    /// 后台授权过滤器
    /// </summary>
    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// 菜单key
        /// </summary>
        private readonly string _menuKey;
        /// <summary>
        /// 操作key
        /// </summary>
        private readonly string _eventKey;
        /// <summary>
        /// 返回类型 HTML 和 JSON
        /// </summary>
        private readonly string _returnType;
        public MyAuthorizeAttribute(string EventKey, string MenuKey, string returnType = "HTML")
        {
            _menuKey = MenuKey;
            _eventKey = EventKey;
            _returnType = !string.IsNullOrWhiteSpace(returnType) && returnType.ToLower() == "json" ? "JSON" : "HTML";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isOK = false;
            if (Admin.IsAdminLogin())
            {
                Admin my = Core.Admin.GetMyInfo();
                if (my.Roles.IsSuperAdmin == 1)
                {
                    isOK = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(my.Roles.Powers))
                    {
                        IList<AdminMenuEvent> listevents = JsonConvert.DeserializeObject<IList<AdminMenuEvent>>(my.Roles.Powers);
                        if (listevents != null && listevents.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_menuKey) && !string.IsNullOrEmpty(_eventKey))
                            {

                                if (listevents.FirstOrDefault(s => s.EventKey == _eventKey && s.MenuKey == _menuKey) != null)
                                {
                                    isOK = true;
                                }
                                else
                                {
                                    XTrace.WriteLine($"没有找到菜单！EventKey:{_eventKey};MenuKey:{_menuKey}");
                                }
                            }
                        }
                    }
                }
                //没有权限
                if (!isOK)
                {
                    XTrace.WriteLine($"没有权限菜单！EventKey:{_eventKey};MenuKey:{_menuKey}");
                    if (_returnType == "JSON")
                    {
                        JsonTip tip = new JsonTip()
                        {
                            Id = 0,
                            Message = "您没有权限执行此操作！",
                            ReturnUrl = "/AdminCP/Login"
                        };
                        context.Result = new JsonResult(tip);
                        return;
                    }
                    else
                    {
                        context.Result = new RedirectResult("/AdminCP/Index/NotAuthorize");
                        return;
                    }
                }
            }
            //base.OnResultExecuting(context);
        }
    }
}
