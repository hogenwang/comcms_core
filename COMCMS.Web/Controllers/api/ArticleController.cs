using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common;
using COMCMS.Core;
using COMCMS.Core.Models;
using XCode;
using System.Web;
using COMCMS.Web.Filter;
using NewLife.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.WxOpen;

namespace COMCMS.Web.Controllers.api
{
    public class ArticleController : APIBaseController
    {
        #region 获取文章列表
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="kid"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [CheckFilter]
        public object GetArticleList(int kid, int pagesize = 10, int page = 1)
        {
            var where = Article._.IsHide != 1;
            if (kid > 0)
                where &= Article._.KId == kid;

            if (page <= 0) page = 1;
            //计算分页
            int numPerPage, currentPage, startRowIndex;

            numPerPage = pagesize;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;

            IList<Article> list = Article.FindAll(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            List<object> relist = new List<object>();
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

        #region 获取文章列表
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="kid"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [CheckFilter]
        public object GetArticleListWidthSub(int kid, int pagesize = 10, int page = 1, string key = "")
        {

            var where = Article._.IsHide != 1;
            if (kid > 0)
            {
                //获取下属
                List<int> kids = new List<int>();
                kids.Add(kid);
                IList<ArticleCategory> subkinds = ArticleCategory.FindByParentID(kid);
                if (subkinds != null && subkinds.Count > 0)
                {
                    foreach (var item in subkinds)
                    {
                        kids.Add(item.Id);
                    }
                }
                where &= Article._.KId.In(kids);
            }
            if (!string.IsNullOrEmpty(key))
            {
                where &= Article._.Title.Contains(key);
            }
            if (page <= 0) page = 1;
            //计算分页
            int numPerPage, currentPage, startRowIndex;

            numPerPage = pagesize;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;

            IList<Article> list = Article.FindAll(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Article.FindCount(where, Article._.Sequence.Asc(), null, startRowIndex, numPerPage);
            List<object> relist = new List<object>();
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
        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        [CheckFilter]
        public object GetArticleDetail(int id)
        {
            //获取商品
            Article entity = Article.FindById(id);
            if (entity == null || entity.IsHide == 1)
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本记录";
                return reJson;
            }
            entity.Hits++;
            entity.Update();
            return entity;
        }
        #endregion

        #region 获取文章栏目详情，并带下一级栏目
        [HttpGet]
        [CheckFilter]
        public object GetArticleCategory(int id)
        {
            ArticleCategory entity = ArticleCategory.FindById(id);
            if (entity == null)
            {
                reJson.message = "系统找不到本记录";
                return reJson;
            }

            //获取下属栏目
            IList<ArticleCategory> listsub = ArticleCategory.FindAll(ArticleCategory._.PId == entity.Id & ArticleCategory._.IsHide == 0, ArticleCategory._.Rank.Asc(), null, 0, 0);

            List<object> reListSubs = new List<object>();
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
                category = entity,
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
