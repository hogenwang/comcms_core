using System;
using System.Collections.Generic;
using COMCMS.Core;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using XCode;

namespace COMCMS.Web.Controllers.api
{
    public class ArticleController : APIBaseController
    {
        private readonly HitCounterService _hitCounter;

        public ArticleController(HitCounterService hitCounter)
        {
            _hitCounter = hitCounter;
        }

        #region 获取文章列表
        [HttpGet]
        public object GetArticleList(int kid, int pagesize = 10, int page = 1)
        {
            pagesize = Math.Clamp(pagesize, 1, 100);
            var where = Article._.IsHide != 1 & Article._.IsDel != 1;
            if (kid > 0)
                where &= Article._.KId == kid;

            if (page <= 0) page = 1;
            int numPerPage = pagesize;
            int startRowIndex = (page - 1) * numPerPage;

            IList<Article> list = Article.FindAll(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(where, null, null, 0, 0);
            var relist = new List<object>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    relist.Add(new
                    {
                        id = item.Id,
                        title = item.Title,
                        subTitle = item.SubTitle,
                        pic = item.Pic,
                        description = item.Description,
                        addTime = item.AddTime.ToString("yyyy-MM-dd HH:mm"),
                        origin = item.Origin
                    });
                }
            }
            dynamic re = new { total = totalCount, page = page, list = relist };

            reJson.code = 0;
            reJson.message = "获取成功";
            reJson.detail = re;
            return reJson;
        }
        #endregion

        #region 获取文章列表（含子栏目）
        [HttpGet]
        public object GetArticleListWidthSub(int kid, int pagesize = 10, int page = 1, string key = "")
        {
            pagesize = Math.Clamp(pagesize, 1, 100);

            var where = Article._.IsHide != 1 & Article._.IsDel != 1;
            if (kid > 0)
            {
                var kids = new List<int> { kid };
                IList<ArticleCategory> subkinds = ArticleCategory.FindByParentID(kid);
                if (subkinds != null && subkinds.Count > 0)
                {
                    foreach (var item in subkinds)
                        kids.Add(item.Id);
                }
                where &= Article._.KId.In(kids);
            }
            if (!string.IsNullOrEmpty(key))
            {
                where &= Article._.Title.Contains(key);
            }
            if (page <= 0) page = 1;
            int numPerPage = pagesize;
            int startRowIndex = (page - 1) * numPerPage;

            IList<Article> list = Article.FindAll(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(where, null, null, 0, 0);
            var relist = new List<object>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    relist.Add(new
                    {
                        id = item.Id,
                        title = item.Title,
                        subTitle = item.SubTitle,
                        pic = item.Pic,
                        description = item.Description,
                        addTime = item.AddTime.ToString("yyyy-MM-dd HH:mm"),
                        origin = item.Origin
                    });
                }
            }
            dynamic re = new { total = totalCount, page = page, list = relist };

            reJson.code = 0;
            reJson.message = "获取成功";
            reJson.detail = re;
            return reJson;
        }
        #endregion

        #region 获取文章详情
        [HttpGet]
        public object GetArticleDetail(int id)
        {
            Article entity = Article.FindById(id);
            if (entity == null || entity.IsHide == 1 || entity.IsDel == 1)
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本记录";
                return reJson;
            }
            ArticleCategory category = ArticleCategory.FindById(entity.KId);
            if (category == null || category.IsHide == 1 || !string.IsNullOrWhiteSpace(category.AllowIp))
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本记录";
                return reJson;
            }
            _hitCounter.IncrementArticleHit(entity.Id);
            reJson.code = 0;
            reJson.message = "获取成功";
            reJson.detail = new
            {
                id = entity.Id,
                categoryId = entity.KId,
                title = entity.Title,
                subTitle = entity.SubTitle,
                content = entity.Content,
                description = entity.Description,
                pic = entity.Pic,
                banner = entity.BannerImg,
                images = entity.ItemImg,
                tags = entity.Tags,
                origin = entity.Origin,
                originUrl = entity.OriginURL,
                hits = entity.Hits,
                addTime = entity.AddTime
            };
            return reJson;
        }
        #endregion

        #region 获取文章栏目详情，并带下一级栏目
        [HttpGet]
        public object GetArticleCategory(int id)
        {
            ArticleCategory entity = ArticleCategory.FindById(id);
            if (entity == null || entity.IsHide == 1 || !string.IsNullOrWhiteSpace(entity.AllowIp))
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本记录";
                return reJson;
            }

            IList<ArticleCategory> listsub = ArticleCategory.FindAll(
                ArticleCategory._.PId == entity.Id & ArticleCategory._.IsHide == 0,
                ArticleCategory._.Rank.Asc(), null, 0, 0);

            var reListSubs = new List<object>();
            if (listsub != null && listsub.Count > 0)
            {
                foreach (var item in listsub)
                {
                    reListSubs.Add(new
                    {
                        kindName = item.KindName,
                        id = item.Id
                    });
                }
            }
            dynamic detail = new
            {
                category = new
                {
                    id = entity.Id,
                    parentId = entity.PId,
                    kindName = entity.KindName,
                    subTitle = entity.SubTitle,
                    description = entity.Description,
                    banner = entity.BannerImg,
                    pic = entity.Pic,
                    content = entity.KindInfo
                },
                sublist = reListSubs
            };

            reJson.code = 0;
            reJson.message = "获取成功！";
            reJson.detail = detail;
            return reJson;
        }
        #endregion
    }
}
