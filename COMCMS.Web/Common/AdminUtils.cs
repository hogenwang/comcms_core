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

        #region 判断文章栏目或者商品栏目，栏目路径是否已经存在
        /// <summary>
        /// 判断路径名称是否合法
        /// </summary>
        /// <param name="pathname"></param>
        /// <param name="kid">id，大于0是修改</param>
        /// <param name="typeid">类型，0文章，1商品</param>
        /// <returns></returns>
        public static bool CheckFilePathIsOK(string pathname, int kid, int typeid)
        {
            bool flag = true;

            if (string.IsNullOrEmpty(pathname))
            {
                return false;
            }
            if(typeid ==0)//文章
            {
                if (ArticleCategory.FindCount(ArticleCategory._.FilePath == pathname & ArticleCategory._.Id != kid, null, null, 0, 0) > 0)
                {
                    return false;
                }
                if (Category.FindCount(Category._.FilePath == pathname, null, null, 0, 0) > 0)
                {
                    return false;
                }
            }
            else if(typeid == 1)
            {
                if (ArticleCategory.FindCount(ArticleCategory._.FilePath == pathname , null, null, 0, 0) > 0)
                {
                    return false;
                }
                if (Category.FindCount(Category._.FilePath == pathname & Category._.Id != kid, null, null, 0, 0) > 0)
                {
                    return false;
                }
            }

            return flag;
        }
        #endregion

    }
}
