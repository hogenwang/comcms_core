using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using COMCMS.Core;
using Microsoft.AspNetCore.Mvc;
using NewLife.Reflection;
using XCode;

namespace COMCMS.Web.Common
{
    public class MenuRegister
    {
        private static void StartRegister()
        {
            if (AdminMenu.Meta.Count > 0)
                return;
            // 操作项定义
            var menuEventDic = new Dictionary<string, KeyValuePair<int, string>>
            {
                { "add",new KeyValuePair<int, string>(1,"添加")},
                { "edit",new KeyValuePair<int, string>(2,"修改")},
                { "del",new KeyValuePair<int, string>(3,"删除")},
                { "view",new KeyValuePair<int, string>(4,"查看")},
                { "viewlist",new KeyValuePair<int, string>(5,"查看列表")},
                { "import",new KeyValuePair<int, string>(6,"导入")},
                { "export",new KeyValuePair<int, string>(7,"导出")},
                { "filter",new KeyValuePair<int, string>(8,"搜索")},
                { "batch",new KeyValuePair<int, string>(9,"批量操作")},
                { "recycle",new KeyValuePair<int, string>(10,"回收站")},
                { "confirm",new KeyValuePair<int, string>(11,"确认")},

            };

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asmItem in asms)
            {
                var types = asmItem.GetTypes().Where(e => e.Name.EndsWith("Controller")).ToList();
                if (types.Count == 0) continue;

                foreach (var type in types)
                {
                    if (!type.Namespace.Contains(".AdminCP.Controller"))
                    {
                        continue;
                    }

                    // 控制器名称
                    var ctrName = type.Name.TrimEnd("Controller");

                    // 控制器展示名
                    var ctrDpName = type.GetDisplayName();

                    var ctrDes = type.GetDescription();

                    var url = ctrName;

                    var fAdminMenu = AdminMenu.Find(AdminMenu._.MenuKey == ctrName + "Sys" & AdminMenu._.Link == "#");
                    var nAdminMenu = new AdminMenu
                    {
                        Description = ctrDes,
                        IsHide = 0,
                        Level = 0,
                        Link = "#",
                        Location = "0,",
                        MenuKey = ctrName + "Sys",
                        MenuName = ctrDpName,
                        PId = 0
                    };
                    var pAdminMenu = fAdminMenu ?? nAdminMenu;
                    if (pAdminMenu.Id > 0)
                    {
                        pAdminMenu.Description = nAdminMenu.Description;
                        pAdminMenu.MenuName = nAdminMenu.MenuName;
                    }

                    // 控制器所有包含MyAuthorizeAttribute的方法
                    var acts = type.GetMethods()
                        .Where(w => w.IsDefined(typeof(MyAuthorizeAttribute)))
                        .ToList();

                    if (!acts.Any()) continue;

                    pAdminMenu.Save();

                    var eventKeyDic = new Dictionary<string, int>();
                    var adminMenuEventList = new List<AdminMenuEvent>();

                    foreach (var method in acts)
                    {
                        var dn = method.GetDisplayName();
                        var methodDes = method.GetDescription();
                        var actName = method.Name;
                        var myAuthorize = method.GetCustomAttribute<MyAuthorizeAttribute>();

                        var menuKey = myAuthorize.GetValue("_menuKey").ToString();
                        var eventKey = myAuthorize.GetValue("_eventKey").ToString();
                        var returnType = myAuthorize.GetValue("_returnType").ToString();

                        //保存菜单
                        if (eventKey == "viewlist" && returnType == "HTML")
                        {
                            var adminMenu = AdminMenu.Find(AdminMenu._.MenuKey == menuKey & AdminMenu._.Link == url + "/" + actName) ?? new AdminMenu
                            {
                                Description = methodDes,
                                IsHide = 0,
                                Level = pAdminMenu.Level + 1,
                                Link = url + "/" + actName,
                                Location = pAdminMenu.Location + ",",
                                MenuKey = menuKey,
                                MenuName = dn,
                                PId = pAdminMenu.Id
                            };
                            adminMenu.Save();

                            eventKeyDic[menuKey] = adminMenu.Id;
                        }

                        // 保存操作
                        var adminmenuevent = AdminMenuEvent.Find(AdminMenuEvent._.MenuId == menuEventDic[eventKey].Key
                                                                 & AdminMenuEvent._.MenuKey == menuKey & AdminMenuEvent._.EventKey == eventKey)
                                             ?? new AdminMenuEvent
                                             {
                                                 MenuKey = menuKey,
                                                 MenuId = menuEventDic[eventKey].Key,
                                                 EventKey = eventKey,
                                                 EventName = menuEventDic[eventKey].Value
                                             };
                        adminMenuEventList.Add(adminmenuevent);
                    }

                    adminMenuEventList.ForEach(f => f.MenuId = eventKeyDic.ContainsKey(f.MenuKey) ? eventKeyDic[f.MenuKey] : 0);
                    adminMenuEventList.Save();
                }

            }
        }

        public static void Register()
        {
            Task.Run(() => { StartRegister(); }).LogException();
        }
    }
}
