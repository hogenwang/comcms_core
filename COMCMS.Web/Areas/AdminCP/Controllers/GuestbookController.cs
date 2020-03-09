using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using Newtonsoft.Json;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("留言板")]
    public class GuestbookController : AdminBaseController
    {
        #region 留言板分类
        [MyAuthorize("viewlist", "guestbookkinds")]
        [DisplayName("留言分类")]
        public IActionResult GuestbookCategorys()
        {
            IList<GuestbookCategory> list = GuestbookCategory.FindAll(null, GuestbookCategory._.Rank.Asc(), null, 0, 0);
            Core.Admin.WriteLogActions("查看留言分类列表页面；");
            return View(list);
        }
        //添加
        [HttpPost]
        [MyAuthorize("add", "guestbookkinds", "JSON")]
        public IActionResult AddGuestbookCategory(GuestbookCategory model)
        {
            if (string.IsNullOrEmpty(model.KindName))
            {
                tip.Message = "请输入留言分类标题！";
                return Json(tip);
            }
            model.Insert();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加成功！";
            Core.Admin.WriteLogActions("添加留言分类(" + model.Id + ")；");
            return Json(tip);
        }

        //执行编辑操作
        [HttpPost]
        [MyAuthorize("edit", "guestbookkinds", "JSON")]
        public IActionResult EditGuestbookCategory(GuestbookCategory model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            GuestbookCategory oldentity = GuestbookCategory.Find(GuestbookCategory._.Id == model.Id);
            if (oldentity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(model.KindName))
            {
                tip.Message = "请输入留言分类标题！";
                return Json(tip);
            }
            model.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑留言分类成功";
            Core.Admin.WriteLogActions("编辑留言分类(" + model.Id + ")；");
            return Json(tip);
        }
        //执行删除操作
        [HttpPost]
        [MyAuthorize("del", "guestbookkinds", "JSON")]
        public JsonResult DelGuestbookCategory(int id)
        {
            GuestbookCategory entity = GuestbookCategory.Find(GuestbookCategory._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本留言分类！";
                return Json(tip);
            }
            //删除栏目下所有留言
            IList<Guestbook> list = Guestbook.FindAll(Guestbook._.KId == entity.Id, null, null, 0, 0);
            if (list != null && list.Count > 0)
            {
                list.Delete();
            }
            Core.Admin.WriteLogActions("删除留言分类(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除留言分类成功";
            return Json(tip);
        }
        #endregion

        #region 留言列表
        [MyAuthorize("viewlist", "guestbook")]
        [DisplayName("留言列表")]
        public IActionResult GuestbookList()
        {
            IList<GuestbookCategory> list = GuestbookCategory.FindAll(null, GuestbookCategory._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            return View();
        }

        [MyAuthorize("viewlist", "guestbook","JSON")]
        public IActionResult GetGuesbookList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Guestbook._.Id > 0;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (Guestbook._.Id == int.Parse(keyword) | Guestbook._.Title.Contains(keyword));
                }
                else
                {
                    ex &= Guestbook._.Title.Contains(keyword);
                }
            }
            string kid = Request.Query["kid"];
            if (Utils.IsInt(kid) && int.Parse(kid) > 0)
            {
                ex &= Guestbook._.KId == int.Parse(kid);
            }
            IList<Guestbook> list = Guestbook.FindAll(ex, Guestbook._.Id.Desc(), null, startRowIndex, numPerPage);
            long totalCount = Guestbook.FindCount(ex, Guestbook._.Id.Desc(), null, startRowIndex, numPerPage);
            Core.Admin.WriteLogActions("查看留言列表(page:" + page + ");");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
        }

        #endregion

        #region 编辑留言
        [MyAuthorize("edit", "guestbook")]
        public IActionResult EditGuestbook(int id)
        {
            IList<GuestbookCategory> list = GuestbookCategory.FindAll(GuestbookCategory._.Id > 0, GuestbookCategory._.Rank.Asc(), null, 0, 0);
            ViewBag.ListKinds = list;
            Guestbook entity = Guestbook.Find(Guestbook._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            if (entity.IsRead != 1)
            {
                entity.IsRead = 1;
                entity.Update();
            }

            Core.Admin.WriteLogActions("查看/回复留言(" + id + ");");

            return View(entity);
        }
        [HttpPost]
        [MyAuthorize("edit", "guestbook", "JSON")]
        public IActionResult EditGuestbook(IFormCollection fc)
        {
            string id = fc["Id"];
            if (!Utils.IsInt(id) || int.Parse(id) <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }

            Guestbook entity = Guestbook.FindById(int.Parse(id));
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            string kid = fc["KId"];
            if (Utils.IsInt(kid) && int.Parse(kid) != entity.KId)
            {
                entity.KId = int.Parse(kid);
            }
            string isverify = fc["IsVerify"];
            if (!string.IsNullOrEmpty(isverify) && isverify == "1")
            {
                entity.IsVerify = 1;
            }
            else
                entity.IsVerify = 0;
            string replayuser = fc["ReplyUser"];
            entity.ReplyNickName = replayuser;
            string reply = fc["Reply"];
            entity.ReplyContent = reply;
            if (!string.IsNullOrEmpty(entity.ReplyContent) && entity.ReplyAdminId == 0)
            {
                entity.ReplyAdminId = Core.Admin.GetMyInfo().Id;
                entity.ReplyIP = Utils.GetIP();
                entity.ReplyAddTime = DateTime.Now;
            }
            entity.Update();
            Core.Admin.WriteLogActions("编辑审核留言(id:" + entity.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑/审核留言详情成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 删除留言
        //删除文章
        [HttpPost]
        [MyAuthorize("del", "guestbook", "JSON")]
        public JsonResult DelGuestbook(int id)
        {
            Guestbook entity = Guestbook.Find(Guestbook._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本留言！";
                return Json(tip);
            }

            Core.Admin.WriteLogActions("删除留言(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除留言成功";
            return Json(tip);
        }
        #endregion
    }
}