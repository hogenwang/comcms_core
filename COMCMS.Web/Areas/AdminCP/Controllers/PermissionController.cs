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
using Microsoft.AspNetCore.Http;
using System.IO;
using NewLife.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Text;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [Area("AdminCP")]
    [DisplayName("后台权限")]
    public class PermissionController : AdminBaseController
    {
        private IWebHostEnvironment _env;
        public PermissionController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region 事件管理
        /// <summary>
        /// 目标事件
        /// </summary>
        /// <returns></returns>
        [MyAuthorize( "view",  "eventkey")]
        public IActionResult EventKey()
        {
            IList<TargetEvent> list = TargetEvent.FindAll(TargetEvent._.Id > 0, TargetEvent._.Id.Asc(), null, 0, 0);
            return View(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [MyAuthorize( "viewlist",  "eventkey",  "JSON")]
        public IActionResult GetEventKey(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;
            long totalCount = 0;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = TargetEvent._.Id > 0;

            if (!string.IsNullOrWhiteSpace(keyword))
                ex &= TargetEvent._.EventName.Contains(keyword);


            IList<TargetEvent> list = TargetEvent.FindAll(ex, TargetEvent._.Id.Asc(), null, startRowIndex, numPerPage);
            totalCount = TargetEvent.FindCount(ex, TargetEvent._.Id.Asc(), null, startRowIndex, numPerPage);
            Admin.WriteLogActions($"查看后台事件列表（keyword:{keyword};page:{page};limit:{limit}）;");
            return Content(JsonConvert.SerializeObject(new { total = totalCount, rows = list.ToList() }), "text/json");
        }

        //添加eventkey
        [MyAuthorize( "add",  "eventkey")]
        public IActionResult AddEventKey()
        {
            Admin.WriteLogActions($"查看添加后台事件页面;");
            return View();
        }

        [MyAuthorize( "add",  "eventkey",  "JSON")]
        [HttpPost]
        public IActionResult AddEventKey(IFormCollection fc)
        {
            TargetEvent entity = new TargetEvent();
            entity.EventKey = fc["EventKey"];
            entity.EventName = fc["EventName"];
            string rank = fc["Rank"];
            if (string.IsNullOrWhiteSpace(rank) || !Utils.IsInt(rank)) rank = "0";
            entity.Rank = int.Parse(rank);
            if (string.IsNullOrWhiteSpace(fc["IsDisable"]) || fc["IsDisable"] != "1")
                entity.IsDisable = 0;
            else
                entity.IsDisable = 1;

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
            entity.EventKey = entity.EventKey.ToLower();

            if (TargetEvent.FindCount(TargetEvent._.EventKey == entity.EventKey.ToLower(), null, null, 0, 0) > 0)
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
        #endregion

        #region 后台栏目管理
        //后台菜单列表
        [MyAuthorize( "viewlist",  "adminmenu")]
        [DisplayName("后台栏目")]
        public IActionResult AdminMenuList()
        {
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, false, true);
            return View(list);
        }
        //添加后台菜单
        [MyAuthorize( "add",  "adminmenu")]
        public IActionResult AddAdminMenu()
        {
            //获取上级栏目
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.IsDisable != 1, TargetEvent._.Rank.Asc(), null, 0, 0);
            ViewBag.ListEvent = listevent;
            return View();
        }
        //执行添加菜单
        [HttpPost]
        [MyAuthorize( "add",  "adminmenu",  "JSON")]
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
            //判断
            if (AdminMenu.FindCount(AdminMenu._.MenuKey == model.MenuKey, null, null, 0, 0) > 0)
            {
                tip.Message = "菜单KEY已经存在，请填写其他的！";
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
                            AdminMenuEvent tmp = new AdminMenuEvent();
                            tmp.EventId = te.Id;
                            tmp.EventKey = te.EventKey;
                            tmp.EventName = te.EventName;
                            tmp.MenuKey = model.MenuKey;
                            listme.Add(tmp);
                        }
                    }
                }
            }
            model.Insert();
            if (listme != null && listme.Count > 0)
            {
                listme.ForEach(s =>
                {
                    s.MenuId = model.Id;
                });
                listme.Insert();
            }
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加后台菜单成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        //修改菜单
        public IActionResult EditAdminMenu(int id)
        {
            AdminMenu entity = AdminMenu.Find(AdminMenu._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            //获取上级栏目
            IList<AdminMenu> list = AdminMenu.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.IsDisable != 1, TargetEvent._.Rank.Asc(), null, 0, 0);
            ViewBag.ListEvent = listevent;

            return View(entity);
        }

        //执行添加菜单
        [HttpPost]
        [MyAuthorize( "edit",  "adminmenu",  "JSON")]
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
            //赋值
            entity.MenuName = model.MenuName;
            //如果key修改了。判断是否有存在
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
                //重新获取level和location
                entity.Level = AdminMenu.GetNewLevel(model.PId);
                entity.Location = AdminMenu.GetNewLocation(model.PId);
            }
            entity.PId = model.PId;
            entity.Update();

            //处理权限
            IList<AdminMenuEvent> oldevent = AdminMenuEvent.FindAll(AdminMenuEvent._.MenuId == entity.Id, null, null, 0, 0);

            //所有的权限
            IList<TargetEvent> listevent = TargetEvent.FindAll(TargetEvent._.Id > 0, TargetEvent._.Rank.Asc(), null, 0, 0);
            //新的权限
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
                            AdminMenuEvent tmp = new AdminMenuEvent();
                            tmp.EventId = te.Id;
                            tmp.EventKey = te.EventKey;
                            tmp.EventName = te.EventName;
                            tmp.MenuKey = model.MenuKey;
                            tmp.MenuId = entity.Id;
                            listme.Add(tmp);
                        }
                    }
                }
            }

            //判断是否在旧的里面
            listme.ForEach(s =>
            {
                var oe = oldevent.FirstOrDefault(o => o.EventId == s.EventId);
                //如果是旧列表存在，则删掉旧列表里面的，然后更新自己
                if (oe != null)
                {
                    s.Id = oe.Id;
                    s.Update();
                    oldevent.Remove(oe);
                }
                else
                {
                    //如果不存在，则是新增的。直接新增处理
                    s.Insert();
                }
            });
            //如果还有，则删除旧列表里面的
            if (oldevent != null && oldevent.Count > 0)
            {
                oldevent.Delete();
            }

            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑后台菜单成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        //删除菜单
        [HttpPost]
        [MyAuthorize( "del",  "adminmenu",  "JSON")]
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
        #endregion

        #region 系统运行日志
        [HttpGet]
        [MyAuthorize("viewlist", "runlogs")]
        [DisplayName("系统运行日志")]
        public IActionResult RunLogs()
        {
            string logPath = $"{_env.ContentRootPath}{Path.DirectorySeparatorChar}Log{Path.DirectorySeparatorChar}";
            List<string> lognames = new List<string>();
            try
            {
                if (Directory.Exists(logPath))
                {
                    DirectoryInfo directory = new DirectoryInfo(logPath);
                    List<string> listFiles = new List<string>();
                    DirectoryInfo[] directorys = directory.GetDirectories();
                    FileInfo[] files;
                    files = directory.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        lognames.Add(file.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
            lognames.Reverse();//倒序排列一下
            ViewBag.logPath = logPath;
            return View(lognames);
        }
        #endregion

        #region 获取日志详情
        [HttpPost]
        [MyAuthorize("viewlist", "runlogs")]
        public async Task<IActionResult> GetRunLogDetail(string logname)
        {
            if (string.IsNullOrEmpty(logname))
            {
                return Content("错误日志名称！");
            }
            string logPath = $"{_env.ContentRootPath}{Path.DirectorySeparatorChar}Log{Path.DirectorySeparatorChar}";
            string logFullPath = logPath + logname;


            if (!System.IO.File.Exists(logFullPath))
            {
                return Content("系统找不到本日志文件！");
            }
            //XTrace.WriteLine("查看日志：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            IFileProvider fileProvider = new PhysicalFileProvider(logPath);
            StringBuilder detail = new StringBuilder();
            using (Stream stream = fileProvider.GetFileInfo(logname).CreateReadStream())
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                detail.Append(Encoding.Default.GetString(buffer));
            }


            //using (StreamReader fs = new StreamReader(logFullPath))
            //{
            //    detail.Append(fs.ReadToEndAsync());
            //}

            //string detail = await System.IO.File.ReadAllTextAsync(logFullPath);
            return Content(detail.ToString());
        }
        #endregion
    }
}