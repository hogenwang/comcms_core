using System;
using System.Collections.Generic;
using COMCMS.Core;
using COMCMS.Web.Common;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using XCode;

namespace COMCMS.Web.Controllers
{
    public class TagController : HomeBaseController
    {
        private readonly HitCounterService _hitCounter;

        public TagController(HitCounterService hitCounter)
        {
            _hitCounter = hitCounter;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            int numPerPage = 40;
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;
            var where = Tags._.Counts > 0;

            long totalCount = Tags.FindCount(where, null, null, 0, 0);
            IList<Tags> list = totalCount > 0
                ? Tags.FindAll(where, Tags._.Id.Desc(), null, startRowIndex, numPerPage)
                : new List<Tags>();

            int pageCount = (int)((totalCount + numPerPage - 1) / numPerPage);
            ViewBag.totalCount = totalCount;
            ViewBag.pageCount = pageCount;
            ViewBag.page = currentPage;
            ViewBag.pagesize = numPerPage;
            ViewBag.cfg = cfg;
            return View(list);
        }

        [HttpGet]
        [Route("tag/{name}")]
        [Route("tag/{name}/{page:int}")]
        public IActionResult TagList(string name, int page = 1)
        {
            Tags entity = Tags.Find(Tags._.TagName == name);
            if (entity == null)
                return EchoTip("系统找不到本标签！");

            int numPerPage = 20;
            int currentPage = Math.Max(page, 1);

            if (currentPage == 1)
                _hitCounter.IncrementTagHit(entity.Id);

            int startRowIndex = (currentPage - 1) * numPerPage;
            var where = TagsDetail._.TagName == entity.TagName;

            long totalCount = TagsDetail.FindCount(where, null, null, 0, 0);
            IList<TagsDetail> list = totalCount > 0
                ? TagsDetail.FindAll(where, TagsDetail._.Id.Desc(), null, startRowIndex, numPerPage)
                : new List<TagsDetail>();

            int pageCount = (int)((totalCount + numPerPage - 1) / numPerPage);
            ViewBag.list = list;
            ViewBag.totalCount = totalCount;
            ViewBag.pageCount = pageCount;
            ViewBag.page = currentPage;
            ViewBag.pagesize = numPerPage;
            ViewBag.cfg = cfg;
            return View(entity);
        }
    }
}
