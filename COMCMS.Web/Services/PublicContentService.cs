using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using COMCMS.Common;
using COMCMS.Core;
using XCode;

namespace COMCMS.Web.Services
{
    public sealed class PublicContentResult
    {
        public bool IsRedirect { get; init; }
        public string RedirectUrl { get; init; }
        public bool IsError { get; init; }
        public string ErrorMessage { get; init; }
        public string ViewPath { get; init; }
        public object Model { get; init; }
        public Dictionary<string, object> ViewData { get; } = new();

        public static PublicContentResult Error(string message) => new() { IsError = true, ErrorMessage = message };
        public static PublicContentResult Redirect(string url) => new() { IsRedirect = true, RedirectUrl = url };
    }

    public sealed class PublicContentService
    {
        private static readonly Regex SafeTemplateRegex = new("^[A-Za-z0-9_-]{1,64}\\.cshtml$", RegexOptions.Compiled);
        private readonly HitCounterService _hitCounter;

        public PublicContentService(HitCounterService hitCounter)
        {
            _hitCounter = hitCounter;
        }

        public PublicContentResult BuildArticleCategory(int id, int page, Config cfg)
        {
            var model = ArticleCategory.FindById(id);
            if (model == null)
                return PublicContentResult.Error("系统找不到本记录");

            if (!string.IsNullOrWhiteSpace(model.LinkURL))
            {
                if (Utils.IsInt(model.LinkURL))
                    return PublicContentResult.Redirect(ViewsHelper.EchoArticleURL(int.Parse(model.LinkURL)));

                if (IsSafeRedirectUrl(model.LinkURL))
                    return PublicContentResult.Redirect(model.LinkURL.Trim());
            }

            if (page < 1) page = 1;

            if (!string.IsNullOrWhiteSpace(model.AllowIp) && !Admin.IsAdminLogin() && !IsIpAllowed(model.AllowIp, Utils.GetIP()))
                return PublicContentResult.Error("您的IP不在允许范围内！");

            var result = new PublicContentResult
            {
                ViewPath = "~/Views/Article/" + ResolveSafeTemplateName(model.TemplateFile, "Index.cshtml"),
                Model = model
            };
            result.ViewData["cfg"] = cfg;

            if (model.IsList == 1)
            {
                int numPerPage = model.PageSize > 0 ? Math.Clamp(model.PageSize, 1, 100) : 10;
                int startRowIndex = (page - 1) * numPerPage;

                var ex = Article._.IsHide != 1 & Article._.IsDel != 1;
                if (model.IsShowSubDetail == 1)
                {
                    var allsubkids = new List<int> { model.Id };
                    IList<ArticleCategory> allkind = ArticleCategory.FindAllSubKinds(model.Id);
                    if (allkind != null && allkind.Count > 0)
                    {
                        foreach (var s in allkind)
                            allsubkids.Add(s.Id);
                    }
                    ex &= Article._.KId.In(allsubkids);
                }
                else
                {
                    ex &= Article._.KId == model.Id;
                }

                long totalCount = Article.FindCount(ex, null, null, 0, 0);
                int pageCount = (int)((totalCount + numPerPage - 1) / numPerPage);
                IList<Article> list = Article.FindAll(ex, Article._.Sequence.Asc().And(Article._.Id.Desc()), null, startRowIndex, numPerPage);

                result.ViewData["list"] = list;
                result.ViewData["totalCount"] = totalCount;
                result.ViewData["pageCount"] = pageCount;
                result.ViewData["page"] = page;
                result.ViewData["pagesize"] = numPerPage;
            }

            return result;
        }

        public PublicContentResult BuildArticleDetail(int id, Config cfg)
        {
            var entity = Article.Find(Article._.Id == id);
            if (entity == null || entity.IsDel == 1)
                return PublicContentResult.Error("系统找不到本记录！");

            if (entity.IsHide == 1 && !Admin.IsAdminLogin())
                return PublicContentResult.Error("系统找不到本文章！");

            var kind = ArticleCategory.FindById(entity.KId);
            if (kind == null)
                return PublicContentResult.Error("系统找不到本文章栏目！");

            if (!string.IsNullOrWhiteSpace(kind.AllowIp) && !Admin.IsAdminLogin() && !IsIpAllowed(kind.AllowIp, Utils.GetIP()))
                return PublicContentResult.Error("您的IP不在允许范围内！");

            _hitCounter.IncrementArticleHit(entity.Id);

            if (!string.IsNullOrWhiteSpace(entity.LinkURL) && IsSafeRedirectUrl(entity.LinkURL))
                return PublicContentResult.Redirect(entity.LinkURL.Trim());

            var result = new PublicContentResult
            {
                ViewPath = "~/Views/Article/" + ResolveSafeTemplateName(kind.DetailTemplateFile, "Detail.cshtml"),
                Model = entity
            };
            result.ViewData["kind"] = kind;
            result.ViewData["cfg"] = cfg;

            if (!string.IsNullOrEmpty(entity.ItemImg))
            {
                string[] arrimg = entity.ItemImg.Split(new[] { "|||" }, StringSplitOptions.None);
                var listimg = new List<string>();
                foreach (string s in arrimg)
                {
                    if (!string.IsNullOrEmpty(s) && Utils.IsImgFilename(s))
                        listimg.Add(s.Trim());
                }
                result.ViewData["ListImg"] = listimg;
            }

            return result;
        }

