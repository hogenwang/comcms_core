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
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.ListTree = list;
            //获取模板 模板规则，以Index_开头的，为栏目列表，以Detial_开头的为文章详情
            List<string> listTpls = new List<string>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asmItem in asms)
            {
                string fullName = asmItem.GetName().ToString();
                if (fullName.IndexOf("COMCMS.Web.Views") > -1)
                {
                    var types = asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Article")).ToList();
                    if (types.Count == 0) continue;
                    foreach (var type in types)
                    {
                        string viewName = type.Name.Replace("Views_Article_", "") + ".cshtml";
                        listTpls.Add(viewName);
                    }
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

            if (!string.IsNullOrEmpty(model.FilePath))
            {
                if (!model.FilePath.StartsWith("/"))
                {
                    tip.Message = "栏目路径请以/开头！";
                    return Json(tip);
                }
                if (model.FilePath.EndsWith("/"))
                {
                    tip.Message = "栏目路径结尾不用加上/";
                    return Json(tip);
                }

                if (model.FilePath.Count(x => x == '/') > 4)
                {
                    tip.Message = "最多只能四级路径！";
                    return Json(tip);
                }
            }

            if (!string.IsNullOrEmpty(model.FilePath) && !AdminUtils.CheckFilePathIsOK(model.FilePath, 0, 0))
            {
                tip.Message = "栏目路径不可用，请重新填写！";
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
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.ListTree = list;

            //获取模板 模板规则，以Index_开头的，为栏目列表，以Detial_开头的为文章详情
            List<string> listTpls = new List<string>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asmItem in asms)
            {
                string fullName = asmItem.GetName().ToString();
                if (fullName.IndexOf("COMCMS.Web.Views") > -1)
                {
                    var types = asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Article")).ToList();
                    if (types.Count == 0) continue;
                    foreach (var type in types)
                    {
                        string viewName = type.Name.Replace("Views_Article_", "") + ".cshtml";
                        listTpls.Add(viewName);
                    }
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

            if (!string.IsNullOrEmpty(model.FilePath))
            {
                if (!model.FilePath.StartsWith("/"))
                {
                    tip.Message = "栏目路径请以/开头！";
                    return Json(tip);
                }
                if (model.FilePath.EndsWith("/"))
                {
                    tip.Message = "栏目路径结尾不用加上/";
                    return Json(tip);
                }

                if (model.FilePath.Count(x => x == '/') > 4)
                {
                    tip.Message = "最多只能四级路径！";
                    return Json(tip);
                }
            }



            if (!string.IsNullOrEmpty(model.FilePath) && !AdminUtils.CheckFilePathIsOK(model.FilePath, entity.Id, 0))
            {
                tip.Message = "栏目路径不可用，请重新填写！";
                return Json(tip);
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
            bool idNeedUpdateAllArticleFilePath = entity.FilePath == model.FilePath;
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
            entity.FilePath = model.FilePath;


            entity.Save();
            //修改所有文章的路径
            if (idNeedUpdateAllArticleFilePath)
            {
                IList<Article> listArticles = Article.FindAll(Article._.KId == entity.Id, null, null, 0, 0);
                if(listArticles !=null && listArticles.Count > 0)
                {
                    foreach (var item in listArticles)
                    {
                        item.FilePath = entity.FilePath;
                    }
                    listArticles.Update();
                }
            }
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

        #region 修改文章栏目排序
        [HttpPost]
        [MyAuthorize("edit", "articlecategory", "JSON")]
        public IActionResult DoEditCategoryRank(int id, int rank)
        {
            ArticleCategory entity = ArticleCategory.Find(ArticleCategory._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            entity.Rank = rank;
            entity.Update();
            Admin.WriteLogActions($"修改文章栏目排序(id:{id},排序:{rank});");
            tip.Message = "修改排序成功！";
            tip.Status = JsonTip.SUCCESS;
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
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, true);
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
            Expression ex = new Expression(); ;

            Admin my = Admin.GetMyInfo();
            List<int> aclist = new List<int>();
            if (my.Roles.IsSuperAdmin != 1)
            {
                aclist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(string.IsNullOrEmpty(my.Roles.AuthorizedArticleCagegory)?"[]": my.Roles.AuthorizedArticleCagegory);
                if (aclist == null) aclist = new List<int>();
                if (aclist.Count > 0)
                {
                    ex &= Article._.KId.In(aclist);
                }
                
                //仅显示有权限内容
                if (my.Roles.OnlyEditMyselfArticle == 1)
                    ex &= Article._.AuthorId == my.Id;
            }

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
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { total = totalCount, rows = list }), "text/plain");
            return Json(new { total = totalCount, rows = list });
        }

        //添加文章
        [HttpGet]
        [MyAuthorize("add", "article")]
        public IActionResult AddArticle()
        {
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, true);
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
            if (!string.IsNullOrEmpty(model.FileName) && !Utils.ChekHTMLFileNameIsOK(model.FileName))
            {
                tip.Message = "静态化文件名错误，请填写正确的，或者留空！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            List<int> aclist = new List<int>();
            if (my.Roles.IsSuperAdmin != 1)
            {
                aclist = JsonConvert.DeserializeObject<List<int>>(string.IsNullOrEmpty(my.Roles.AuthorizedArticleCagegory) ? "[]" : my.Roles.AuthorizedArticleCagegory);
                if (aclist == null) aclist = new List<int>();
                if (aclist.Count > 0)
                {
                    if (aclist.FindIndex(x => x == model.KId) == -1)
                    {
                        tip.Message = "当前选择栏目不存在，或者您没这个栏目的权限！";
                        return Json(tip);
                    }
                }
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
            model.AuthorId = my.Id;
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
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, true);
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
            //处理
            if (!string.IsNullOrEmpty(model.FileName) && !Utils.ChekHTMLFileNameIsOK(model.FileName))
            {
                tip.Message = "静态化文件名错误，请填写正确的，或者留空！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            List<int> aclist = new List<int>();
            if (my.Roles.IsSuperAdmin != 1)
            {
                aclist = JsonConvert.DeserializeObject<List<int>>(string.IsNullOrEmpty(my.Roles.AuthorizedArticleCagegory) ? "[]" : my.Roles.AuthorizedArticleCagegory); 
                if (aclist == null) aclist = new List<int>();
                if (aclist.Count > 0)
                {
                    if (aclist.FindIndex(x => x == model.KId) == -1)
                    {
                        tip.Message = "当前选择栏目不存在，或者您没这个栏目的权限！";
                        return Json(tip);
                    }
                }

                if(my.Roles.OnlyEditMyselfArticle ==1 && entity.AuthorId != my.Id)
                {
                    tip.Message = "系统限制，您无法编辑非自己添加的文章！";
                    return Json(tip);
                }
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
            string isnew = Request.Form["IsNew"];
            model.IsNew = isnew == "1" ? 1 : 0;

            string IsRecommend = Request.Form["IsRecommend"];
            model.IsRecommend = IsRecommend == "1" ? 1 : 0;

            string IsHide = Request.Form["IsHide"];
            model.IsHide = IsHide == "1" ? 1 : 0;
            //处理三个勾选属性的

            model.Hits = entity.Hits;
            model.UpdateTime = DateTime.Now;

            model.ItemImg = morIMG;
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = ThumbnailHelper.SaveRemoteImgForContent(content);
                model.Content = content;
            }


            entity.AddTime = model.AddTime;
            entity.AdsId = model.AdsId;
            entity.BannerImg = model.BannerImg;
            entity.Content = model.Content;
            entity.Description = model.Description;
            entity.FilePath = model.FilePath;
            entity.Hits = model.Hits;
            entity.Icon = model.Icon;
            entity.IsBest = model.IsBest;
            entity.IsComment = model.IsComment;
            entity.IsDel = model.IsDel;
            entity.IsHide = model.IsHide;
            entity.IsLock = model.IsLock;
            entity.IsMember = model.IsMember;
            entity.IsNew = model.IsNew;
            entity.IsRecommend = model.IsRecommend;
            entity.IsTop = model.IsTop;
            entity.ItemImg = morIMG;
            entity.Keyword = model.Keyword;
            entity.KId = model.KId;
            entity.LinkURL = model.LinkURL;
            entity.Location = model.Location;
            entity.Origin = model.Origin;
            entity.OriginURL = model.OriginURL;
            entity.Pic = model.Pic;
            entity.Sequence = model.Sequence;
            entity.SubTitle = model.SubTitle;
            entity.Tags = model.Tags;
            entity.TemplateFile = model.TemplateFile;
            entity.Title = model.Title;
            entity.TitleColor = model.TitleColor;
            entity.UpdateTime = DateTime.Now;
            entity.FileName = model.FileName;

            //model.AuthorId = Core.Admin.GetMyInfo().Id;
            entity.Update();
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
        public JsonResult DelArticle(int id)
        {
            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本文章！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            List<int> aclist = new List<int>();
            if (my.Roles.IsSuperAdmin != 1)
            {
                aclist = JsonConvert.DeserializeObject<List<int>>(string.IsNullOrEmpty(my.Roles.AuthorizedArticleCagegory) ? "[]" : my.Roles.AuthorizedArticleCagegory);
                if (aclist == null) aclist = new List<int>();
                if (aclist.Count > 0)
                {
                    if (aclist.FindIndex(x => x == entity.KId) == -1)
                    {
                        tip.Message = "者您没这个栏目的权限，无法删除该栏目文章！";
                        return Json(tip);
                    }
                }

                if (my.Roles.OnlyEditMyselfArticle == 1 && entity.AuthorId != my.Id)
                {
                    tip.Message = "系统限制，您无法删除非自己添加的文章！";
                    return Json(tip);
                }
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

        #region 迁移文章
        [HttpGet]
        [MyAuthorize("batch", "article")]
        public IActionResult MoveArticle()
        {
            //获取上级栏目
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, true, false);
            ViewBag.ListKinds = list;
            return View();
        }
        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult MoveArticle(int from, int to)
        {
            if (from <= 0 || to <= 0)
            {
                tip.Message = "请选择好栏目";
                return Json(tip);
            }
            if (from == to)
            {
                tip.Message = "迁移前后栏目一致，无法迁移";
                return Json(tip);
            }
            Article.Update("KId=" + to, "KId=" + from);
            Core.Admin.WriteLogActions("迁移文章(from:" + from + ",to:" + to + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "迁移文章成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 批量隐藏文章
        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchHideArticle(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                tip.Message = "请最少选择一篇文章！";
                return Json(tip);
            }
            List<int> listIds = new List<int>();
            try
            {
                listIds = JsonConvert.DeserializeObject<List<int>>(ids);
            }
            catch (Exception ex)
            {
                tip.Message = "转换出错：" + ex.Message;
                return Json(tip);
            }

            if (listIds == null || listIds.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            IList<Article> list = Article.FindAll(Article._.Id.In(listIds), null, null, 0, 0);
            if (list == null || list.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            foreach (var item in list)
            {
                item.IsHide = 1;
            }
            
            list.Update();
            Admin.WriteLogActions($"批量隐藏文章：{ids};");
            tip.Message = "批量隐藏文章成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 批量显示文章
        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchShowArticle(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                tip.Message = "请最少选择一篇文章！";
                return Json(tip);
            }
            List<int> listIds = new List<int>();
            try
            {
                listIds = JsonConvert.DeserializeObject<List<int>>(ids);
            }
            catch (Exception ex)
            {
                tip.Message = "转换出错：" + ex.Message;
                return Json(tip);
            }

            if (listIds == null || listIds.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            IList<Article> list = Article.FindAll(Article._.Id.In(listIds), null, null, 0, 0);
            if (list == null || list.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            foreach (var item in list)
            {
                item.IsHide = 0;
            }
            list.Update();
            Admin.WriteLogActions($"批量显示文章：{ids};");
            tip.Message = "批量显示文章成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 批量删除文章
        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchDelArticle(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                tip.Message = "请最少选择一篇文章！";
                return Json(tip);
            }
            List<int> listIds = new List<int>();
            try
            {
                listIds = JsonConvert.DeserializeObject<List<int>>(ids);
            }
            catch (Exception ex)
            {
                tip.Message = "转换出错：" + ex.Message;
                return Json(tip);
            }

            if (listIds == null || listIds.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            IList<Article> list = Article.FindAll(Article._.Id.In(listIds), null, null, 0, 0);
            if (list == null || list.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            foreach (var item in list)
            {
                item.IsHide = 0;
            }
            list.Delete();
            Admin.WriteLogActions($"批量删除文章：{ids};");
            tip.Message = "批量删除文章成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion
    }
}