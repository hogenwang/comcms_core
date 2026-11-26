using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewLife.Log;
using Newtonsoft.Json;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    [DisplayName("后台权限")]
    public class PermissionController : AdminBaseController
    {
        private const int MaxLogReadBytes = 2 * 1024 * 1024; // 2 MiB 尾部上限，防止大日志 OOM
        private static readonly Regex SafeLogFileNameRegex = new(@"^[a-zA-Z0-9_\-\.]+\.(log|txt)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));

        private readonly IWebHostEnvironment _env;

        public PermissionController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region 事件管理
        [MyAuthorize("view", "eventkey")]
        public IActionResult EventKey()
        {
            IList<TargetEvent> list = TargetEvent.FindAll(TargetEvent._.Id > 0, TargetEvent._.Id.Asc(), null, 0, 0);
            return View(list);
        }

        [MyAuthorize("viewlist", "eventkey", "JSON")]
        public IActionResult GetEventKey(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;

            Expression ex = TargetEvent._.Id > 0;
            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= TargetEvent._.EventName.Contains(keyword);

            IList<TargetEvent> list = TargetEvent.FindAll(ex, TargetEvent._.Id.Asc(), null, startRowIndex, numPerPage);
            long totalCount = TargetEvent.FindCount(ex, TargetEvent._.Id.Asc(), null, startRowIndex, numPerPage);
            Admin.WriteLogActions($"查看后台事件列表（page:{currentPage};limit:{numPerPage}）;");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list.ToList() }), "application/json");
        }

        [MyAuthorize("add", "eventkey")]
        public IActionResult AddEventKey()
        {
            Admin.WriteLogActions("查看添加后台事件页面;");
            return View();
        }

        [MyAuthorize("add", "eventkey", "JSON")]
        [HttpPost]
        public IActionResult AddEventKey(IFormCollection fc)
        {
            TargetEvent entity = new TargetEvent
            {
                EventKey = fc["EventKey"],
                EventName = fc["EventName"]
            };
            string rank = fc["Rank"];
            if (string.IsNullOrWhiteSpace(rank) || !Utils.IsInt(rank)) rank = "0";
            entity.Rank = int.Parse(rank);
            entity.IsDisable = (!string.IsNullOrWhiteSpace(fc["IsDisable"]) && fc["IsDisable"] == "1") ? 1 : 0;

            if (string.IsNullOrWhiteSpace(entity.EventKey))
            {
                tip.Message = "请填写事件KEY";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(entity.EventName))
            {
                tip.Message = "请填写事件名称";
                return Json(tip);
            }
            entity.EventKey = entity.EventKey.Trim().ToLowerInvariant();

            if (TargetEvent.FindCount(TargetEvent._.EventKey == entity.EventKey, null, null, 0, 0) > 0)
            {
                tip.Message = "事件key已经存在，请重新填写一个";
                return Json(tip);
            }
            entity.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加事件成功";
            tip.ReturnUrl = "close";

            Core.Admin.WriteLogActions("添加事件详情(id:" + entity.Id + ")；");
            return Json(tip);
        }

        #region 编辑事件详情
        [HttpGet]
        [MyAuthorize("edit", "eventkey")]
        public IActionResult EditEventKey(int id)
        {
            TargetEvent entity = TargetEvent.Find(TargetEvent._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Admin.WriteLogActions("查看/编辑事件详情(id:" + entity.Id + ")；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "eventkey", "JSON")]
        public IActionResult EditEventKey(TargetEvent model)
        {
            if (string.IsNullOrWhiteSpace(model.EventKey))
            {
                tip.Message = "请填写事件KEY";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.EventName))
            {
                tip.Message = "请填写事件名称";
                return Json(tip);
            }
            TargetEvent entity = TargetEvent.Find(TargetEvent._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (entity.EventKey != model.EventKey)
            {
                if (TargetEvent.FindCount(TargetEvent._.EventKey == model.EventKey, null, null, 0, 0) > 0)
                {
                    tip.Message = "事件key已经存在，请重新填写一个";
                    return Json(tip);
                }
            }
            entity.EventKey = model.EventKey;
            entity.EventName = model.EventName;
            entity.Rank = model.Rank;
            entity.IsDisable = (!string.IsNullOrWhiteSpace(Request.Form["IsDisable"]) && Request.Form["IsDisable"] == "1") ? 1 : 0;
            entity.Update();
            Admin.WriteLogActions("编辑事件详情(id:" + entity.Id + ")；");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑事件权限详情成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 删除事件权限
        [HttpPost]
        [MyAuthorize("del", "eventkey", "JSON")]
        public JsonResult DelEventKey(int id)
        {
            TargetEvent entity = TargetEvent.Find(TargetEvent._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            Admin.WriteLogActions($"删除事件详情(id:{entity.Id},key:{entity.EventKey})；");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除事件权限详情成功";
            return Json(tip);
        }
        #endregion
        #endregion

        #region 后台栏目管理
        [MyAuthorize("viewlist", "adminmenu")]
        [DisplayName("后台栏目")]
        public IActionResult AdminMenuList()
        {
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, true);
            return View(list);
        }

        [MyAuthorize("add", "adminmenu")]
        public IActionResult AddAdminMenu()
        {
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.IsDisable != 1, TargetEvent._.Rank.Asc(), null, 0, 0);
            ViewBag.ListEvent = listevent;
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "adminmenu", "JSON")]
        public IActionResult AddAdminMenu(AdminMenu model)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                tip.Message = messages;
                return Json(tip);
            }
            if (AdminMenu.FindCount(AdminMenu._.MenuKey == model.MenuKey, null, null, 0, 0) > 0)
            {
                tip.Message = "菜单KEY已经存在，请填写其他的！";
                return Json(tip);
            }

            IList<TargetEvent> listevent = TargetEvent.FindAll(null, TargetEvent._.Rank.Asc(), null, 0, 0);
            string[] eventkeys = Request.Form["eventkey"];
            List<AdminMenuEvent> listme = new List<AdminMenuEvent>();
            if (eventkeys != null && eventkeys.Length > 0)
            {
                foreach (string s in eventkeys)
                {
                    if (Utils.IsInt(s))
                    {
                        TargetEvent te = listevent.FirstOrDefault(t => t.Id == int.Parse(s));
                        if (te != null)
                        {
                            listme.Add(new AdminMenuEvent
                            {
                                EventId = te.Id,
                                EventKey = te.EventKey,
                                EventName = te.EventName,
                                MenuKey = model.MenuKey
                            });
                        }
                    }
                }
            }
            model.Insert();
            if (listme.Count > 0)
            {
                listme.ForEach(s => s.MenuId = model.Id);
                listme.Insert();
            }
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加后台菜单成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [HttpGet]
        [MyAuthorize("edit", "adminmenu")]
        public IActionResult EditAdminMenu(int id)
        {
            AdminMenu entity = AdminMenu.Find(AdminMenu._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.IsDisable != 1, TargetEvent._.Rank.Asc(), null, 0, 0);
            ViewBag.ListEvent = listevent;

            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "adminmenu", "JSON")]
        public IActionResult EditAdminMenu(AdminMenu model)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                tip.Message = messages;
                return Json(tip);
            }
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            AdminMenu entity = AdminMenu.Find(AdminMenu._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.MenuName = model.MenuName;
            if (entity.MenuKey != model.MenuKey)
            {
                if (AdminMenu.FindCount(AdminMenu._.MenuKey == model.MenuKey & AdminMenu._.Id != entity.Id, null, null, 0, 0) > 0)
                {
                    tip.Message = "您修改的菜单KEY已经存在，请填写其他的！";
                    return Json(tip);
                }
                entity.MenuKey = model.MenuKey;
            }
            entity.Link = model.Link;
            entity.IsHide = model.IsHide;
            entity.ClassName = model.ClassName;
            entity.Rank = model.Rank;
            if (entity.PId != model.PId)
            {
                entity.Level = AdminMenu.GetNewLevel(model.PId);
                entity.Location = AdminMenu.GetNewLocation(model.PId);
            }
            entity.PId = model.PId;
            entity.Update();

            IList<AdminMenuEvent> oldevent = AdminMenuEvent.FindAll(AdminMenuEvent._.MenuId == entity.Id, null, null, 0, 0);
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.Id > 0, TargetEvent._.Rank.Asc(), null, 0, 0);
            string[] eventkeys = Request.Form["eventkey"];
            List<AdminMenuEvent> listme = new List<AdminMenuEvent>();
            if (eventkeys != null && eventkeys.Length > 0)
            {
                foreach (string s in eventkeys)
                {
                    if (Utils.IsInt(s))
                    {
                        TargetEvent te = listevent.FirstOrDefault(t => t.Id == int.Parse(s));
                        if (te != null)
                        {
                            listme.Add(new AdminMenuEvent
                            {
                                EventId = te.Id,
                                EventKey = te.EventKey,
                                EventName = te.EventName,
                                MenuKey = model.MenuKey,
                                MenuId = entity.Id
                            });
                        }
                    }
                }
            }

            listme.ForEach(s =>
            {
                var oe = oldevent.FirstOrDefault(o => o.EventId == s.EventId);
                if (oe != null)
                {
                    s.Id = oe.Id;
                    s.Update();
                    oldevent.Remove(oe);
                }
                else
                {
                    s.Insert();
                }
            });
            if (oldevent != null && oldevent.Count > 0)
            {
                oldevent.Delete();
            }

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑后台菜单成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "adminmenu", "JSON")]
        public IActionResult DelAdminMenu(int id)
        {
            AdminMenu entity = AdminMenu.Find(AdminMenu._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (AdminMenu.FindCount(AdminMenu._.PId == entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "本菜单有下级菜单，不允许删除！";
                return Json(tip);
            }
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除后台菜单成功";
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("edit", "adminmenu", "JSON")]
        public IActionResult DoChangeAdminMenuHide(int id)
        {
            AdminMenu entity = AdminMenu.Find(AdminMenu._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.IsHide = entity.IsHide == 1 ? 0 : 1;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改菜单隐藏状态成功";
            return Json(tip);
        }
        #endregion

        #region 系统运行日志
        [HttpGet]
        [MyAuthorize("viewlist", "runlogs")]
        [DisplayName("系统运行日志")]
        public IActionResult RunLogs()
        {
            string logPath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "Log"));
            List<string> lognames = new List<string>();
            try
            {
                if (Directory.Exists(logPath))
                {
                    DirectoryInfo directory = new DirectoryInfo(logPath);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (SafeLogFileNameRegex.IsMatch(file.Name))
                        {
                            lognames.Add(file.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
            lognames.Reverse();
            ViewBag.logPath = logPath;
            return View(lognames);
        }
        #endregion

        #region 获取日志详情
        [HttpPost]
        [MyAuthorize("viewlist", "runlogs", "JSON")]
        public async Task<IActionResult> GetRunLogDetail(string logname)
        {
            if (!TryResolveLogFile(_env.ContentRootPath, logname, out var logFullPath) || !System.IO.File.Exists(logFullPath))
            {
                return Content("系统找不到本日志文件！", "text/plain", Encoding.UTF8);
            }

            try
            {
                await using var stream = new FileStream(logFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var readLength = (int)Math.Min(stream.Length, MaxLogReadBytes);
                bool truncated = stream.Length > MaxLogReadBytes;
                if (truncated)
                {
                    stream.Seek(-readLength, SeekOrigin.End);
                }
                var buffer = new byte[readLength];
                int offset = 0;
                while (offset < readLength)
                {
                    int n = await stream.ReadAsync(buffer.AsMemory(offset, readLength - offset), HttpContext.RequestAborted);
                    if (n == 0) break;
                    offset += n;
                }

                var text = Encoding.UTF8.GetString(buffer, 0, offset);
                if (truncated)
                {
                    text = $"[日志超过 {MaxLogReadBytes / (1024 * 1024)} MiB，仅展示尾部内容]\r\n" + text;
                }
                return Content(text, "text/plain", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return Content($"读取日志失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。", "text/plain", Encoding.UTF8);
            }
        }

        internal static bool TryResolveLogFile(string contentRootPath, string logName, out string fullPath)
        {
            fullPath = null;
            if (string.IsNullOrWhiteSpace(contentRootPath) || string.IsNullOrWhiteSpace(logName))
                return false;
            if (logName.Contains('/') || logName.Contains('\\') || logName.Contains("..", StringComparison.Ordinal))
                return false;
            if (!SafeLogFileNameRegex.IsMatch(logName))
                return false;

            var logRoot = Path.GetFullPath(Path.Combine(contentRootPath, "Log"));
            var candidate = Path.GetFullPath(Path.Combine(logRoot, logName));
            var relative = Path.GetRelativePath(logRoot, candidate);
            if (Path.IsPathRooted(relative) || relative == ".." || relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                return false;

            fullPath = candidate;
            return true;
        }
        #endregion
    }
}
