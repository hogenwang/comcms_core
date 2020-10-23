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
    public class ArticleController : HomeBaseController
    {
        #region 显示列表
        // GET: Article
        [HttpGet]
        public IActionResult Index(int id = 0, int page = 1)
        {
            ArticleCategory model = ArticleCategory.FindById(id);
            
            if (model == null)
            {
                return AlertAndGoBack("系统找不到本记录");
            }
            //如果跳转到地址
            if (!string.IsNullOrEmpty(model.LinkURL))
            {
                if (Utils.IsInt(model.LinkURL))//如果是数字，则跳转到详情
                {
                    //return Redirect($"/Article/Detail/{model.LinkURL}");
                    string linkUrl = ViewsHelper.EchoArticleURL(int.Parse(model.LinkURL));
                    return Redirect(linkUrl);
                }
                else
                {
                    return Redirect(model.LinkURL);
                }

            }
            if (page < 1) page = 1;

            if (model.IsList == 1)
            {
                IList<Article> list = new List<Article>();
                int numPerPage, currentPage, startRowIndex;
                numPerPage = model.PageSize;
                if (page > 0)
                    currentPage = page;
                else
                    currentPage = 1;

                startRowIndex = (currentPage - 1) * numPerPage;

                var ex = Article._.IsHide != 1 & Article._.IsDel != 1;

                //如果显示下级栏目文章
                if (model.IsShowSubDetail == 1)
                {
                    //获取下级所有栏目
                    List<int> allsubkids = new List<int>();
                    allsubkids.Add(model.Id);

                    IList<ArticleCategory> allkind = ArticleCategory.FindAllSubKinds(model.Id);
                    if (allkind != null && allkind.Count > 0)
                    {
                        foreach (var s in allkind)
                        {
                            allsubkids.Add(s.Id);
                        }
                    }
                    ex &= Article._.KId.In(allsubkids);
                }
                else
                    ex &= Article._.KId == model.Id;
                long totalCount = Article.FindCount(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
                int pageCount = (int)totalCount / numPerPage;//总页数
                if (totalCount % numPerPage > 0)
                {
                    pageCount += 1;
                }
                list = Article.FindAll(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
                ViewBag.list = list;//列表
                //分页信息
                ViewBag.totalCount = totalCount;
                ViewBag.pageCount = pageCount;
                ViewBag.page = page;
                ViewBag.pagesize = numPerPage;
            }

            ViewBag.cfg = cfg;
            string templatesname = "Index.cshtml";//模板名称
            if (!string.IsNullOrEmpty(model.TemplateFile))
            {
                templatesname = model.TemplateFile;
                //return Content(templatesname);
            }
            return View("~/Views/Article/" + templatesname, model); //固定死就是这个
        }
        #endregion

        #region 显示详情
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                return AlertAndGoBack("系统找不到本记录！");
            }
            ArticleCategory kind = ArticleCategory.FindById(entity.KId);
            if (kind == null)
            {
                return AlertAndGoBack("系统找不到本文章栏目！");
            }
            //增加点击
            entity.Hits++;
            entity.Update();
            if (!string.IsNullOrEmpty(entity.LinkURL))
            {
                //return Content(entity.LinkURL);
                return Redirect(entity.LinkURL);
            }
            ViewBag.kind = kind;
            ViewBag.cfg = cfg;
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
            string templatesname = "";//模板名称
            if (!string.IsNullOrEmpty(kind.TemplateFile))
            {
                templatesname = kind.DetailTemplateFile;//.Replace(".cshtml", "").Replace(".aspx", "");
                return View("~/Views/Article/" + templatesname, entity);
            }
            else
            {
                return View(entity);
            }
        }
        #endregion
    }
}