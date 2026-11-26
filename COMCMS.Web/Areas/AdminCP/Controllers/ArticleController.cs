using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Common;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XCode;

namespace COMCMS.Web.Areas.AdminCP.Controllers
{
    [DisplayName("文章系统")]
    [Description("文章系统管理，包括文章、文章栏目")]
    public class ArticleController : AdminBaseController
    {
        private readonly IContentSanitizer _contentSanitizer;
        private readonly RemoteImageService _remoteImages;

        public ArticleController(IContentSanitizer contentSanitizer, RemoteImageService remoteImages)
        {
            _contentSanitizer = contentSanitizer;
            _remoteImages = remoteImages;
        }

        #region 文章栏目
        [MyAuthorize("viewlist", "articlecategory")]
        [DisplayName("文章栏目")]
        public IActionResult ArticleCategoryList()
        {
            IList<ArticleCategory> list = ArticleCategory.GetListTree(0, -1, false, true);
            Core.Admin.WriteLogActions("查看文章栏目列表页面；");
            return View(list);
        }

        [MyAuthorize("add", "articlecategory")]
        public IActionResult AddArticleCategory()
        {
            ViewBag.ListTree = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.ListTpl = GetArticleTemplateNames();
            Core.Admin.WriteLogActions("查看添加文章栏目页面；");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "articlecategory", "JSON")]
        public IActionResult AddArticleCategory(ArticleCategory model)
        {
            if (string.IsNullOrWhiteSpace(model.KindName))
            {
                tip.Message = "文章栏目标题不能为空！";
                return Json(tip);
            }

            if (!ValidateCategoryFilePath(model.FilePath, 0, out var pathError))
            {
                tip.Message = pathError;
                return Json(tip);
            }

            if (!ValidateAllowIpList(model.AllowIp, out var ipError))
            {
                tip.Message = ipError;
                return Json(tip);
            }

            model.PageSize = model.PageSize <= 0 ? 15 : model.PageSize;
            model.KindInfo = _contentSanitizer.Sanitize(model.KindInfo);
            model.Insert();
            Core.Admin.WriteLogActions("添加文章栏目(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加文章栏目成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [MyAuthorize("edit", "articlecategory")]
        public IActionResult EditArticleCategory(int id)
        {
            ArticleCategory entity = ArticleCategory.Find(ArticleCategory._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            ViewBag.ListTree = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.ListTpl = GetArticleTemplateNames();
            Core.Admin.WriteLogActions("查看/编辑文章栏目（id:" + id + "）页面；");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "articlecategory", "JSON")]
        public IActionResult EditArticleCategory(ArticleCategory model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
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
                return Json(tip);
            }

            if (!ValidateCategoryFilePath(model.FilePath, entity.Id, out var pathError))
            {
                tip.Message = pathError;
                return Json(tip);
            }

            if (!ValidateAllowIpList(model.AllowIp, out var ipError))
            {
                tip.Message = ipError;
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
                IList<Article> alist = Article.FindAll(Article._.KId == model.Id, Article._.Id.Desc(), null, 0, 0);
                if (alist != null && alist.Count > 0)
                {
                    foreach (var a in alist)
                    {
                        a.Location = entity.Location + "," + entity.Id;
                    }
                    alist.Save();
                }
            }

            bool isNeedUpdateAllArticleFilePath = !string.Equals(entity.FilePath, model.FilePath, StringComparison.Ordinal);
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
            entity.KindInfo = _contentSanitizer.Sanitize(model.KindInfo);
            entity.KindDomain = model.KindDomain;
            entity.Rank = model.Rank;
            entity.FilePath = model.FilePath;
            entity.AllowIp = string.IsNullOrWhiteSpace(model.AllowIp) ? "" : model.AllowIp;

            entity.Save();
            if (isNeedUpdateAllArticleFilePath)
            {
                IList<Article> listArticles = Article.FindAll(Article._.KId == entity.Id, Article._.Id.Desc(), null, 0, 0);
                if (listArticles != null && listArticles.Count > 0)
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

        [HttpPost]
        [MyAuthorize("del", "articlecategory", "JSON")]
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
        [MyAuthorize("viewlist", "article")]
        [DisplayName("文章")]
        public IActionResult ArticleList()
        {
            ViewBag.ListKinds = ArticleCategory.GetListTree(0, -1, true, true);
            Core.Admin.WriteLogActions("查看文章列表;");
            return View();
        }

        [MyAuthorize("viewlist", "article", "JSON")]
        public IActionResult GetArticleList(string keyword, int page = 1, int limit = 20)
        {
            int numPerPage = Math.Clamp(limit, 1, 100);
            int currentPage = Math.Max(page, 1);
            int startRowIndex = (currentPage - 1) * numPerPage;
            Expression ex = new Expression();

            Admin my = Admin.GetMyInfo();
            if (my?.Roles != null && my.Roles.IsSuperAdmin != 1)
            {
                var aclist = ParseAuthorizedIds(my.Roles.AuthorizedArticleCagegory);
                if (aclist.Count > 0)
                {
                    ex &= Article._.KId.In(aclist);
                }
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
                int kidVal = int.Parse(kid);
                if (isshowsub == "1")
                {
                    List<int> kids = new List<int> { kidVal };
                    IList<ArticleCategory> subkinds = ArticleCategory.FindByParentID(kidVal);
                    if (subkinds != null && subkinds.Count > 0)
                    {
                        kids.AddRange(subkinds.Select(s => s.Id));
                    }
                    ex &= Article._.KId.In(kids);
                }
                else
                {
                    ex &= Article._.KId == kidVal;
                }
            }

            IList<Article> list = Article.FindAll(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);
            return Json(new { total = totalCount, rows = list });
        }

        [HttpGet]
        [MyAuthorize("add", "article")]
        public IActionResult AddArticle()
        {
            ViewBag.ListKinds = ArticleCategory.GetListTree(0, -1, true, true);
            ViewBag.lastkid = SessionHelper.GetSession("com_add_article_kid").ToString();
            Core.Admin.WriteLogActions("查看添加文章页面;");
            return View();
        }

        [HttpPost]
        [MyAuthorize("add", "article", "JSON")]
        public async Task<IActionResult> AddArticle(Article model)
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
            if (!CanAccessArticle(my, model.KId, authorId: null, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                        morIMG += s + "|||";
                }
            }
            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = await _remoteImages.ImportAsync(content, HttpContext.RequestAborted);
                model.Content = content;
            }
            model.Content = _contentSanitizer.Sanitize(model.Content);
            model.ItemImg = morIMG;
            model.AuthorId = my.Id;
            model.Insert();
            ArticleCategory.UpdateDetailCount(model.KId);
            SessionHelper.WriteSession("com_add_article_kid", model.KId);
            Tags.InsertTags(model.Tags, Utils.CMSType.Article, model.Id, model.Title);
            Core.Admin.WriteLogActions("添加文章(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加文章成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        [HttpGet]
        [MyAuthorize("edit", "article")]
        public IActionResult EditArticle(int id)
        {
            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            Admin my = Admin.GetMyInfo();
            if (!CanAccessArticle(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                return EchoTipPage(deniedReason);
            }

            ViewBag.ListKinds = ArticleCategory.GetListTree(0, -1, true, true);
            Core.Admin.WriteLogActions("查看文章(" + id + ");");
            return View(entity);
        }

        [HttpPost]
        [MyAuthorize("edit", "article", "JSON")]
        public async Task<IActionResult> EditArticle(Article model)
        {
            if (model.Id <= 0)
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
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
            if (!string.IsNullOrEmpty(model.FileName) && !Utils.ChekHTMLFileNameIsOK(model.FileName))
            {
                tip.Message = "静态化文件名错误，请填写正确的，或者留空！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            // 同时校验原文章栏目+作者归属 以及 目标栏目权限，防止跨栏目劫持
            if (!CanAccessArticle(my, entity.KId, entity.AuthorId, out var deniedReason) ||
                !CanAccessArticle(my, model.KId, authorId: null, out deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            string[] moreImgSrc = Request.Form["nImgUrl"];
            string morIMG = string.Empty;
            if (moreImgSrc != null && moreImgSrc.Length > 0)
            {
                foreach (string s in moreImgSrc)
                {
                    if (Utils.IsImgFilename(s))
                        morIMG += s + "|||";
                }
            }

            model.IsNew = Request.Form["IsNew"] == "1" ? 1 : 0;
            model.IsRecommend = Request.Form["IsRecommend"] == "1" ? 1 : 0;
            model.IsHide = Request.Form["IsHide"] == "1" ? 1 : 0;
            model.Hits = entity.Hits;
            model.UpdateTime = DateTime.Now;
            model.ItemImg = morIMG;

            string content = model.Content;
            if (Request.Form["autoSaveRemoteImg"] == "1" && !string.IsNullOrEmpty(content))
            {
                content = await _remoteImages.ImportAsync(content, HttpContext.RequestAborted);
                model.Content = content;
            }
            model.Content = _contentSanitizer.Sanitize(model.Content);

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

            entity.Update();
            Tags.ModifyTags(model.Tags, Utils.CMSType.Article, model.Id, model.Title);
            Core.Admin.WriteLogActions("编辑文章(id:" + model.Id + ");");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑文章详情成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

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
            if (!CanAccessArticle(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            int kid = entity.KId;
            Tags.DeleteTag(entity.Tags, Utils.CMSType.Article, entity.Id);
            Core.Admin.WriteLogActions("删除文章(id:" + entity.Id + ");");
            entity.Delete();
            ArticleCategory.UpdateDetailCount(kid);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除文章成功";
            return Json(tip);
        }
        #endregion

        #region 修改文章排序
        [HttpPost]
        [MyAuthorize("edit", "article", "JSON")]
        public IActionResult DoEditArticleSequence(int id, int rank)
        {
            Article entity = Article.Find(Article._.Id == id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            Admin my = Admin.GetMyInfo();
            if (!CanAccessArticle(my, entity.KId, entity.AuthorId, out var deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }
            entity.Sequence = rank;
            entity.Update();
            Admin.WriteLogActions($"修改文章排序(id:{id},排序:{rank});");
            tip.Message = "修改排序成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 迁移文章
        [HttpGet]
        [MyAuthorize("batch", "article")]
        public IActionResult MoveArticle()
        {
            ViewBag.ListKinds = ArticleCategory.GetListTree(0, -1, true, false);
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

            Admin my = Admin.GetMyInfo();
            if (my?.Roles != null && my.Roles.IsSuperAdmin != 1 && my.Roles.OnlyEditMyselfArticle == 1)
            {
                tip.Message = "当前管理组仅允许操作本人文章，禁止整栏目迁移！";
                return Json(tip);
            }
            if (!CanAccessArticle(my, from, null, out var deniedReason) || !CanAccessArticle(my, to, null, out deniedReason))
            {
                tip.Message = deniedReason;
                return Json(tip);
            }

            Article.Update(new[] { Article.__.KId }, new object[] { to }, new[] { Article.__.KId }, new object[] { from });
            ArticleCategory.UpdateDetailCount(from);
            ArticleCategory.UpdateDetailCount(to);
            Core.Admin.WriteLogActions($"迁移文章(from:{from},to:{to});");
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "迁移文章成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        #endregion

        #region 批量隐藏/显示/删除文章
        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchHideArticle(string ids) => ExecuteBatchArticleAction(ids, "隐藏", list =>
        {
            foreach (var item in list) item.IsHide = 1;
            list.Update();
        });

        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchShowArticle(string ids) => ExecuteBatchArticleAction(ids, "显示", list =>
        {
            foreach (var item in list) item.IsHide = 0;
            list.Update();
        });

        [HttpPost]
        [MyAuthorize("batch", "article", "JSON")]
        public IActionResult DoBatchDelArticle(string ids) => ExecuteBatchArticleAction(ids, "删除", list =>
        {
            var affectedKinds = list.Select(a => a.KId).Distinct().ToList();
            foreach (var item in list)
            {
                Tags.DeleteTag(item.Tags, Utils.CMSType.Article, item.Id);
            }
            list.Delete();
            foreach (var kid in affectedKinds)
            {
                ArticleCategory.UpdateDetailCount(kid);
            }
        });

        private IActionResult ExecuteBatchArticleAction(string ids, string actionName, Action<IList<Article>> operation)
        {
            if (string.IsNullOrEmpty(ids))
            {
                tip.Message = "请最少选择一篇文章！";
                return Json(tip);
            }

            List<int> listIds;
            try
            {
                listIds = JsonConvert.DeserializeObject<List<int>>(ids);
            }
            catch (JsonException)
            {
                tip.Message = "参数格式错误！";
                return Json(tip);
            }

            if (listIds == null || listIds.Count < 1 || listIds.Count > 200)
            {
                tip.Message = "请至少选择一篇文章（单次最多 200 篇）！";
                return Json(tip);
            }

            IList<Article> list = Article.FindAll(Article._.Id.In(listIds), null, null, 0, 0);
            if (list == null || list.Count < 1)
            {
                tip.Message = "请至少选择一篇文章！";
                return Json(tip);
            }

            Admin my = Admin.GetMyInfo();
            foreach (var item in list)
            {
                if (!CanAccessArticle(my, item.KId, item.AuthorId, out var deniedReason))
                {
                    tip.Message = deniedReason;
                    return Json(tip);
                }
            }

            operation(list);
            Admin.WriteLogActions($"批量{actionName}文章(数量:{list.Count});");
            tip.Message = $"批量{actionName}文章成功！";
            tip.Status = JsonTip.SUCCESS;
            return Json(tip);
        }
        #endregion

        #region 权限与校验辅助方法
        internal static bool CanAccessArticle(Admin admin, int categoryId, int? authorId, out string reason)
        {
            reason = null;
            if (admin?.Roles == null)
            {
                reason = "当前登录状态无效！";
                return false;
            }
            if (admin.Roles.IsSuperAdmin == 1) return true;

            var aclist = ParseAuthorizedIds(admin.Roles.AuthorizedArticleCagegory);
            if (aclist.Count > 0 && !aclist.Contains(categoryId))
            {
                reason = "当前选择栏目不存在，或者您没有该栏目的操作权限！";
                return false;
            }
            if (authorId.HasValue && admin.Roles.OnlyEditMyselfArticle == 1 && authorId.Value != admin.Id)
            {
                reason = "系统限制，您无法操作非自己添加的文章！";
                return false;
            }
            return true;
        }

        private static List<int> ParseAuthorizedIds(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return new List<int>();
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
            }
            catch (JsonException)
            {
                return new List<int>();
            }
        }

        private static bool ValidateCategoryFilePath(string filePath, int excludeId, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(filePath)) return true;
            if (!filePath.StartsWith("/", StringComparison.Ordinal))
            {
                error = "栏目路径请以/开头！";
                return false;
            }
            if (filePath.EndsWith("/", StringComparison.Ordinal))
            {
                error = "栏目路径结尾不用加上/";
                return false;
            }
            if (filePath.Count(x => x == '/') > 4)
            {
                error = "最多只能四级路径！";
                return false;
            }
            if (!AdminUtils.CheckFilePathIsOK(filePath, excludeId, 0))
            {
                error = "栏目路径不可用，请重新填写！";
                return false;
            }
            return true;
        }

        private static bool ValidateAllowIpList(string allowIp, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(allowIp)) return true;
            var rows = allowIp.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawRow in rows)
            {
                var row = rawRow.Trim();
                if (string.IsNullOrEmpty(row)) continue;
                if (row.Contains('-'))
                {
                    var arrIps = row.Split('-', StringSplitOptions.None);
                    if (arrIps.Length != 2)
                    {
                        error = "每一行只能有一个“-”，如果需要限制多个ip段，请换行再输入！";
                        return false;
                    }
                    var startIp = arrIps[0].Trim();
                    var endIp = arrIps[1].Trim();
                    if (!Utils.IsIP(startIp) || !Utils.IsIP(endIp))
                    {
                        error = "您输入的IP地址有误：" + row;
                        return false;
                    }
                    if (Utils.GetLongIP(startIp) > Utils.GetLongIP(endIp))
                    {
                        error = "您输入的IP地址段有误，请按小到大输入";
                        return false;
                    }
                }
                else if (!Utils.IsIP(row))
                {
                    error = "您输入的IP地址有误：" + row;
                    return false;
                }
            }
            return true;
        }

        private static readonly Lazy<List<string>> CachedArticleTemplates = new(() =>
        {
            var listTpls = new List<string>();
            foreach (var asmItem in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asmItem.GetName().Name?.Contains("COMCMS.Web", StringComparison.OrdinalIgnoreCase) != true) continue;
                foreach (var type in asmItem.GetTypes().Where(e => e.Name.StartsWith("Views_Article", StringComparison.Ordinal)))
                {
                    listTpls.Add(type.Name.Replace("Views_Article_", "") + ".cshtml");
                }
            }
            return listTpls;
        });

        private static List<string> GetArticleTemplateNames() => CachedArticleTemplates.Value;
        #endregion
    }
}
