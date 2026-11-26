using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using COMCMS.Common;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    /// <summary>
    /// 后台用户管理控制器
    /// </summary>
    [DisplayName("管理员")]
    public class MemberController : AdminBaseController
    {
        private readonly SystemSetting _systemSetting;
        private readonly IWebHostEnvironment _env;
        private readonly AttachConfigEntity attach;

        public MemberController(IWebHostEnvironment env, IOptions<SystemSetting> systemSetting)
        {
            attach = Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _systemSetting = systemSetting.Value;
        }

        #region 修改密码
        [MyAuthorize("view", "editme")]
        public IActionResult EditMe()
        {
            Core.Admin my = Core.Admin.GetMyInfo();
            Core.Admin.WriteLogActions("查看/编辑我的信息;");
            ViewBag.passwordTip = Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength);
            return View(my);
        }

        [HttpPost]
        [MyAuthorize("view", "editme", "JSON")]
        public IActionResult EditMe(IFormCollection fc)
        {
            Core.Admin my = Core.Admin.GetMyInfo();
            if (my == null)
            {
                tip.Message = "登录状态无效，请重新登录！";
                return Json(tip);
            }

            string userName = fc["UserName"];
            string oldPwd = fc["txtOldPwd"];
            string newPwd = fc["txtNewPwd"];
            string renewPwd = fc["txtreNewPwd"];
            string realname = fc["RealName"];
            string tel = fc["Tel"];
            string email = fc["Email"];

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

            if (userName != my.UserName)
            {
                if (Core.Admin.FindCount(Core.Admin._.UserName == userName & Core.Admin._.Id != my.Id, null, null, 0, 0) > 0)
                {
                    tip.Message = "新用户名已经存在，请选择其他用户名！";
                    return Json(tip);
                }
                my.UserName = userName;
            }

            if (!string.IsNullOrEmpty(newPwd))
            {
                if (string.IsNullOrWhiteSpace(oldPwd) || oldPwd.Length < 5)
                {
                    tip.Message = "您修改密码，旧密码不能为空！";
                    return Json(tip);
                }
                if (string.IsNullOrWhiteSpace(newPwd) || newPwd.Length < 12 || newPwd.Length > 128)
                {
                    tip.Message = "管理员密码长度必须为12至128个字符！";
                    return Json(tip);
                }
                if (_systemSetting.PasswordStrength > 0 && !Utils.CheckPasswordStrength(newPwd, _systemSetting.PasswordStrength))
                {
                    tip.Message = $"您的密码强度不符合要求:{Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength)}！";
                    return Json(tip);
                }
                if (newPwd != renewPwd)
                {
                    tip.Message = "您输入的两次密码不一样！";
                    return Json(tip);
                }
                if (!PasswordHashService.Verify(my.PassWord, my.Salt, oldPwd, out _))
                {
                    tip.Message = "您输入的旧密码不正确，请重新输入！";
                    return Json(tip);
                }
                my.PassWord = PasswordHashService.HashPassword(newPwd);
            }
            my.Tel = tel;
            my.Email = email;
            my.RealName = realname;
            my.Update();
            Core.Admin.WriteLogActions("修改我的信息;");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑我的信息成功！";
            return Json(tip);
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

        [MyAuthorize("add", "adminrole")]
        public IActionResult AddAdminRole()
        {
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, false);
            ViewBag.MenuList = list;
            IList<ArticleCategory> aclist = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.aclist = aclist;
            IList<Category> pclist = Category.GetListTree(0, -1, true, true);
            ViewBag.pclist = pclist;

            Core.Admin.WriteLogActions("查看添加管理组页面;");
            return View();
        }

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

            if (!int.TryParse(IsSuperAdmin, out var isSuperAdminVal)) isSuperAdminVal = 0;
            if (!int.TryParse(OnlyEditMyselfArticle, out var onlyArticleVal)) onlyArticleVal = 0;
            if (!int.TryParse(OnlyEditMyselfProduct, out var onlyProductVal)) onlyProductVal = 0;

            if (string.IsNullOrWhiteSpace(RoleName))
            {
                tip.Message = "管理组名称不能为空！";
                return Json(tip);
            }

            Core.Admin caller = Core.Admin.GetMyInfo();
            if (!CanManageSuperAdminRole(caller?.Roles?.IsSuperAdmin == 1, targetIsSuperAdmin: isSuperAdminVal == 1, existingIsSuperAdmin: false))
            {
                tip.Message = "非超级管理员无权创建超级管理员组！";
                return Json(tip);
            }

            AdminRoles entity = new AdminRoles
            {
                RoleName = RoleName.Trim(),
                RoleDescription = RoleDescription,
                IsSuperAdmin = isSuperAdminVal == 1 ? 1 : 0,
                NotAllowDel = NotAllowDel == "1" ? 1 : 0,
                OnlyEditMyselfArticle = onlyArticleVal == 1 ? 1 : 0,
                OnlyEditMyselfProduct = onlyProductVal == 1 ? 1 : 0
            };

            PopulateRolePermissions(entity);
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加管理组成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新管理组（{entity.Id}）;");
            return Json(tip);
        }

        [MyAuthorize("edit", "adminrole")]
        public IActionResult EditAdminRole(int id)
        {
            AdminRoles entity = AdminRoles.Find(AdminRoles._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！", 0, true, "");
            }
            Core.Admin caller = Core.Admin.GetMyInfo();
            if (entity.IsSuperAdmin == 1 && caller?.Roles?.IsSuperAdmin != 1)
            {
                return EchoTipPage("非超级管理员无权查看或编辑超级管理员组！", 0, true, "");
            }

            if (string.IsNullOrEmpty(entity.Powers)) entity.Powers = "[]";
            if (string.IsNullOrEmpty(entity.Menus)) entity.Menus = "[]";
            if (string.IsNullOrEmpty(entity.AuthorizedArticleCagegory)) entity.AuthorizedArticleCagegory = "[]";
            if (string.IsNullOrEmpty(entity.AuthorizedCagegory)) entity.AuthorizedCagegory = "[]";

            ViewBag.aclist = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.pclist = Category.GetListTree(0, -1, true, true);
            ViewBag.MenuList = AdminMenu.GetListTree(0, -1, false, false);
            Core.Admin.WriteLogActions($"查看管理组（{id}）详情;");
            return View(entity);
        }

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

            if (!int.TryParse(Id, out var roleId) || roleId <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(RoleName))
            {
                tip.Message = "管理组名称不能为空！";
                return Json(tip);
            }
            if (!int.TryParse(IsSuperAdmin, out var isSuperAdminVal)) isSuperAdminVal = 0;
            if (!int.TryParse(OnlyEditMyselfArticle, out var onlyArticleVal)) onlyArticleVal = 0;
            if (!int.TryParse(OnlyEditMyselfProduct, out var onlyProductVal)) onlyProductVal = 0;

            AdminRoles entity = AdminRoles.Find(AdminRoles._.Id == roleId);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }

            Core.Admin caller = Core.Admin.GetMyInfo();
            if (!CanManageSuperAdminRole(caller?.Roles?.IsSuperAdmin == 1, targetIsSuperAdmin: isSuperAdminVal == 1, existingIsSuperAdmin: entity.IsSuperAdmin == 1))
            {
                tip.Message = "非超级管理员无权编辑超级管理员组或将其提升为超级管理员！";
                return Json(tip);
            }

            entity.RoleName = RoleName.Trim();
            entity.RoleDescription = RoleDescription;
            entity.IsSuperAdmin = isSuperAdminVal == 1 ? 1 : 0;
            entity.NotAllowDel = NotAllowDel == "1" ? 1 : 0;
            entity.OnlyEditMyselfArticle = onlyArticleVal == 1 ? 1 : 0;
            entity.OnlyEditMyselfProduct = onlyProductVal == 1 ? 1 : 0;

            PopulateRolePermissions(entity);
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑管理组详情成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"执行编辑管理组（{entity.Id}）详情;");
            return Json(tip);
        }

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
            Core.Admin my = Core.Admin.GetMyInfo();
            if (my?.Roles?.IsSuperAdmin != 1)
            {
                tip.Message = "非超级管理员，不能执行此操作！";
                return Json(tip);
            }
            if (AdminRoles.FindCount(null, null, null, 0, 0) == 1)
            {
                tip.Message = "只有一个管理组，不能删除！";
                return Json(tip);
            }
            Core.Admin.WriteLogActions($"执行删除管理组（{entity.Id}:{entity.RoleName}）详情;");
            entity.Delete();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除管理组成功";
            return Json(tip);
        }

        private void PopulateRolePermissions(AdminRoles entity)
        {
            if (entity.IsSuperAdmin == 1)
            {
                entity.Powers = "";
                entity.Menus = "";
            }
            else
            {
                string[] menuids = Request.Form["menuid"];
                IList<AdminMenu> alllist = AdminMenu.GetListTree(0, -1, false, false);
                IList<AdminMenu> list = new List<AdminMenu>();
                IList<AdminMenuEvent> listevents = new List<AdminMenuEvent>();
                if (menuids != null && menuids.Length > 0)
                {
                    foreach (string s in menuids)
                    {
                        if (Utils.IsInt(s))
                        {
                            var menuId = int.Parse(s);
                            var menu = alllist.FirstOrDefault(v => v.Id == menuId);
                            if (menu != null)
                            {
                                list.Add(menu.CloneEntity());
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
                    }
                }
                entity.Menus = JsonConvert.SerializeObject(list);
                entity.Powers = JsonConvert.SerializeObject(listevents);
            }

            string[] acpower = Request.Form["acpower"];
            string[] pcpower = Request.Form["pcpower"];
            List<int> acids = acpower.Where(x => !string.IsNullOrEmpty(x) && Utils.IsInt(x)).Select(int.Parse).ToList();
            List<int> pcids = pcpower.Where(x => !string.IsNullOrEmpty(x) && Utils.IsInt(x)).Select(int.Parse).ToList();
            entity.AuthorizedArticleCagegory = JsonConvert.SerializeObject(acids);
            entity.AuthorizedCagegory = JsonConvert.SerializeObject(pcids);
        }
        #endregion

        #region 管理员管理
        [MyAuthorize("viewlist", "admins")]
        [DisplayName("管理员")]
        public IActionResult Admins()
        {
            IList<AdminRoles> list = AdminRoles.FindAll(AdminRoles._.Id > 0, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            Core.Admin.WriteLogActions("查看管理员列表");
            return View();
        }

        [MyAuthorize("viewlist", "admins", "JSON")]
        public IActionResult GetAdmins(string keyword, int page = 1, int limit = 20, int roleid = 0)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

            Expression ex = Core.Admin._.Id > 0;
            if (roleid > 0)
                ex &= Core.Admin._.RoleId == roleid;
            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= Core.Admin._.UserName.Contains(keyword);

            IList<Core.Admin> list = Core.Admin.FindAll(ex, Admin._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = Core.Admin.FindCount(ex, null, null, startRowIndex, numPerPage);

            // 仅返回安全 DTO，禁止序列化 PassWord 哈希与 Salt
            var rows = list.Select(ToSafeAdminDto).ToList();
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows }), "application/json");
        }

        [MyAuthorize("add", "admins")]
        public IActionResult AddAdmin()
        {
            Core.Admin caller = Core.Admin.GetMyInfo();
            Expression roleEx = AdminRoles._.Id > 0;
            if (caller?.Roles?.IsSuperAdmin != 1)
                roleEx &= AdminRoles._.IsSuperAdmin != 1;
            ViewBag.RoleList = AdminRoles.FindAll(roleEx, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.passwordTip = Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength);
            Core.Admin.WriteLogActions("查看添加管理员页面;");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "admins", "JSON")]
        public IActionResult AddAdmin(IFormCollection fc)
        {
            string userName = fc["UserName"];
            string newPwd = fc["PassWord"];
            string renewPwd = fc["PassWord2"];
            string roleid = fc["RoleId"];
            string realname = fc["RealName"];

            if (!int.TryParse(roleid, out var targetRoleId) || targetRoleId <= 0)
            {
                tip.Message = "请选择一个管理组！";
                return Json(tip);
            }

            AdminRoles targetRole = AdminRoles.Find(AdminRoles._.Id == targetRoleId);
            if (targetRole == null)
            {
                tip.Message = "所选管理组不存在！";
                return Json(tip);
            }

            Core.Admin caller = Core.Admin.GetMyInfo();
            if (!CanAssignOrModifyAdmin(caller?.Roles?.IsSuperAdmin == 1, targetRole.IsSuperAdmin == 1, existingTargetIsSuperAdmin: false))
            {
                tip.Message = "非超级管理员无权分配超级管理员角色！";
                return Json(tip);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                tip.Message = "登录用户名不能为空！";
                return Json(tip);
            }
            userName = userName.Trim();
            if (Utils.GetStringLength(userName) < 5)
            {
                tip.Message = "登录用户名不能小于5个字节！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(newPwd) || newPwd.Length < 12 || newPwd.Length > 128)
            {
                tip.Message = "管理员密码长度必须为12至128个字符！";
                return Json(tip);
            }
            if (_systemSetting.PasswordStrength > 0 && !Utils.CheckPasswordStrength(newPwd, _systemSetting.PasswordStrength))
            {
                tip.Message = $"您的密码强度不符合要求:{Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength)}！";
                return Json(tip);
            }
            if (newPwd != renewPwd)
            {
                tip.Message = "两次输入密码不一致，请重新输入！";
                return Json(tip);
            }
            if (Core.Admin.FindCount(Core.Admin._.UserName == userName, null, null, 0, 0) > 0)
            {
                tip.Message = "该用户名已经存在，请选择其他用户名！";
                return Json(tip);
            }

            Core.Admin entity = new Core.Admin
            {
                UserName = userName,
                RealName = realname,
                Salt = Utils.GetRandomChar(10),
                PassWord = PasswordHashService.HashPassword(newPwd),
                RoleId = targetRoleId
            };
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加管理员成功！";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新管理员({entity.UserName});");
            return Json(tip);
        }

        [MyAuthorize("edit", "admins")]
        public IActionResult EditAdmin(int id)
        {
            Core.Admin entity = Core.Admin.Find(Core.Admin._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin caller = Core.Admin.GetMyInfo();
            if (entity.Roles?.IsSuperAdmin == 1 && caller?.Roles?.IsSuperAdmin != 1)
            {
                return EchoTipPage("非超级管理员无权编辑超级管理员账号！");
            }

            Expression roleEx = AdminRoles._.Id > 0;
            if (caller?.Roles?.IsSuperAdmin != 1)
                roleEx &= AdminRoles._.IsSuperAdmin != 1;
            ViewBag.RoleList = AdminRoles.FindAll(roleEx, AdminRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.passwordTip = Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength);
            Core.Admin.WriteLogActions($"查看/编辑管理员({entity.UserName});");
            return View(entity);
        }

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

            if (!int.TryParse(Id, out var adminId) || adminId <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            Core.Admin entity = Core.Admin.Find(Core.Admin._.Id == adminId);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (!int.TryParse(roleid, out var targetRoleId) || targetRoleId <= 0)
            {
                tip.Message = "请选择一个管理组！";
                return Json(tip);
            }
            AdminRoles targetRole = AdminRoles.Find(AdminRoles._.Id == targetRoleId);
            if (targetRole == null)
            {
                tip.Message = "所选管理组不存在！";
                return Json(tip);
            }

            Core.Admin caller = Core.Admin.GetMyInfo();
            if (!CanAssignOrModifyAdmin(caller?.Roles?.IsSuperAdmin == 1, targetRole.IsSuperAdmin == 1, existingTargetIsSuperAdmin: entity.Roles?.IsSuperAdmin == 1))
            {
                tip.Message = "非超级管理员无权编辑超级管理员或将账号提升为超级管理员！";
                return Json(tip);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                tip.Message = "登录用户名不能为空！";
                return Json(tip);
            }
            userName = userName.Trim();
            if (Utils.GetStringLength(userName) < 5)
            {
                tip.Message = "登录用户名不能小于5个字节！";
                return Json(tip);
            }
            if (entity.UserName != userName)
            {
                if (Core.Admin.FindCount(Core.Admin._.Id != entity.Id & Core.Admin._.UserName == userName, null, null, 0, 0) > 0)
                {
                    tip.Message = "该用户名已经存在，请选择其他用户名！";
                    return Json(tip);
                }
                entity.UserName = userName;
            }
            if (!string.IsNullOrEmpty(newPwd))
            {
                if (string.IsNullOrWhiteSpace(newPwd) || newPwd.Length < 12 || newPwd.Length > 128)
                {
                    tip.Message = "管理员密码长度必须为12至128个字符！";
                    return Json(tip);
                }
                if (_systemSetting.PasswordStrength > 0 && !Utils.CheckPasswordStrength(newPwd, _systemSetting.PasswordStrength))
                {
                    tip.Message = $"您的密码强度不符合要求:{Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength)}！";
                    return Json(tip);
                }
                if (newPwd != renewPwd)
                {
                    tip.Message = "两次输入密码不一致，请重新输入！";
                    return Json(tip);
                }
                entity.PassWord = PasswordHashService.HashPassword(newPwd);
            }
            entity.RoleId = targetRoleId;
            entity.RealName = realname;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改管理员信息成功！";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"修改管理员({entity.Id}:{entity.UserName});");
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "admins", "JSON")]
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
            if (entity.Roles?.IsSuperAdmin == 1 && my?.Roles?.IsSuperAdmin != 1)
            {
                tip.Message = "您不可以删除超级管理员！";
                return Json(tip);
            }
            Core.Admin.WriteLogActions($"删除管理员(id:{entity.Id};username:{entity.UserName});");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除管理员成功";
            return Json(tip);
        }
        #endregion

        #region 用户组管理
        [MyAuthorize("viewlist", "memberrole")]
        [DisplayName("用户组列表")]
        public IActionResult MemberRole()
        {
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看用户组列表;");
            return View(list);
        }

        [MyAuthorize("add", "memberrole")]
        public IActionResult AddMemberRole()
        {
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, false);
            ViewBag.MenuList = list;
            Core.Admin.WriteLogActions("查看添加用户组;");
            return View();
        }

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

            if (string.IsNullOrWhiteSpace(RoleName))
            {
                tip.Message = "用户组名称不能为空！";
                return Json(tip);
            }
            if (!int.TryParse(Rank, out var rankVal))
            {
                tip.Message = "排序只能是数字！";
                return Json(tip);
            }
            if (!decimal.TryParse(CashBack, out var cashBackVal) || cashBackVal < 0)
            {
                tip.Message = "返现金额格式错误！";
                return Json(tip);
            }
            if (!decimal.TryParse(ParentCashBack, out var parentCashBackVal) || parentCashBackVal < 0)
            {
                tip.Message = "父级返现金额格式错误！";
                return Json(tip);
            }
            if (!decimal.TryParse(GrandfatherCashBack, out var grandfatherCashBackVal) || grandfatherCashBackVal < 0)
            {
                tip.Message = "爷级返现金额格式错误！";
                return Json(tip);
            }
            if (!decimal.TryParse(YearsPerformance, out var yearsPerfVal) || yearsPerfVal < 0)
            {
                tip.Message = "年销售额格式错误！";
                return Json(tip);
            }
            if (!decimal.TryParse(JoinPrice, out var joinPriceVal) || joinPriceVal < 0)
            {
                tip.Message = "入驻费用格式错误！";
                return Json(tip);
            }

            MemberRoles entity = new MemberRoles
            {
                RoleName = RoleName.Trim(),
                RoleDescription = RoleDescription,
                NotAllowDel = NotAllowDel == "1" ? 1 : 0,
                IsHalved = IsHalved == "1" ? 1 : 0,
                Rank = rankVal,
                CashBack = cashBackVal,
                ParentCashBack = parentCashBackVal,
                GrandfatherCashBack = grandfatherCashBackVal,
                YearsPerformance = yearsPerfVal,
                JoinPrice = joinPriceVal
            };
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加用户组成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加新用户组（{entity.Id}）;");
            return Json(tip);
        }

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
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.RoleName))
            {
                tip.Message = "用户组名称不能为空！";
                return Json(tip);
            }
            if (model.CashBack < 0 || model.ParentCashBack < 0 || model.GrandfatherCashBack < 0 || model.YearsPerformance < 0 || model.JoinPrice < 0)
            {
                tip.Message = "金额或比例不能为负数！";
                return Json(tip);
            }

            MemberRoles entity = MemberRoles.Find(MemberRoles._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.RoleName = model.RoleName.Trim();
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
                tip.Message = "本用户组设定不允许删除，如果需要删除，请先解除限制！";
                return Json(tip);
            }
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
            IList<MemberRoles> list = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            ViewBag.RoleList = list;
            Core.Admin.WriteLogActions("查看用户列表;");
            return View();
        }

        [MyAuthorize("viewlist", "members", "JSON")]
        public IActionResult GetMembers(string keyword, int page = 1, int limit = 20, int roleid = 0, int groupid = 0)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

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

            IList<Member> list = Member.FindAll(ex, Member._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = Member.FindCount(ex, null, null, startRowIndex, numPerPage);

            // 仅返回安全 DTO，严禁泄露 PassWord 哈希、Salt 等敏感字段
            var rows = list.Select(ToSafeMemberDto).ToList();
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows }), "application/json");
        }

        [MyAuthorize("add", "members")]
        public IActionResult AddMember()
        {
            ViewBag.RoleList = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            // 限制下拉候选数量，避免海量会员 OOM
            ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1, Member._.Id.Desc(), null, 0, 200);
            ViewBag.passwordTip = Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength);
            Core.Admin.WriteLogActions("查看添加用户页面;");
            return View(new Member());
        }

        [HttpPost]
        [MyAuthorize("add", "members", "JSON")]
        public IActionResult AddMember(Member model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                tip.Message = "用户名不能为空！";
                return Json(tip);
            }
            if (Member.FindCount(Member._.UserName == model.UserName, null, null, 0, 0) > 0)
            {
                tip.Message = "用户名已经存在！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.PassWord) || model.PassWord.Length < 10 || model.PassWord.Length > 128)
            {
                tip.Message = "会员密码长度必须为10至128个字符";
                return Json(tip);
            }
            if (_systemSetting.PasswordStrength > 0 && !Utils.CheckPasswordStrength(model.PassWord, _systemSetting.PasswordStrength))
            {
                tip.Message = $"您的密码强度不符合要求:{Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength)}！";
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

            Member entity = new Member
            {
                UserName = model.UserName.Trim(),
                Salt = Utils.GetRandomChar(20),
                PassWord = PasswordHashService.HashPassword(model.PassWord),
                Email = model.Email,
                Tel = model.Tel,
                Parent = model.Parent,
                Grandfather = model.Grandfather,
                IsLock = 0,
                RoleId = model.RoleId,
                RealName = model.RealName,
                RegIP = Utils.GetIP(),
                RegTime = DateTime.Now,
                Balance = 0,
                GiftBalance = 0,
                ExtCredits1 = 0,
                ExtCredits2 = 0,
                ExtCredits3 = 0,
                ExtCredits4 = 0,
                ExtCredits5 = 0,
                TotalCredits = 0,
                YearsPerformance = 0
            };
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加用户成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"添加用户({entity.Id}/{entity.UserName});");
            return Json(tip);
        }

        [MyAuthorize("edit", "members")]
        public IActionResult EditMember(int id)
        {
            ViewBag.RoleList = MemberRoles.FindAll(MemberRoles._.Id > 0, MemberRoles._.Rank.Asc(), null, 0, 0);
            Member entity = Member.Find(Core.Member._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1 & Member._.Id != entity.Id, Member._.Id.Desc(), null, 0, 200);
            ViewBag.listbalancelogs = BalanceChangeLog.FindAll(BalanceChangeLog._.UId == entity.Id, BalanceChangeLog._.Id.Desc(), null, 0, 200);
            ViewBag.listrechargebalancelogs = RebateChangeLog.FindAll(RebateChangeLog._.UId == entity.Id, RebateChangeLog._.Id.Desc(), null, 0, 200);
            ViewBag.passwordTip = Utils.GetPasswordStrengthTip(_systemSetting.PasswordStrength);

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
            bool passwordChanged = false;
            if (!string.IsNullOrEmpty(model.PassWord))
            {
                if (string.IsNullOrWhiteSpace(model.PassWord) || model.PassWord.Length < 10 || model.PassWord.Length > 128)
                {
                    tip.Message = "会员密码长度必须为10至128个字符";
                    return Json(tip);
                }
                string PassWord2 = Request.Form["PassWord2"];
                if (model.PassWord != PassWord2)
                {
                    tip.Message = "两次输入密码不一致，请重新输入！";
                    return Json(tip);
                }
                entity.PassWord = PasswordHashService.HashPassword(model.PassWord);
                passwordChanged = true;
            }
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                tip.Message = "用户名不能为空！";
                return Json(tip);
            }
            if (Member.FindCount(Member._.UserName == model.UserName & Member._.Id != entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "用户名已经存在，请重新填写一个！";
                return Json(tip);
            }

            string IsVerifySellers = Request.Form["IsVerifySellers"];
            entity.UserName = model.UserName.Trim();
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
            entity.IsVerifySellers = IsVerifySellers == "1" ? 1 : 0;

            entity.Update();

            if (passwordChanged)
            {
                RevokeMemberSessions(entity.Id);
            }

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑用户成功";
            tip.ReturnUrl = "close";
            Core.Admin.WriteLogActions($"编辑用户({entity.Id}/{entity.UserName});");
            return Json(tip);
        }

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
            RevokeMemberSessions(he.Id);
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

        [MyAuthorize("viewlist", "admincplog", "JSON")]
        public IActionResult GetAdminCPLogList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

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
            IList<AdminLog> list = AdminLog.FindAll(ex, AdminLog._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = AdminLog.FindCount(ex, AdminLog._.Id.Desc(), null, startRowIndex, numPerPage);
            Core.Admin.WriteLogActions("查看后台管理日志列表(page:" + currentPage + ");");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "application/json");
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

        #region 权限与脱敏辅助方法
        internal static bool CanManageSuperAdminRole(bool callerIsSuperAdmin, bool targetIsSuperAdmin, bool existingIsSuperAdmin)
        {
            if (callerIsSuperAdmin) return true;
            return !targetIsSuperAdmin && !existingIsSuperAdmin;
        }

        internal static bool CanAssignOrModifyAdmin(bool callerIsSuperAdmin, bool targetRoleIsSuperAdmin, bool existingTargetIsSuperAdmin)
        {
            if (callerIsSuperAdmin) return true;
            return !targetRoleIsSuperAdmin && !existingTargetIsSuperAdmin;
        }

        internal static object ToSafeAdminDto(Core.Admin admin) => new
        {
            Id = admin.Id,
            UserName = admin.UserName,
            RealName = admin.RealName,
            Tel = admin.Tel,
            Email = admin.Email,
            UserLevel = admin.UserLevel,
            RoleId = admin.RoleId,
            Roles = admin.Roles == null ? null : new { admin.Roles.Id, admin.Roles.RoleName, admin.Roles.IsSuperAdmin },
            GroupId = admin.GroupId,
            LastLoginTime = admin.LastLoginTime,
            LastLoginIP = admin.LastLoginIP,
            ThisLoginTime = admin.ThisLoginTime,
            ThisLoginIP = admin.ThisLoginIP,
            IsLock = admin.IsLock
        };

        internal static object ToSafeMemberDto(Member member) => new
        {
            Id = member.Id,
            UserName = member.UserName,
            RealName = member.RealName,
            Nickname = member.Nickname,
            Tel = member.Tel,
            Email = member.Email,
            RoleId = member.RoleId,
            Roles = member.Roles == null ? null : new { member.Roles.Id, member.Roles.RoleName },
            LastLoginTime = member.LastLoginTime,
            LastLoginIP = member.LastLoginIP,
            ThisLoginTime = member.ThisLoginTime,
            ThisLoginIP = member.ThisLoginIP,
            RegTime = member.RegTime,
            Balance = member.Balance,
            GiftBalance = member.GiftBalance,
            Rebate = member.Rebate,
            TotalCredits = member.TotalCredits,
            IsLock = member.IsLock,
            IsVerifySellers = member.IsVerifySellers
        };

        private static void RevokeMemberSessions(int memberId)
        {
            var now = DateTime.UtcNow;
            var sessions = AuthSession.FindAll(
                AuthSession._.SubjectType == "member" &
                AuthSession._.SubjectId == memberId &
                AuthSession._.IsRevoked == 0);
            foreach (var session in sessions)
            {
                session.IsRevoked = 1;
                session.RevokedUtc = now;
                session.Update();
            }
        }
        #endregion
    }
}
