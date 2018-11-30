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
    [DisplayName("文章系统")]
    [Description("文章系统管理，包括文章、文章栏目")]
    public class ArticleController : AdminBaseController
    {
        #region 文章栏目
        /// <summary>
        /// 文章栏目列表
        /// </summary>
        /// <returns></returns>
        [MyAuthorize( "viewlist",  "articlecategory")]
        [DisplayName("文章栏目")]
        public IActionResult ArticleCategoryList()
        {
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, false, true);
            Core.Admin.WriteLogActions("查看文章栏目列表页面；");
            return View(list);
        }

        [MyAuthorize( "add",  "articlecategory")]
        public IActionResult AddArticleCategory()
        {
            //获取上级栏目
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;
            //获取模板 模板规则，以Index_开头的，为栏目列表，以Detial_开头的为文章详情
            List<string> listTpls = new List<string>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asmItem in asms)
            {
                var types = asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Article")).ToList();
                if (types.Count == 0) continue;
                foreach (var type in types)
                {
                    string viewName = type.Name.Replace("Views_Article_", "") + ".cshtml";
                    listTpls.Add(viewName);
                }
            }
            ViewBag.ListTpl = listTpls;
            Core.Admin.WriteLogActions("查看添加文章栏目页面；");
            return View();
        }

        [HttpPost]
        [MyAuthorize( "add",  "articlecategory", "JSON")]
        public IActionResult AddArticleCategory(ArticleCategory model)
        {
            //获取上级栏目
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "文章栏目标题不能为空！";
                return Json(tip);
            }
            model.Insert();
            Core.Admin.WriteLogActions("添加文章栏目(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加文章栏目成功";
            tip.ReturnUrl = "close";

            return Json(tip);
        }

        //编辑栏目
        [MyAuthorize( "edit",  "articlecategory")]
        public IActionResult EditArticleCategory(int id)
        {
            ArticleCategory entity = ArticleCategory.Find(ArticleCategory._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            //获取上级栏目
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListTree = list;

            //获取模板 模板规则，以Index_开头的，为栏目列表，以Detial_开头的为文章详情
            List<string> listTpls = new List<string>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asmItem in asms)
            {
                var types = asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Article")).ToList();
                if (types.Count == 0) continue;
                foreach (var type in types)
                {
                    string viewName = type.Name.Replace("Views_Article_", "") + ".cshtml";
                    listTpls.Add(viewName);
                }
            }
            ViewBag.ListTpl = listTpls;
            Core.Admin.WriteLogActions("查看/编辑文章栏目（id:" + id + "）页面；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize( "edit",  "articlecategory", "JSON")]
        public IActionResult EditArticleCategory(ArticleCategory model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "文章栏目标题不能为空！";
                return Json(tip);
            }

            ArticleCategory entity = ArticleCategory.Find(ArticleCategory._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                Json(tip);
            }

            if (entity.PId != model.PId)
            {
                if (entity.Id == model.PId)
                {
                    tip.Message = "上级栏目不能选择本身！";
                    return Json(tip);
                }
                entity.PId = model.PId;
                entity.Location = ArticleCategory.GetNewLocation(model.PId);
                entity.Level = ArticleCategory.GetNewLevel(model.PId);
                //修改文章的location
                IList<Article> alist = Article.FindAll(Article._.KId == model.Id, null, null, 0, 0);
                if (alist != null && alist.Count > 0)
                {
                    foreach (var a in alist)
                    {
                        a.Location = entity.Location + "," + entity.Id;
                        //a.FilePath = entity.FilePath;
                    }
                    alist.Save();
                }
            }
            //赋值
            entity.PId = model.PId;
            entity.KindName = model.KindName;
            entity.SubTitle = model.SubTitle;
            entity.KindTitle = model.KindTitle;
            entity.Keyword = model.Keyword;
            entity.Description = model.Description;
            entity.LinkURL = model.LinkURL;
            entity.TitleColor = model.TitleColor;
            entity.TemplateFile = model.TemplateFile;
            entity.DetailTemplateFile = model.DetailTemplateFile;
            entity.IsList = model.IsList;
            entity.PageSize = model.PageSize <= 0 ? 15 : model.PageSize;
            entity.IsLock = model.IsLock;
            entity.IsHide = model.IsHide;
            entity.IsShowSubDetail = model.IsShowSubDetail;
            entity.BannerImg = model.BannerImg;
            entity.Pic = model.Pic;
            entity.KindInfo = model.KindInfo;
            entity.KindDomain = model.KindDomain;
            entity.Rank = model.Rank;
            entity.Pic = model.Pic;


            entity.Save();
            Core.Admin.WriteLogActions("修改文章栏目(id:" + entity.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "修改文章栏目成功";
            tip.ReturnUrl = "close";

            return Json(tip);
        }

        //删除栏目
        [HttpPost]
        [MyAuthorize( "del",  "articlecategory", "JSON")]
        public JsonResult DelArticleCategory(int id)
        {
            ArticleCategory entity = ArticleCategory.Find(ArticleCategory._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本文章栏目！";
                return Json(tip);
            }
            if (ArticleCategory.FindCount(ArticleCategory._.PId == entity.Id, null, null, 0, 0) > 0)
            {
                tip.Message = "本栏目有下级栏目，不允许删除！";
                return Json(tip);
            }
            //删除文章
            IList<Article> list = Article.FindAll(Article._.KId == entity.Id, null, null, 0, 0);
            if (list != null && list.Count > 0)
            {
                list.Delete();
            }
            Core.Admin.WriteLogActions("删除文章栏目(id:" + entity.Id + ");");
            entity.Delete();
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除文章栏目成功";
            return Json(tip);
        }
        #endregion

        #region 文章
        /// <summary>
        /// 文章栏目管理
        /// </summary>
        /// <returns></returns>
        [MyAuthorize("viewlist", "article")]
        [DisplayName("文章")]
        public IActionResult ArticleList()
        {
            //获取上级栏目
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;
            Core.Admin.WriteLogActions("查看文章列表;");
            return View();
        }

        [MyAuthorize("viewlist", "article", "JSON")]
        public IActionResult GetArticleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage, currentPage, startRowIndex;

            numPerPage = limit;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = Article._.Id > 0;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (Utils.IsInt(keyword))
                {
                    ex &= (Article._.Id == int.Parse(keyword) | Article._.Title.Contains(keyword));
                }
                else
                {
                    ex &= Article._.Title.Contains(keyword);
                }
            }
            string kid = Request.Query["kid"];
            string isshowsub = Request.Query["isshowsub"];
            if (Utils.IsInt(kid) && int.Parse(kid) > 0)
            {
                //获取栏目
                if (isshowsub == "1")
                {
                    List<int> kids = new List<int>();
                    kids.Add(int.Parse(kid));
                    IList<ArticleCategory> subkinds = ArticleCategory.FindByParentID(int.Parse(kid));
                    if (subkinds != null && subkinds.Count > 0)
                    {
                        foreach (var item in subkinds)
                        {
                            kids.Add(item.Id);
                        }
                        ex &= Article._.KId.In(kids);
                    }
                }
                else
                {
                    ex &= Article._.KId == int.Parse(kid);
                }
            }

            IList<Article> list = Article.FindAll(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            //return Json(new { total = totalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        //添加文章
        [HttpGet]
        [MyAuthorize("add", "article")]
        public IActionResult AddArticle()
        {
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;
            string lastkid = SessionHelper.GetSession("com_add_article_kid").ToString();
            ViewBag.lastkid = lastkid;
            Core.Admin.WriteLogActions("查看添加文章页面;");
            return View();
        }

        //执行添加文章
        [HttpPost]
        [MyAuthorize("add", "article", "JSON")]
        public IActionResult AddArticle(Article model)
        {
            if (model.KId <= 0)
            {
                tip.Message = "请选择一个文章栏目";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "请填写文章标题";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                tip.Message = "请填写文章内容";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Pic) && !Utils.IsImgFilename(model.Pic))
            {
                tip.Message = "文章图片填写的不是图片格式！";
                return Json(tip);
            }
            //处理文章更多图片
            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;//更多图片的时候用到
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                    {
                        morIMG += s + "|||";//使用“|||”分隔图片
                    }
                }
            }
            string content = model.Content;
            if(Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = ThumbnailHelper.SaveRemoteImgForContent(content);
                model.Content = content;
            }
            model.ItemImg = morIMG;
            model.AuthorId = Core.Admin.GetMyInfo().Id;
            model.Insert();
            ArticleCategory.UpdateDetailCount(model.KId);
            SessionHelper.WriteSession("com_add_article_kid", model.KId);
            //添加TAG
            //Tag.InsertTags(model.Tags, RTType.RatuoModule.Article, model.Id, model.Title);
            Core.Admin.WriteLogActions("添加文章(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加文章成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        //编辑文章
        [HttpGet]
        [MyAuthorize("edit", "article")]
        public IActionResult EditArticle(int id)
        {
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;

            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }

            Core.Admin.WriteLogActions("查看文章(" + id + ");");

            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "article", "JSON")]
        public IActionResult EditArticle(Article model)
        {

            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                Json(tip);
            }
            Article entity = Article.Find(Article._.Id == model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                tip.Message = "文章标题不能为空！";
                return Json(tip);
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                tip.Message = "请填写文章内容";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Pic) && !Utils.IsImgFilename(model.Pic))
            {
                tip.Message = "文章图片填写的不是图片格式！";
                return Json(tip);
            }
            //处理文章更多图片
            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;//更多图片的时候用到
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                    {
                        morIMG += s + "|||";//使用“|||”分隔图片
                    }
                }
            }

            //处理三个勾选属性的
            string isnew = Request.Form["IsNew"];
            model.IsNew = isnew == "1" ? 1 : 0;

            string IsRecommend = Request.Form["IsRecommend"];
            model.IsRecommend = IsRecommend == "1" ? 1 : 0;

            string IsHide = Request.Form["IsHide"];
            model.IsHide = IsHide == "1" ? 1 : 0;
            model.Hits = entity.Hits;
            model.UpdateTime = DateTime.Now;

            model.ItemImg = morIMG;
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = ThumbnailHelper.SaveRemoteImgForContent(content);
                model.Content = content;
            }
            //model.AuthorId = Core.Admin.GetMyInfo().Id;
            model.Save();
            //Tag.ModifyTags(model.Tags, RTType.RatuoModule.Article, model.Id, model.Title);
            Core.Admin.WriteLogActions("编辑文章(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑文章详情成功";
            tip.ReturnUrl = "close";

            return Json(tip);
        }
        //删除文章
        [HttpPost]
        [MyAuthorize("del", "article", "JSON")]
        public JsonResult DelArtice(int id)
        {
            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本文章！";
                return Json(tip);
            }
            int kid = entity.Id;
            //删除TAG
            //Tag.DeleteTag(RTType.RatuoModule.Article, entity.Id);
            Core.Admin.WriteLogActions("删除文章(id:" + entity.Id + ");");
            entity.Delete();
            ArticleCategory.UpdateDetailCount(kid);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除文章成功";
            return Json(tip);
        }
        #endregion
    }
}