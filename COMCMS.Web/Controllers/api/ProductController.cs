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
using NewLife.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.WxOpen;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COMCMS.Web.Controllers.api
{
    public class ProductController : APIBaseController
    {
        #region 获取分类列表
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetCategories(int level, int pid = 0)
        {
            var list = Category.GetListTree(pid, level, false, false);

            List<object> relist = new List<object>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    relist.Add(new
                    {
                        id = item.Id,
                        kindName = item.KindName,
                        subTitle = item.SubTitle,
                        pic = item.Pic,
                        banner = item.BannerImg,
                        description = item.Description
                    });
                }
            }
            reJson.code = 0;
            reJson.message = "获取成功";
            reJson.detail = relist;
            return reJson;
        }
        #endregion

        #region 获取商品列表
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="kid">分类ID</param>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetProductList(int kid, int page, int pageSize = 10)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var where = Product._.IsHide == 0;
            if (kid > 0)
                where &= Product._.KId == kid;

            if (page <= 0) page = 1;
            //计算分页
            int numPerPage, currentPage, startRowIndex;

            numPerPage = pageSize;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;

            IList<Product> list = Product.FindAll(where, Product._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Product.FindCount(where, Product._.Sequence.Asc(), null, startRowIndex, numPerPage);
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
                        price = item.Price,
                        marketPrice = item.MarketPrice,
                        pic = item.Pic,
                        description = item.Description,
                        stock = item.Stock,
                        addTime = item.AddTime.ToString("yyyy-MM-dd HH:mm")
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

        #region 商品详情
        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="id">商品id</param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetProductDetail(int id, string random = "", string timeStamp = "", string signature = "")
        {
            //获取商品
            Product entity = Product.FindById(id);
            if (entity == null || entity.IsHide == 1)
            {
                reJson.code = 40000;
                reJson.message = "系统找不到本记录";
                return reJson;
            }
            entity.Hits++;
            entity.Update();
            return ToPublicProduct(entity);
        }
        #endregion

        #region 搜索商品
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="random"></param>
        /// <param name="timeStamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetSearchProductList(string key, int page, int pageSize = 10, int kid = 0, string random = "", string timeStamp = "", string signature = "")
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var where = Product._.IsHide == 0;

            if (kid != 0)
            {
                where &= Product._.KId == kid;
            }

            if (!string.IsNullOrEmpty(key))
            {
                key = Utils.NoHTML(key);
                where &= Product._.Title.Contains(key);
            }

            if (page <= 0) page = 1;
            //计算分页
            int numPerPage, currentPage, startRowIndex;

            numPerPage = pageSize;
            currentPage = page;
            startRowIndex = (currentPage - 1) * numPerPage;

            IList<Product> list = Product.FindAll(where, Product._.Sequence.Asc(), null, startRowIndex, numPerPage);
            long totalCount = Product.FindCount(where, Product._.Sequence.Asc(), null, startRowIndex, numPerPage);
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
                        price = item.Price,
                        marketPrice = item.MarketPrice,
                        pic = item.Pic,
                        description = item.Description,
                        stock = item.Stock,
                        addTime = item.AddTime.ToString("yyyy-MM-dd HH:mm")
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

        private static object ToPublicProduct(Product entity)
        {
            return new
            {
                id = entity.Id,
                categoryId = entity.KId,
                title = entity.Title,
                subTitle = entity.SubTitle,
                content = entity.Content,
                parameters = entity.Parameters,
                description = entity.Description,
                price = entity.Price,
                marketPrice = entity.MarketPrice,
                specialPrice = entity.SpecialPrice,
                fare = entity.Fare,
                stock = entity.Stock,
                pic = entity.Pic,
                banner = entity.BannerImg,
                images = entity.ItemImg,
                tags = entity.Tags,
                service = entity.Service,
                hits = entity.Hits,
                addTime = entity.AddTime
            };
        }


    }
}
