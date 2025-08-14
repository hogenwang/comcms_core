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
    public class DataDictionaryController : AdminBaseController
    {
        #region 数据字典首页
        [HttpGet]
        [MyAuthorize("viewlist", "datadictionary")]
        public IActionResult Index()
        {
            Admin.WriteLogActions($"查看数据字典页面;");
            return View();
        }
        #endregion

        #region 异步获取数据字典列表，带分页
        [MyAuthorize("viewlist", "datadictionary", "JSON")]
        public IActionResult GetDataDictionaryList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = new Expression();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (DataDictionary._.Id == int.Parse(keyword) | DataDictionary._.Key.Contains(keyword));
                }
                else
                {
                    ex &= DataDictionary._.Key.Contains(keyword);
                }
            }
            IList<DataDictionary> list = DataDictionary.FindAll(ex, DataDictionary._.Rank.Asc().And(DataDictionary._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = DataDictionary.FindCount(ex, DataDictionary._.Rank.Asc().And(DataDictionary._.Id.Desc()), null, startRowIndex, numPerPage);
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            return Json(new { total = totalCount, rows = list });
        }
        #endregion

        #region 获取数据字典
        [HttpPost]
        [MyAuthorize("edit", "datadictionary", "JSON")]
        public IActionResult GetDataDictionary(int id)
        {
            var entity = DataDictionary.Find(DataDictionary._.Id == id);
            if(entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            var detail = new
            {
                id = entity.Id,
                rank = entity.Rank,
                description = entity.Description,
                key = entity.Key
            };
            tip.Detail = detail;
            tip.Message = "获取成功！";
            tip.Status = JsonTip.SUCCESS;
            Admin.WriteLogActions($"查看数据字典（{entity.Id}）;");
            return Json(tip);
        }
        #endregion

        #region 保存（添加或者修改）数据字典
        [HttpPost]
        [MyAuthorize("add", "datadictionary", "JSON")]
        public IActionResult DoSaveDataDictionary(DataDictionary model)
        {
            if (string.IsNullOrEmpty(model.Key))
            {
                tip.Message = "请输入字典Key";
                return Json(tip);
            }
            if(model.Id==0 && DataDictionary.FindCount(DataDictionary._.Key== model.Key, null, null, 0, 0) > 0)
            {
                tip.Message = "此字典Key已经存在，请填写其他的！";
                return Json(tip);
            }
            if(model.Id> 0 && DataDictionary.FindCount(DataDictionary._.Key == model.Key & DataDictionary._.Id !=model.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "此字典Key已经存在，请填写其他的！";
                return Json(tip);
            }
            DataDictionary entity = null;
            string tipMessage = "";
            if (model.Id > 0)
            {
                entity = DataDictionary.Find(DataDictionary._.Id == model.Id);
                if(entity == null)
                {
                    tip.Message = "系统找不到本记录！";
                    return Json(tip);
                }
                tipMessage = "修改";
            }
            else
            {
                entity = new DataDictionary();
                entity.AddTime = DateTime.Now;
                tipMessage = "添加";
            }
            entity.Key = model.Key;
            entity.Description = model.Description;
            entity.Rank = model.Rank;
            entity.Save();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = $"{tipMessage}数据字典成功！";
            Admin.WriteLogActions($"{tipMessage}数据字典成功({entity.Id});");
            return Json(tip);
        }
        #endregion

        #region 删除数据字典
        [HttpPost]
        [MyAuthorize("del", "datadictionary", "JSON")]
        public IActionResult DelDataDictionary(int id)
        {
            var entity = DataDictionary.Find(DataDictionary._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            //获取数据字典详情
            IList<DataDictionaryDetail> list = DataDictionaryDetail.FindAll(DataDictionaryDetail._.DataDictionaryId == entity.Id, null, null, 0, 0);
            if (list.Count > 0)
            {
                list.Delete();
            }
            Admin.WriteLogActions($"删除数据字典（{entity.Id}）;");
            entity.Delete();


            tip.Message = "删除数据字典成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion


        #region 获取字典值列表
        [HttpPost]
        [MyAuthorize("edit", "datadictionary", "JSON")]
        public IActionResult GetDataDictionaryDetail(int id)
        {
            var entity = DataDictionary.Find(DataDictionary._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            //获取数据字典详情
            IList<DataDictionaryDetail> list = DataDictionaryDetail.FindAll(DataDictionaryDetail._.DataDictionaryId == entity.Id, DataDictionaryDetail._.Rank.Asc().And(DataDictionaryDetail._.Id.Desc()), null, 0, 0);

            List<object> detail = new List<object>();

            foreach (var item in list)
            {
                detail.Add(new
                {
                    dataDictionaryId = item.DataDictionaryId,
                    id = item.Id,
                    val = item.Val,
                    title = item.Title,
                    description = item.Description,
                    rank = item.Rank,
                    isDefault = item.IsDefault,
                    addTime = item.AddTime.ToString("yyyy-MM-dd HH:mm")
                });
            }
            tip.Detail = detail;
            tip.Message = "获取成功！";
            tip.Status = JsonTip.SUCCESS;
            Admin.WriteLogActions($"查看数据字典值列表（{entity.Id}）;");
            return Json(tip);
        }
        #endregion

        #region 保存字典值详情
        [HttpPost]
        [MyAuthorize("add", "datadictionary", "JSON")]
        public IActionResult DoSaveDataDictionaryDetail(DataDictionaryDetail model)
        {
            if (string.IsNullOrEmpty(model.Val))
            {
                tip.Message = "请输入字典值";
                return Json(tip);
            }
            if(model.DataDictionaryId == 0)
            {
                tip.Message = "请先选择一个字典进行查看后再编辑或者添加！";
                return Json(tip);
            }
            if (model.Id == 0 && DataDictionaryDetail.FindCount(DataDictionaryDetail._.Val == model.Val & DataDictionaryDetail._.DataDictionaryId == model.DataDictionaryId, null, null, 0, 0) > 0)
            {
                tip.Message = "此字典值已经存在，请填写其他的！";
                return Json(tip);
            }
            if (model.Id > 0 && DataDictionaryDetail.FindCount(DataDictionaryDetail._.Val == model.Val & DataDictionaryDetail._.DataDictionaryId == model.DataDictionaryId & DataDictionaryDetail._.Id != model.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "此字典值已经存在，请填写其他的！";
                return Json(tip);
            }
            DataDictionaryDetail entity = null;
            string tipMessage = "";
            if (model.Id > 0)
            {
                entity = DataDictionaryDetail.Find(DataDictionaryDetail._.Id == model.Id);
                if (entity == null)
                {
                    tip.Message = "系统找不到本记录！";
                    return Json(tip);
                }
                tipMessage = "修改";
            }
            else
            {
                entity = new DataDictionaryDetail();
                entity.AddTime = DateTime.Now;
                entity.DataDictionaryId = model.DataDictionaryId;
                tipMessage = "添加";
            }
            entity.Val = model.Val;
            entity.Description = model.Description;
            entity.Title = model.Title;
            entity.IsDefault = model.IsDefault;
            entity.Rank = model.Rank;
            entity.Save();

            tip.Status = JsonTip.SUCCESS;
            tip.Message = $"{tipMessage}数据字典成功！";
            Admin.WriteLogActions($"{tipMessage}数据字典成功({entity.Id});");
            return Json(tip);
        }
        #endregion

        #region 删除数据字典
        [HttpPost]
        [MyAuthorize("del", "datadictionary", "JSON")]
        public IActionResult DelDataDictionaryDetail(int id)
        {
            var entity = DataDictionaryDetail.Find(DataDictionaryDetail._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }

            Admin.WriteLogActions($"删除数据字典详情（{entity.Id}）;");
            entity.Delete();

            tip.Message = "删除数据字典值详情成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion
    }
}
