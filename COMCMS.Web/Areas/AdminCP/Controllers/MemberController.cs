using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.AspNetCore.Http;
using System.Web;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    /// <summary>
    /// 后台用户管理控制器
    /// </summary>
    [DisplayName("管理员")]
    public class MemberController : AdminBaseController
    {
        #region 修改密码
        //修改个人信息
        [MyAuthorize("view", "editme")]
        public IActionResult EditMe()
        {
            Core.Admin my = Core.Admin.GetMyInfo();
            Core.Admin.WriteLogActions("查看/编辑我的信息;");
            return View(my);
        }
        [HttpPost]
        [MyAuthorize("view", "editme", "JSON")]
        public IActionResult EditMe(IFormCollection fc)
        {
            Core.Admin my = Core.Admin.GetMyInfo();

            string userName = fc["UserName"];
            string oldPwd = fc["txtOldPwd"];
            string newPwd = fc["txtNewPwd"];
            string renewPwd = fc["txtreNewPwd"];
            string realname = fc["RealName"];
            string tel = fc["Tel"];
            string email = fc["Email"];
            string editor = fc["Editor"];
            if (!Utils.IsInt(editor)) editor = "0";
            //判断
            if (string.IsNullOrWhiteSpace(userName))
            {
                tip.Message = "用户名不能为空！";
                return Json(tip);
            }
            userName = userName.Trim();
            if (Utils.GetStringLength(userName) < 5)
            {
                tip.Message = "用户名不能小于5个字符！";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(email) && !Utils.IsValidEmail(email))
            {
                tip.Message = "请填写正确的Email地址！";
                return Json(tip);
            }

            if (userName != my.UserName)//修改用户名
            {
                if (Core.Admin.FindCount(Core.Admin._.UserName == userName.Trim() & Core.Admin._.Id != my.Id, null, null, 0, 0) > 0)
                {
                    tip.Message = "新用户名在已经存在，请选择其他用户名！";
                    return Json(tip);
                }
                my.UserName = userName.Trim();
            }

            if (!string.IsNullOrEmpty(newPwd))
            {
                //修改密码的情况
                if (string.IsNullOrWhiteSpace(oldPwd) || oldPwd.Length < 5)
                {
                    tip.Message = "您修改密码，旧密码不能为空！";
                    return Json(tip);
                }
                if (newPwd.Length < 5)
                {
                    tip.Message = "新密码不能小于5个字符！";
                    return Json(tip);
                }
                if (newPwd != renewPwd)
                {
                    tip.Message = "您输入的两次密码不一样！";
                    return Json(tip);
                }
                //判断旧密码是否正确
                if (my.PassWord != Utils.MD5(my.Salt + oldPwd.Trim()))
                {
                    tip.Message = "您输入的旧密码不正确，请重新输入！";
                    return Json(tip);
                }
                my.PassWord = Utils.MD5(my.Salt + newPwd);
            }
            tip.Message = "测试版暂时屏蔽修改密码，敬请原谅！";
            return Json(tip);
            //my.Tel = tel;
            //my.Email = email;
            //my.RealName = realname;
            //my.Update();
            //Core.Admin.WriteLogActions("修改我的信息;");
            //tip.Status = JsonTip.SUCCESS;
            //tip.Message = "编辑我的信息成功！";

            //return Json(tip);
        }
        #endregion

        #region 管理组管理
        [MyAuthorize("viewlist", "adminrole")]
        [DisplayName("管理组")]
        public IActionResult AdminRole()
        {
            IList<AdminRoles> list = AdminRoles.FindAll(AdminRoles._.Id > 0, AdminRoles._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看管理组列表;");
            return View(list);
        }
        //添加管理组
        [MyAuthorize("add", "adminrole")]
        public IActionResult AddAdminRole()
        {
            //获取所有的菜单列表
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, false);
            ViewBag.MenuList = list;
            //获取所有文章 商品栏目
            IList<ArticleCategory> aclist = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.aclist = aclist;

            IList<Category> pclist = Category.GetListTree(0, -1, true, true);
            ViewBag.pclist = pclist;

            Core.Admin.WriteLogActions("查看添加管理组页面;");
            return View();
        }
        //执行添加管理组
        [HttpPost]
        [MyAuthorize("add", "adminrole", "JSON")]
        public IActionResult AddAdminRole(IFormCollection fc)
        {
            string RoleName = fc["RoleName"];
            string RoleDescription = fc["RoleDescription"];
            string IsSuperAdmin = fc["IsSuperAdmin"];
            string NotAllowDel = fc["NotAllowDel"];
            string OnlyEditMyselfArticle = fc["OnlyEditMyselfArticle"];
            string OnlyEditMyselfProduct = fc["OnlyEditMyselfProduct"];

            if (string.IsNullOrEmpty(OnlyEditMyselfArticle) || !Utils.IsInt(OnlyEditMyselfProduct)) OnlyEditMyselfProduct = "0";
            if (string.IsNullOrEmpty(OnlyEditMyselfProduct) || !Utils.IsInt(OnlyEditMyselfProduct)) OnlyEditMyselfProduct = "0";


            if (string.IsNullOrEmpty(RoleName))
            {
                tip.Message = "管理组名称不能为空！";
                return Json(tip);
            }

            AdminRoles entity = new AdminRoles();

            entity.RoleName = RoleName;
            entity.RoleDescription = RoleDescription;
            entity.IsSuperAdmin = int.Parse(IsSuperAdmin);
            entity.NotAllowDel = !string.IsNullOrEmpty(NotAllowDel) && NotAllowDel == "1" ? 1 : 0;
            entity.OnlyEditMyselfArticle = int.Parse(OnlyEditMyselfArticle);
            entity.OnlyEditMyselfProduct = int.Parse(OnlyEditMyselfProduct);

            //处理权限
            if (entity.IsSuperAdmin == 1)
            {
                entity.Powers = "";
                entity.Menus = "";
            }
            else
            {
                //第一步，获取菜单
                string[] menuids = Request.Form["menuid"];
                //获取所有的菜单列表
                IList<AdminMenu> alllist = AdminMenu.GetListTree(0, -1, false, false);
                IList<AdminMenu> list = new List<AdminMenu>();
                IList<AdminMenuEvent> listevents = new List<AdminMenuEvent>();
                if (menuids != null && menuids.Length > 0)
                {
                    foreach (string s in menuids)
                    {
                        if (Utils.IsInt(s) && alllist.FirstOrDefault(v => v.Id == int.Parse(s)) != null)
                        {
                            AdminMenu tmp = alllist.FirstOrDefault(a => a.Id == int.Parse(s)).CloneEntity();
                            list.Add(tmp);
                            //处理详细权限  详细权限，每一行，则每一个菜单的详细权限，则同一个name
                            string[] eventids = Request.Form["EventKey_" + s];
                            if (eventids != null && eventids.Length > 0)
                            {
                                foreach (var item in eventids)
                                {
                                    if (Utils.IsInt(item))
                                    {
                                        AdminMenuEvent model = AdminMenuEvent.Find(AdminMenuEvent._.Id == int.Parse(item));
                                        if (model != null)
                                        {
                                            listevents.Add(model);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //序列化成json
                    if (list != null && list.Count > 0)
                    {
                        entity.Menus = JsonConvert.SerializeObject(list);
                    }
                    if (listevents != null && listevents.Count > 0)
                    {
                        entity.Powers = JsonConvert.SerializeObject(listevents);
                    }
                }
            }

            //处理文章和商品栏目权限
            string[] acpower = Request.Form["acpower"];
            string[] pcpower = Request.Form["pcpower"];

            List<int> acids = new List<int>();
            List<int> pcids = new List<int>();
            foreach (var item in acpower)
            {
                if (!string.IsNullOrEmpty(item) && Utils.IsInt(item))
                {
                    acids.Add(int.Parse(item));
                }
            }
            foreach (var item in pcpower)
            {
                if (!string.IsNullOrEmpty(item) && Utils.IsInt(item))
                {
                    pcids.Add(int.Parse(item));
                }
            }

            entity.AuthorizedArticleCagegory = JsonConvert.SerializeObject(acids);
            entity.AuthorizedCagegory = JsonConvert.SerializeObject(pcids);

            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加管理组成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新管理组（{entity.Id}）;");

            return Json(tip);
        }

        //查看编辑管理组
        [MyAuthorize("edit", "adminrole")]
        public IActionResult EditAdminRole(int id)
        {
            AdminRoles entity = AdminRoles.Find(AdminRoles._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！", 0, true, "");
            }
            if (string.IsNullOrEmpty(entity.Powers))
                entity.Powers = "[]";
            if (string.IsNullOrEmpty(entity.Menus))
                entity.Menus = "[]";
            if (string.IsNullOrEmpty(entity.AuthorizedArticleCagegory))
                entity.AuthorizedArticleCagegory = "[]";
            if (string.IsNullOrEmpty(entity.AuthorizedCagegory))
                entity.AuthorizedCagegory = "[]";
            //获取所有的菜单列表
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, false);

            //获取所有文章 商品栏目
            IList<ArticleCategory> aclist = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.aclist = aclist;

            IList<Category> pclist = Category.GetListTree(0, -1, true, true);
            ViewBag.pclist = pclist;

            ViewBag.MenuList = list;
            Core.Admin.WriteLogActions($"查看管理组（{id}）详情;");
            return View(entity);
        }
        //执行编辑管理组
        [HttpPost]
        [MyAuthorize("edit", "adminrole", "JSON")]
        public IActionResult EditAdminRole(IFormCollection fc)
        {
            string Id = fc["Id"];
            string RoleName = fc["RoleName"];
            string RoleDescription = fc["RoleDescription"];
            string IsSuperAdmin = fc["IsSuperAdmin"];
            string NotAllowDel = fc["NotAllowDel"];
            string OnlyEditMyselfArticle = fc["OnlyEditMyselfArticle"];
            string OnlyEditMyselfProduct = fc["OnlyEditMyselfProduct"];

            if (string.IsNullOrEmpty(OnlyEditMyselfArticle) || !Utils.IsInt(OnlyEditMyselfProduct)) OnlyEditMyselfProduct = "0";
            if (string.IsNullOrEmpty(OnlyEditMyselfProduct) || !Utils.IsInt(OnlyEditMyselfProduct)) OnlyEditMyselfProduct = "0";

            if (string.IsNullOrEmpty(Id))
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(RoleName))
            {
                tip.Message = "管理组名称不能为空！";
                return Json(tip);
            }

            AdminRoles entity = AdminRoles.Find(AdminRoles._.Id == int.Parse(Id));
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.RoleName = RoleName;
            entity.RoleDescription = RoleDescription;
            entity.IsSuperAdmin = int.Parse(IsSuperAdmin);
            entity.NotAllowDel = !string.IsNullOrEmpty(NotAllowDel) && NotAllowDel == "1" ? 1 : 0;
            entity.OnlyEditMyselfArticle = int.Parse(OnlyEditMyselfArticle);
            entity.OnlyEditMyselfProduct = int.Parse(OnlyEditMyselfProduct);

            //处理权限
            if (entity.IsSuperAdmin == 1)
            {
                entity.Powers = "";
                entity.Menus = "";
            }
            else
            {
                //第一步，获取菜单
                string[] menuids = Request.Form["menuid"];
                //获取所有的菜单列表
                IList<AdminMenu> alllist = AdminMenu.GetListTree(0, -1, false, false);
                IList<AdminMenu> list = new List<AdminMenu>();
                IList<AdminMenuEvent> listevents = new List<AdminMenuEvent>();
                if (menuids != null && menuids.Length > 0)
                {
                    foreach (string s in menuids)
                    {
                        if (Utils.IsInt(s) && alllist.FirstOrDefault(v => v.Id == int.Parse(s)) != null)
                        {
                            AdminMenu tmp = alllist.FirstOrDefault(a => a.Id == int.Parse(s)).CloneEntity();
                            list.Add(tmp);
                            //处理详细权限  详细权限，每一行，则每一个菜单的详细权限，则同一个name
                            string[] eventids = Request.Form["EventKey_" + s];
                            if (eventids != null && eventids.Length > 0)
                            {
                                foreach (var item in eventids)
                                {
                                    if (Utils.IsInt(item))
                                    {
                                        AdminMenuEvent model = AdminMenuEvent.Find(AdminMenuEvent._.Id == int.Parse(item));
                                        if (model != null)
                                        {
                                            listevents.Add(model);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //序列化成json
                    if (list != null && list.Count > 0)
                    {
                        entity.Menus = JsonConvert.SerializeObject(list);
                    }
                    if (listevents != null && listevents.Count > 0)
                    {
                        entity.Powers = JsonConvert.SerializeObject(listevents);
                    }
                }
            }

            //处理文章和商品栏目权限
            string[] acpower = Request.Form["acpower"];
            string[] pcpower = Request.Form["pcpower"];

            List<int> acids = new List<int>();
            List<int> pcids = new List<int>();
            foreach (var item in acpower)
            {
                if (!string.IsNullOrEmpty(item) && Utils.IsInt(item))
                {
                    acids.Add(int.Parse(item));
                }
            }
            foreach (var item in pcpower)
            {
                if (!string.IsNullOrEmpty(item) && Utils.IsInt(item))
                {
                    pcids.Add(int.Parse(item));
                }
            }

            entity.AuthorizedArticleCagegory = JsonConvert.SerializeObject(acids);
            entity.AuthorizedCagegory = JsonConvert.SerializeObject(pcids);

            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑管理组详情成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"执行编辑管理组（{entity.Id}）详情;");
            return Json(tip);
        }
        //删除管理组
        [HttpPost]
        [MyAuthorize("del", "adminrole", "JSON")]
        public IActionResult DelAdminRole(int id)
        {
            AdminRoles entity = AdminRoles.Find(AdminRoles._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本管理组详情！";
                return Json(tip);
            }
            if (entity.NotAllowDel == 1)
            {
                tip.Message = "本管理组设定不允许删除，如果需要删除，请先解除限制！";
                return Json(tip);
            }
            //如果不是超级管理员，不允许删除
            Core.Admin my = Core.Admin.GetMyInfo();
            if (my.Roles.IsSuperAdmin != 1)
            {
                tip.Message = "非超级管理员，不能执行此操作！";
                return Json(tip);
            }
            //如果只有一个管理组，不允许删除！
            if (AdminRoles.FindCount(null, null, null, 0, 0) == 1)
            {
                tip.Message = "只有一个管理组，不能删除！";
                return Json(tip);
            }
            //删除管理组，并删除旗下所有管理员
            Core.Admin.WriteLogActions($"执行删除管理组（{entity.Id}:{entity.RoleName}）详情;");
            entity.Delete();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除管理组成功";
            return Json(tip);
        }
        #endregion

        #region 管理员管理
        //管理员管理
        [MyAuthorize("viewlist", "admins")]
        [DisplayName("管理员")]
        public IActionResult Admins()
        {
            //加载管理组
            IList<AdminRoles> list = AdminRoles.FindAll(AdminRoles._.Id > 0, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            Core.Admin.WriteLogActions("查看管理员列表");
            return View();
        }
        [MyAuthorize("viewlist",  "admins", "JSON")]
        public IActionResult GetAdmins(string keyword, int page = 1, int limit = 20, int roleid = 0)
        {
            int numPerPage, currentPage, startRowIndex;
            long totalCount = 0;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Core.Admin._.Id > 0;

            if (roleid > 0)
                ex &= Core.Admin._.RoleId == roleid;

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Core.Admin._.UserName.Contains(keyword);

            IList<Core.Admin> list = Core.Admin.FindAll(ex, null, null, startRowIndex, numPerPage);
            totalCount = Core.Admin.FindCount(ex, null, null, startRowIndex, numPerPage);
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }
        //添加管理员
        [MyAuthorize("add", "admins")]
        public IActionResult AddAdmin()
        {
            //加载管理组
            IList<AdminRoles> list = AdminRoles.FindAll(AdminRoles._.Id > 0, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            Core.Admin.WriteLogActions("查看添加管理员页面;");
            return View();
        }
        //执行添加管理员
        [HttpPost]
        [MyAuthorize("add", "admins", "JSON")]
        public IActionResult AddAdmin(IFormCollection fc)
        {
            string userName = fc["UserName"];
            string newPwd = fc["PassWord"];
            string renewPwd = fc["PassWord2"];
            string roleid = fc["RoleId"];
            string realname = fc["RealName"];

            if (!Utils.IsInt(roleid))
            {
                tip.Message = "请选择一个管理组！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(userName))
            {
                tip.Message = "登录用户名不能为空！";
                return Json(tip);
            }
            if (Utils.GetStringLength(userName.Trim()) < 5)
            {
                tip.Message = "登录用户名不能小于5个字节！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(newPwd))
            {
                tip.Message = "密码不能为空！";
                return Json(tip);
            }
            if (newPwd.Length < 5)
            {
                tip.Message = "密码不能小于5个字符！";
                return Json(tip);
            }
            if (newPwd != renewPwd)
            {
                tip.Message = "两次输入密码不一致，请重新输入！";
                return Json(tip);
            }
            //验证用户名
            if (Core.Admin.FindCount(Core.Admin._.UserName == userName, null, null, 0, 0) > 0)
            {
                tip.Message = "该用户名已经存在，请选择其他用户名！";
                return Json(tip);
            }

            Core.Admin entity = new Core.Admin();
            entity.UserName = userName;
            entity.RealName = realname;
            entity.Salt = Utils.GetRandomChar(10);
            entity.PassWord = Utils.MD5(entity.Salt + newPwd);
            entity.RoleId = int.Parse(roleid);
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加管理员成功！";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新管理员({entity.UserName});");
            return Json(tip);
        }

        //查看，编辑管理员
        [MyAuthorize("edit", "admins")]
        public IActionResult EditAdmin(int id)
        {
            //加载管理组
            IList<AdminRoles> list = AdminRoles.FindAll(AdminRoles._.Id > 0, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;

            Core.Admin entity = Core.Admin.Find(Core.Admin._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Core.Admin.WriteLogActions($"查看/编辑管理员({entity.UserName});");
            return View(entity);
        }
        //执行编辑管理员
        [HttpPost]
        [MyAuthorize("edit", "admins", "JSON")]
        public IActionResult EditAdmin(IFormCollection fc)
        {
            string userName = fc["UserName"];
            string newPwd = fc["PassWord"];
            string renewPwd = fc["PassWord2"];
            string roleid = fc["RoleId"];
            string realname = fc["RealName"];
            string Id = fc["Id"];
            if (!Utils.IsInt(Id))
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            Core.Admin entity = Core.Admin.Find(Core.Admin._.Id == int.Parse(Id));
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (!Utils.IsInt(roleid))
            {
                tip.Message = "请选择一个管理组！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(userName))
            {
                tip.Message = "登录用户名不能为空！";
                return Json(tip);
            }
            if (Utils.GetStringLength(userName.Trim()) < 5)
            {
                tip.Message = "登录用户名不能小于5个字节！";
                return Json(tip);
            }
            if (entity.UserName != userName)//修改用户名
            {
                //验证用户名是否存在
                if (Core.Admin.FindCount(Core.Admin._.Id != entity.Id & Core.Admin._.UserName == userName, null, null, 0, 0) > 0)
                {
                    tip.Message = "该用户名已经存在，请选择其他用户名！";
                    return Json(tip);
                }
                entity.UserName = userName;
            }
            if (!string.IsNullOrEmpty(newPwd))//修改密码
            {
                if (newPwd.Length < 5)
                {
                    tip.Message = "密码不能小于5个字符！";
                    return Json(tip);
                }
                if (newPwd != renewPwd)
                {
                    tip.Message = "两次输入密码不一致，请重新输入！";
                    return Json(tip);
                }
                entity.PassWord = Utils.MD5(entity.Salt + newPwd);
            }
            entity.RoleId = int.Parse(roleid);
            entity.RealName = realname;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改管理员信息成功！";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"修改新管理员({entity.Id}:{entity.UserName});");
            return Json(tip);
        }
        //删除管理员
        [HttpPost]
        [MyAuthorize("del",  "admins", "JSON")]
        public IActionResult DelAdmin(int id)
        {
            Core.Admin entity = Core.Admin.Find(Core.Admin._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            Core.Admin my = Core.Admin.GetMyInfo();
            if (entity.Id == my.Id)
            {
                tip.Message = "您不可以删除自己！";
                return Json(tip);
            }
            //如果是普通管理员，不能删除超级管理员
            if (entity.Roles.IsSuperAdmin == 1 && my.Roles.IsSuperAdmin != 1)
            {
                tip.Message = "您不可以删除超级管理员！";
                return Json(tip);
            }
            Core.Admin.WriteLogActions($"删除管理员(id:{entity.Id};usernmae:{entity.UserName});");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除管理员成功";
            return Json(tip);
        }
        #endregion

        #region 用户组管理
        [MyAuthorize( "viewlist",  "memberrole")]
        [DisplayName("用户组列表")]
        public IActionResult MemberRole()
        {
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看用户组列表;");
            return View(list);
        }

        //添加管理组
        [MyAuthorize("add", "memberrole")]
        public IActionResult AddMemberRole()
        {
            //获取所有的菜单列表
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, false);
            ViewBag.MenuList = list;
            Core.Admin.WriteLogActions("查看添加用户组;");
            return View();
        }
        //执行添加用户组
        [HttpPost]
        [MyAuthorize("add", "memberrole", "JSON")]
        public IActionResult AddMemberRole(IFormCollection fc)
        {
            string RoleName = fc["RoleName"];
            string RoleDescription = fc["RoleDescription"];
            string NotAllowDel = fc["NotAllowDel"];
            string Rank = fc["Rank"];
            string CashBack = fc["CashBack"];
            string ParentCashBack = fc["ParentCashBack"];
            string GrandfatherCashBack = fc["GrandfatherCashBack"];
            string IsHalved = fc["IsHalved"];
            string YearsPerformance = fc["YearsPerformance"];
            string JoinPrice = fc["JoinPrice"];

            if (string.IsNullOrEmpty(RoleName))
            {
                tip.Message = "用户组名称不能为空！";
                return Json(tip);
            }
            if (!Utils.IsInt(Rank))
            {
                tip.Message = "排序只能是数字！";
                return Json(tip);
            }
            if (!Utils.IsDecimal(CashBack))
            {
                tip.Message = "返现金额格式错误！";
                return Json(tip);
            }
            if (!Utils.IsDecimal(ParentCashBack))
            {
                tip.Message = "父级返现金额格式错误！";
                return Json(tip);
            }
            if (!Utils.IsDecimal(ParentCashBack))
            {
                tip.Message = "爷级返现金额格式错误！";
                return Json(tip);
            }
            if (!Utils.IsDecimal(YearsPerformance))
            {
                tip.Message = "年销售额格式错误！";
                return Json(tip);
            }
            if (!Utils.IsDecimal(JoinPrice))
            {
                tip.Message = "入驻费用格式错误！";
                return Json(tip);
            }
            MemberRoles entity = new MemberRoles();
            entity.RoleName = RoleName;
            entity.RoleDescription = RoleDescription;
            entity.NotAllowDel = !string.IsNullOrEmpty(NotAllowDel) && NotAllowDel == "1" ? 1 : 0;
            entity.IsHalved = !string.IsNullOrEmpty(IsHalved) && IsHalved == "1" ? 1 : 0;
            entity.Rank = int.Parse(Rank);
            entity.CashBack = decimal.Parse(CashBack);
            entity.ParentCashBack = decimal.Parse(ParentCashBack);
            entity.GrandfatherCashBack = decimal.Parse(GrandfatherCashBack);
            entity.YearsPerformance = decimal.Parse(YearsPerformance);
            entity.JoinPrice = decimal.Parse(JoinPrice);
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加用户组成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新用户组（{entity.Id}）;");
            return Json(tip);
        }
        //编辑用户组
        [MyAuthorize("edit", "memberrole")]
        public IActionResult EditMemberRole(int id)
        {
            MemberRoles entity = MemberRoles.Find(MemberRoles._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！", 0, true, "");
            }
            Core.Admin.WriteLogActions($"查看/编辑用户组（{id}）详情;");
            return View(entity);
        }
        [HttpPost]
        [MyAuthorize("edit", "memberrole", "JSON")]
        public IActionResult EditMemberRole(MemberRoles model)
        {
            if (model.Id <=0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(model.RoleName))
            {
                tip.Message = "用户组名称不能为空！";
                return Json(tip);
            }

            if (model.CashBack <0)
            {
                tip.Message = "返现金额格式错误！";
                return Json(tip);
            }
            if (model.ParentCashBack <0)
            {
                tip.Message = "父级返现金额格式错误！";
                return Json(tip);
            }
            if (model.GrandfatherCashBack<0)
            {
                tip.Message = "爷级返现金额格式错误！";
                return Json(tip);
            }
            if (model.YearsPerformance <0)
            {
                tip.Message = "年销售额格式错误！";
                return Json(tip);
            }
            if (model.JoinPrice <0)
            {
                tip.Message = "入驻费用格式错误！";
                return Json(tip);
            }

            MemberRoles entity = MemberRoles.Find(MemberRoles._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.RoleName = model.RoleName;
            entity.RoleDescription = model.RoleDescription;
            entity.NotAllowDel = model.NotAllowDel;
            entity.IsHalved = model.IsHalved;
            entity.Rank = model.Rank;
            entity.CashBack = model.CashBack;
            entity.ParentCashBack = model.ParentCashBack;
            entity.GrandfatherCashBack = model.GrandfatherCashBack;
            entity.YearsPerformance = model.YearsPerformance;
            entity.JoinPrice = model.JoinPrice;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑用户组成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"编辑用户组（{entity.Id}）;");
            return Json(tip);
        }

        //删除用户组
        [HttpPost]
        [MyAuthorize("del", "memberrole", "JSON")]
        public IActionResult DelMemberRole(int id)
        {
            MemberRoles entity = MemberRoles.Find(MemberRoles._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本用户组详情！";
                return Json(tip);
            }
            if (entity.NotAllowDel == 1)
            {
                tip.Message = "本管理组设定不允许删除，如果需要删除，请先解除限制！";
                return Json(tip);
            }

            //删除管理组，并删除旗下所有管理员
            Core.Admin.WriteLogActions($"执行删除用户组（{entity.Id}:{entity.RoleName}）详情;");
            entity.Delete();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除用户组成功";
            return Json(tip);
        }

        #endregion

        #region 用户管理
        [MyAuthorize("viewlist", "members")]
        [DisplayName("用户列表")]
        public IActionResult Members()
        {
            //加载用户组
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            Core.Admin.WriteLogActions("查看用户列表;");
            return View();
        }
        [MyAuthorize("viewlist", "members", "JSON")]
        public IActionResult GetMembers(string keyword, int page = 1, int limit = 20, int roleid = 0, int groupid = 0)
        {
            int numPerPage, currentPage, startRowIndex;
            long totalCount = 0;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Member._.Id > 0;

            if (roleid > 0)
                ex &= Member._.RoleId == roleid;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (Member._.Id == int.Parse(keyword) | Member._.Tel.Contains(keyword));
                }
                else
                {
                    ex &= (Member._.UserName.Contains(keyword) | Member._.Nickname.Contains(HttpUtility.UrlEncode(keyword)) | Member._.RealName.Contains(keyword));
                }
            }

            IList<Member> list = Member.FindAll(ex, null, null, startRowIndex, numPerPage);
            totalCount = Member.FindCount(ex, null, null, startRowIndex, numPerPage);
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }
        //查看，编辑用户信息
        [MyAuthorize("add", "members")]
        public IActionResult AddMember()
        {
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1, null, null, 0, 0);

            Core.Admin.WriteLogActions($"查看添加用户页面;");
            Member model = new Member();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize("add", "members", "JSON")]
        public IActionResult AddMember(Member model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                tip.Message = "用户名必须是一个手机号码！";
                return Json(tip);
            }
            //判断用户是否存在
            if (Member.FindCount(Member._.UserName == model.UserName, null, null, 0, 0) > 0)
            {
                tip.Message = "用户名已经存在！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(model.PassWord) || model.PassWord.Length < 6)
            {
                tip.Message = "密码必须不小于6个字符";
                return Json(tip);
            }
            string PassWord2 = Request.Form["PassWord2"];
            if (model.PassWord != PassWord2)
            {
                tip.Message = "两次输入密码不一致，请重新输入！";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Email) && !Utils.IsValidEmail(model.Email))
            {
                tip.Message = "邮箱格式错误！";
                return Json(tip);
            }
            if (model.RoleId <= 0)
            {
                tip.Message = "请选择一个用户组！";
                return Json(tip);
            }
            if (model.Parent == 0 && model.Grandfather != 0)
                model.Grandfather = 0;
            //执行添加
            Member entity = new Member();
            entity.UserName = model.UserName;
            entity.Salt = Utils.GetRandomChar(20);
            entity.PassWord = Utils.MD5(entity.Salt + model.PassWord);
            entity.Email = model.Email;
            entity.Tel = model.Tel;
            entity.Parent = model.Parent;
            entity.Grandfather = model.Grandfather;
            entity.IsLock = 0;
            entity.RoleId = model.RoleId;
            entity.RealName = model.RealName;
            entity.RegIP = "127.0.0.1";
            entity.RegTime = DateTime.Now;
            entity.Balance = 0;
            entity.GiftBalance = 0;
            entity.ExtCredits1 = 0;
            entity.ExtCredits2 = 0;
            entity.ExtCredits3 = 0;
            entity.ExtCredits4 = 0;
            entity.ExtCredits5 = 0;
            entity.TotalCredits = 0;
            entity.YearsPerformance = 0;
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加用户成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加用户({entity.Id}/{entity.UserName});");
            return Json(tip);
        }


        //查看，编辑用户信息
        [MyAuthorize("edit", "members")]
        public IActionResult EditMember(int id)
        {
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;

            Member entity = Member.Find(Core.Member._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1 & Member._.Id != entity.Id, null, null, 0, 0);
            //获取用户余额变化记录
            IList<BalanceChangeLog> listbalancelogs = BalanceChangeLog.FindAll(BalanceChangeLog._.UId == entity.Id, null, null, 0, 0);
            ViewBag.listbalancelogs = listbalancelogs;

            IList<RebateChangeLog> listrechargebalancelogs = RebateChangeLog.FindAll(RebateChangeLog._.UId == entity.Id, null, null, 0, 0);
            ViewBag.listrechargebalancelogs = listrechargebalancelogs;



            Core.Admin.WriteLogActions($"查看/编辑用户({entity.UserName});");
            return View(entity);
        }
        [HttpPost]
        [MyAuthorize("edit", "members", "JSON")]
        public IActionResult EditMember(Member model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误请求";
                return Json(tip);
            }
            Member entity = Member.FindById(model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.PassWord))
            {
                if (string.IsNullOrEmpty(model.PassWord) || model.PassWord.Length < 6)
                {
                    tip.Message = "密码必须不小于6个字符";
                    return Json(tip);
                }
                string PassWord2 = Request.Form["PassWord2"];
                if (model.PassWord != PassWord2)
                {
                    tip.Message = "两次输入密码不一致，请重新输入！";
                    return Json(tip);
                }
                entity.PassWord = Utils.MD5(entity.Salt + model.PassWord);
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                tip.Message = "用户名必须是一个手机号码！";
                return Json(tip);
            }
            //判断用户是否存在
            if (Member.FindCount(Member._.UserName == model.UserName & Member._.Id != entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "用户名已经存在，请重新填写一个！";
                return Json(tip);
            }
            string IsVerifySellers = Request.Form["IsVerifySellers"];
            entity.UserName = model.UserName;
            entity.Email = model.Email;
            entity.Tel = model.Tel;
            entity.Parent = model.Parent;
            entity.Grandfather = model.Grandfather;
            entity.RoleId = model.RoleId;
            entity.RealName = model.RealName;
            entity.Phone = model.Phone;
            entity.Alipay = model.Alipay;
            entity.Bank = model.Bank;
            entity.BankCardNO = model.BankCardNO;
            entity.Skype = model.Skype;
            if (IsVerifySellers == "")
            {
                entity.IsVerifySellers = 1;
            }
            else
                entity.IsVerifySellers = 0;

            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑用户成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"编辑用户({entity.Id}/{entity.UserName});");
            return Json(tip);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [MyAuthorize("del", "members", "JSON")]
        public IActionResult DelMember(int id)
        {
            Member he = Member.Find(Member._.Id == id);
            if (he == null)
            {
                tip.Message = "系统找不到本用户！";
                return Json(tip);
            }
            he.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除用户成功！";
            Core.Admin.WriteLogActions($"删除用户({he.Id}|{he.UserName});");
            return Json(tip);
        }
        #endregion

        #region 后台管理日志列表
        [MyAuthorize("viewlist", "admincplog")]
        [DisplayName("后台管理日志")]
        public IActionResult AdminCPLogList()
        {
            return View();
        }
        [MyAuthorize("viewlist", "admincplog","JSON")]
        public IActionResult GetAdminCPLogList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = AdminLog._.Id > 0;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (AdminLog._.Id == int.Parse(keyword) | AdminLog._.UserName.Contains(keyword));
                }
                else
                {
                    ex &= AdminLog._.UserName.Contains(keyword);
                }
            }
            string kid = Request.Query["kid"];
            //if (Utils.IsInt(kid) && int.Parse(kid) > 0)
            //{
            //    ex &= AdminLog._.KId == int.Parse(kid);
            //}
            IList<AdminLog> list = AdminLog.FindAll(ex, AdminLog._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = AdminLog.FindCount(ex, AdminLog._.Id.Desc(), null, startRowIndex, numPerPage);
            Core.Admin.WriteLogActions("后台管理日志留言列表(page:" + page + ");");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }

        #endregion

        #region 查看管理日志详情
        [MyAuthorize("view", "admincplog")]
        public IActionResult ViewAdminLogDetail(int id)
        {
            AdminLog entity = AdminLog.Find(AdminLog._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin.WriteLogActions("查看管理日志详情(" + id + ");");

            return View(entity);
        }
        #endregion
    }
}