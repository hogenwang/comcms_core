using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using COMCMS.Web.Common;
using XCode;

namespace COMCMS.Web.Controllers
{
    public class TagController : HomeBaseController
    {
        #region Tag首页
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            int numPerPage, currentPage, startRowIndex;
            numPerPage = 40;
            if (page > 0)
                currentPage = page;
            else
                currentPage = 1;
            startRowIndex = (currentPage - 1) * numPerPage;
            var where = Tags._.Counts > 0;

            long totalCount = Tags.FindCount(where, null, null, startRowIndex, numPerPage);
            IList<Tags> list = new List<Tags>();
            if (totalCount > 0)
            {
                list = Tags.FindAll(where, Tags._.Id.Desc(), null, startRowIndex, numPerPage);
            }

            int pageCount = (int)totalCount / numPerPage;//总页数
            if (totalCount % numPerPage > 0)
            {
                pageCount += 1;
            }
            //ViewBag.list = list;//列表
            //分页信息
            ViewBag.totalCount = totalCount;
            ViewBag.pageCount = pageCount;
            ViewBag.page = page;
            ViewBag.pagesize = numPerPage;
            ViewBag.cfg = cfg;
            return View(list);
        }
        #endregion

        #region Tag标签列表页面
        [HttpGet]
        [Route("tag/{name}")]
        [Route("tag/{name}/{page:int}")]
        public IActionResult TagList(string name, int page = 1)
        {
            Tags entity = Tags.Find(Tags._.TagName == name);
            if(entity == null)
            {
                return EchoTip("系统找不到本标签！");
            }

            int numPerPage, currentPage, startRowIndex;
            numPerPage = 20;
            if (page > 0)
                currentPage = page;
            else
                currentPage = 1;

            if(currentPage == 1)
            {
                entity.Hits++;
                entity.Update();
            }
            startRowIndex = (currentPage - 1) * numPerPage;
            var where = TagsDetail._.TagName == entity.TagName;

            long totalCount = TagsDetail.FindCount(where, null, null, startRowIndex, numPerPage);
            IList<TagsDetail> list = new List<TagsDetail>();
            if (totalCount > 0)
            {
                list = TagsDetail.FindAll(where, TagsDetail._.Id.Desc(), null, startRowIndex, numPerPage);
            }

            int pageCount = (int)totalCount / numPerPage;//总页数
            if (totalCount % numPerPage > 0)
            {
                pageCount += 1;
            }
            ViewBag.list = list;//列表
            //分页信息
            ViewBag.totalCount = totalCount;
            ViewBag.pageCount = pageCount;
            ViewBag.page = page;
            ViewBag.pagesize = numPerPage;
            ViewBag.cfg = cfg;
            //IList<Tags> list = Tags.FindAll()
            return View(entity);
        }
        #endregion

    }
}
