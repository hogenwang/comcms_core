using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Web.Models;
using COMCMS.Common;
using COMCMS.Core;
using XCode;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using COMCMS.Web.Common;

namespace COMCMS.Web.Controllers
{
    public class HomeController : HomeBaseController
    {
        #region 首页
        public IActionResult Index()
        {
            ViewBag.cfg = cfg;
            ViewBag.about = Article.FindById(1);//关于我们
            return View("~/Views/Home/Index.cshtml");
        }
        #endregion


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 测试
        //[Route("c/{*path}/index2.html")]
        //public IActionResult Test(string path = "")
        //{
        //    string tel = "13332835377";
        //    return Content(tel.Substring(tel.Length - 5) + ";path:" + path);
        //}
        //[Route("{path1:regex([[a-zA-Z0-9-]])}/{path2:regex([[a-zA-Z0-9-]])}/index.html")]
        //public IActionResult Test2(string path1 = "", string path2 = "")
        //{
        //    ArticleController ac = new ArticleController();
        //    return ac.Index(1);
        //    //return Content(";ath1:" + path1+";path2:"+path2);
        //}
        #endregion

        #region 测试拼音
        public IActionResult Test3()
        {
            var mac = Utils.GetMacs().ToList();
            string re = "";
            foreach (var item in mac)
            {
                if (string.IsNullOrEmpty(re))
                    re = item.ToHex();
                else
                    re += "-" + item.ToHex();
            }
            return Content(re);
        }
        #endregion

        #region 系统文章栏目、商品栏目路由
        /// <summary>
        /// 一级路径栏目
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Route("{path:regex([[a-zA-Z0-9-]])}/index.html")]
        [Route("{path:regex([[a-zA-Z0-9-]])}/index-{page:int}.html")]
        [Route("{path:regex([[a-zA-Z0-9-]])}/")]
        public IActionResult ShowCategory(string path,int page=1)
        {
            //先判断文章栏目
            ArticleCategory articleCategory = ArticleCategory.FindByFilePath("/" + path);
            if(articleCategory != null)
            {
                //return Content(articleCategory.Id.ToString());
                ArticleController ac = new ArticleController();
                return ac.Index(articleCategory.Id);
            }
            //再判断商品
            Category category = Category.FindByFilePath("/" + path);
            if(category != null)
            {
                ProductController pc = new ProductController();
                return pc.Index(category.Id,page);
            }
            return EchoTip("系统找不到本栏目！"+"/"+path);
        }
        #endregion

    }
}