        public PublicContentResult BuildProductCategory(int id, int page, Config cfg)
        {
            Category entity;
            if (id > 0)
            {
                entity = Category.FindById(id);
                if (entity == null)
                    return PublicContentResult.Error("系统找不到本分类！");
            }
            else
            {
                entity = new Category
                {
                    Id = 0,
                    KindName = "所有分类",
                    Keyword = "所有分类",
                    Description = "所有分类",
                    PageSize = 10
                };
            }

            if (page < 1) page = 1;
            int numPerPage = entity.PageSize > 0 ? Math.Clamp(entity.PageSize, 1, 100) : 10;
            int startRowIndex = (page - 1) * numPerPage;

            Expression ex = Product._.IsHide == 0;
            if (entity.Id > 0)
                ex &= Product._.KId == entity.Id;

            IList<Product> list = Product.FindAll(ex, null, null, startRowIndex, numPerPage);
            long totalCount = Product.FindCount(ex, null, null, 0, 0);

            var result = new PublicContentResult
            {
                ViewPath = "~/Views/Product/" + ResolveSafeTemplateName(entity.TemplateFile, "Index.cshtml"),
                Model = entity
            };
            result.ViewData["totalCount"] = totalCount;
            result.ViewData["page"] = page;
            result.ViewData["pagesize"] = numPerPage;
            result.ViewData["cfg"] = cfg;
            result.ViewData["list"] = list;
            return result;
        }

        public PublicContentResult BuildProductDetail(int id, Config cfg)
        {
            var entity = Product.FindById(id);
            if (entity == null || (entity.IsHide == 1 && !Admin.IsAdminLogin()))
                return PublicContentResult.Error("系统找不到本记录！");

            var kind = Category.FindById(entity.KId);
            if (kind == null)
                return PublicContentResult.Error("系统找不到本商品分类！");

            _hitCounter.IncrementProductHit(entity.Id);

            var result = new PublicContentResult
            {
                ViewPath = "~/Views/Product/" + ResolveSafeTemplateName(kind.DetailTemplateFile, "Detail.cshtml"),
                Model = entity
            };
            result.ViewData["cfg"] = cfg;
            result.ViewData["category"] = kind;

            if (!string.IsNullOrEmpty(entity.ItemImg))
            {
                string[] arrimg = entity.ItemImg.Split(new[] { "|||" }, StringSplitOptions.None);
                var listimg = new List<string>();
                foreach (string s in arrimg)
                {
                    if (!string.IsNullOrEmpty(s) && Utils.IsImgFilename(s))
                        listimg.Add(s.Trim());
                }
                result.ViewData["ListImg"] = listimg;
            }

            return result;
        }

        internal static string ResolveSafeTemplateName(string candidate, string defaultName)
        {
            if (string.IsNullOrWhiteSpace(candidate))
                return defaultName;

            var trimmed = candidate.Trim();
            if (!SafeTemplateRegex.IsMatch(trimmed))
                return defaultName;

            return trimmed;
        }

        internal static bool IsIpAllowed(string allowIp, string clientIp)
        {
            if (string.IsNullOrWhiteSpace(allowIp))
                return true;
            if (string.IsNullOrWhiteSpace(clientIp))
                return false;

            long myIp = Utils.GetLongIP(clientIp.Trim());
            if (myIp <= 0)
                return false;

            string[] rows = allowIp.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawRow in rows)
            {
                var row = rawRow.Trim();
                if (string.IsNullOrEmpty(row))
                    continue;

                if (row.Contains('-'))
                {
                    var parts = row.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        long ip1 = Utils.GetLongIP(parts[0].Trim());
                        long ip2 = Utils.GetLongIP(parts[1].Trim());
                        if (ip1 > 0 && ip2 > 0 && ip1 <= myIp && myIp <= ip2)
                            return true;
                    }
                }
                else
                {
                    long allowedIp = Utils.GetLongIP(row);
                    if (allowedIp > 0 && myIp == allowedIp)
                        return true;
                }
            }

            return false;
        }

        internal static bool IsSafeRedirectUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            var trimmed = url.Trim();
            if (trimmed.StartsWith("/", StringComparison.Ordinal) &&
                !trimmed.StartsWith("//", StringComparison.Ordinal) &&
                !trimmed.StartsWith("/\\", StringComparison.Ordinal))
            {
                return true;
            }

            return Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
