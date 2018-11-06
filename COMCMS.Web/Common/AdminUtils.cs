using COMCMS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using XCode;
using NewLife.Log;

namespace COMCMS.Web.Common
{
    public class AdminUtils
    {
        #region 检查管理员有没有管理权限
        /// <summary>
        /// 检查管理员有没有管理权限
        /// </summary>
        /// <param name="eventKey">动作，如add、edit</param>
        /// <param name="menuKey">菜单名称，如article、product</param>
        /// <returns></returns>
        public static bool CheckAdminPower(string eventKey, string menuKey)
        {
            if (!Admin.IsAdminLogin())
            {
                return false;
            }

            //判断权限
            Admin my = Admin.GetMyInfo();
            if (my.Roles.IsSuperAdmin != 1)
            {
                //获取所有的菜单权限
                if (string.IsNullOrEmpty(my.Roles.Powers))
                    return false;
                IList<AdminMenuEvent> listevents = JsonConvert.DeserializeObject<IList<AdminMenuEvent>>(my.Roles.Powers);
                if (listevents == null || listevents.Count <= 0)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(menuKey) && !string.IsNullOrEmpty(eventKey))
                {
                    if (listevents.FirstOrDefault(s => s.EventKey == eventKey && s.MenuKey == menuKey) == null)
                    {
                        //获取用户信息
                        Admin he = Admin.GetMyInfo();
                        string path = MyHttpContext.Current.Request.Path;
                        XTrace.WriteLine($"管理员：{he.UserName}访问地址：{path}没有找到权限！EventKey:{eventKey};MenuKey:{menuKey}");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

    }
}
