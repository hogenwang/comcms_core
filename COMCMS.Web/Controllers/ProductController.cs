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
    public class ProductController : HomeBaseController
    {
        #region 分类页面
        [HttpGet]
        public IActionResult Index(int id = 0, int page = 1)
        {
            Category entity = null;
            if (id > 0)
            {
                entity = Category.FindById(id);
                if (entity == null)
                {
                    return EchoTip("系统找不到本分类！");
                }
            }
            else
            {
                entity = new Category()
                {
                    Id = 0,
                    KindName = "所有分类",
                    Keyword = "所有分类",
                    Description = "所有分类",
                    PageSize = 10
                };
            }
            Expression ex = Product._.IsHide == 0;
            if (entity.Id > 0)
            {
                ex &= Product._.KId == entity.Id;
            }

            //计算分页
            int numPerPage, currentPage, startRowIndex;
            long totalCount = 0;

            numPerPage = 10;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;

            IList<Product> list = Product.FindAll(ex, null, null, startRowIndex, numPerPage);
            totalCount = Product.FindCount(ex, null, null, startRowIndex, numPerPage);

            ViewBag.totalCount = totalCount;
            ViewBag.page = page;
            ViewBag.pagesize = numPerPage;
            ViewBag.cfg = cfg;
            ViewBag.list = list;

            return View(entity);
        }
        #endregion

        #region 商品详情
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Product entity = Product.FindById(id);
            if (entity == null || entity.IsHide == 1)
            {
                return EchoTip("系统找不到本记录！");
            }
            //文章更多图片列表
            if (!string.IsNullOrEmpty(entity.ItemImg))
            {
                string[] arrimg = entity.ItemImg.Split(new string[] { "|||" }, StringSplitOptions.None);
                List<string> listimg = new List<string>();
                foreach (string s in arrimg)
                {
                    if (!string.IsNullOrEmpty(s) && Utils.IsImgFilename(s))
                    {
                        listimg.Add(s.Trim());
                    }
                }
                ViewBag.ListImg = listimg;
            }
            entity.Hits++;
            entity.Update();
            ViewBag.cfg = cfg;
            ViewBag.category = entity.Category;

            return View(entity);
        }
        #endregion
    }
}