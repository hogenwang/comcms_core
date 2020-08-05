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

    }
}
