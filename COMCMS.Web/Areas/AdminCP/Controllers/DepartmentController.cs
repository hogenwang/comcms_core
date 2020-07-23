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

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    public class DepartmentController : AdminBaseController
    {
        #region 部门管理
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
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
            //获取上级栏目
            IList<Department> list = Department.GetListTree(0, 0, true, false);
            ViewBag.ListTree = list;
            Department entity = new Department();
            Core.Admin.WriteLogActions("查看添加部门页面；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("add", "department", "JSON")]
        public IActionResult AddDepartment(Department model)
        {
            //获取上级栏目
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
        //编辑部门
        [HttpGet]
        [MyAuthorize("edit", "department")]
        public IActionResult EditDepartment(int id)
        {
            Department entity = Department.Find(Department._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            //获取上级栏目
            IList<Department> list = Department.GetListTree(0, 0, true, false);
            ViewBag.ListTree = list;
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
                Json(tip);
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
                Json(tip);
            }
            //赋值
            
            entity.DepartmentName = model.DepartmentName;
            entity.Pic = model.Pic;
            entity.DepartmentDescription = model.DepartmentDescription;
            entity.Rank = model.Rank;
            entity.Pic = model.Pic;
            entity.Tel = model.Tel;
            entity.Fax = model.Fax;
            entity.Email = model.Email;
            entity.IsNotAllowDel = Request.Form["IsNotAllowDel"] == "1" ? 1 : 0;
            //如果修改了路径
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

        //删除栏目
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
            if (Department.FindCount(Department._.PId == entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "部门目有下级部门，不允许删除！";
                return Json(tip);
            }
            //删除文章

            Core.Admin.WriteLogActions("删除部门(id:" + entity.Id + ");");
            entity.IsDel =1;
            entity.Update();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除部门成功";
            return Json(tip);
        }
        #endregion
    }
}
