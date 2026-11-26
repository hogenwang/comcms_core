using System.Collections.Generic;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class DepartmentController : AdminBaseController
    {
        #region 部门管理
        [MyAuthorize("viewlist", "department")]
        public IActionResult DepartmentList()
        {
            IList<Department> list = Department.GetListTree(0, -1, false, true);
            Core.Admin.WriteLogActions("查看部门列表页面；");
            return View(list);
        }
        #endregion

        #region 添加部门
        [MyAuthorize("add", "department")]
        public IActionResult AddDepartment()
        {
            ViewBag.ListTree = Department.GetListTree(0, 0, true, false);
            Core.Admin.WriteLogActions("查看添加部门页面；");
            return View(new Department());
        }

        [HttpPost]
        [MyAuthorize("add", "department", "JSON")]
        public IActionResult AddDepartment(Department model)
        {
            if (string.IsNullOrWhiteSpace(model.DepartmentName))
            {
                tip.Message = "部门名称不能为空！";
                return Json(tip);
            }

            model.Insert();
            Core.Admin.WriteLogActions("添加部门(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加部门成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 编辑部门
        [HttpGet]
        [MyAuthorize("edit", "department")]
        public IActionResult EditDepartment(int id)
        {
            Department entity = Department.Find(Department._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            ViewBag.ListTree = Department.GetListTree(0, 0, true, false);
            Core.Admin.WriteLogActions("查看/编辑部门（id:" + id + "）页面；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "department", "JSON")]
        public IActionResult EditDepartment(Department model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.DepartmentName))
            {
                tip.Message = "部门名称不能为空！";
                return Json(tip);
            }
            Department entity = Department.Find(Department._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (model.PId == entity.Id)
            {
                tip.Message = "上级部门不能选择自身！";
                return Json(tip);
            }

            entity.DepartmentName = model.DepartmentName;
            entity.Pic = model.Pic;
            entity.DepartmentDescription = model.DepartmentDescription;
            entity.Rank = model.Rank;
            entity.Tel = model.Tel;
            entity.Fax = model.Fax;
            entity.Email = model.Email;
            entity.IsNotAllowDel = Request.Form["IsNotAllowDel"] == "1" ? 1 : 0;
            if (entity.PId != model.PId)
            {
                entity.PId = model.PId;
                entity.Location = Department.GetNewLocation(model.PId);
            }
            entity.Save();

            Core.Admin.WriteLogActions("修改部门(id:" + entity.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改部门成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [HttpPost]
        [MyAuthorize("del", "department", "JSON")]
        public JsonResult DelDepartment(int id)
        {
            Department entity = Department.Find(Department._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本部门！";
                return Json(tip);
            }
            if (entity.IsNotAllowDel == 1)
            {
                tip.Message = "该部门已设为不允许删除！";
                return Json(tip);
            }
            if (Department.FindCount(Department._.PId == entity.Id & Department._.IsDel != 1, null, null, 0, 0) > 0)
            {
                tip.Message = "部门有下级部门，不允许删除！";
                return Json(tip);
            }

            Core.Admin.WriteLogActions("删除部门(id:" + entity.Id + ");");
            entity.IsDel = 1;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除部门成功";
            return Json(tip);
        }
        #endregion
    }
}
