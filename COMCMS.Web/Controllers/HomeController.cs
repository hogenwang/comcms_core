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
using Microsoft.AspNetCore.Hosting;

namespace COMCMS.Web.Controllers
{
    public class HomeController : HomeBaseController
    {
        private IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region 首页
        public IActionResult Index()
        {
            bool hasData = AdminMenu.FindCount(null, null, null, 0, 0) > 0;
            if (!hasData)
            {
                return Redirect("/Home/Install");
            }
            ViewBag.cfg = cfg;
            ViewBag.about = Article.FindById(1);//关于我们
            return View("~/Views/Home/Index.cshtml");
        }
        #endregion


        #region 显示默认错误页面
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region 初始化安装
        [HttpGet]
        public IActionResult Install()
        {
            string lockPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}install.lock";
            if (System.IO.File.Exists(lockPath))
            {
                return EchoTip("COMCMS系统已经安装过，如果需要重新安装，请删除程序wwwroot目录下的install.lock文件！", false, "/");
            }
            //判断数据库类型 mysql mssql 暂时只支持这两种
            string sqltype = "mysql";
            string sqlProviderName = Utils.GetSetting("connectionStrings:dbconn:providerName");
            if (sqlProviderName == "System.Data.SqlClient")
            {
                sqltype = "mssql";
            }
            bool hasData = AdminMenu.FindCount(null, null, null, 0, 0) > 0;
            ViewBag.hasData = hasData;
            ViewBag.sqltype = sqltype;
            return View();
        }
        #endregion

        #region 执行初始化数据库数据
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoInstall()
        {
            string sitename = Request.Form["sitename"];
            string siteurl = Request.Form["siteurl"];
            string mysqltype = Request.Form["mysqltype"];
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string password2 = Request.Form["password2"];

            if (string.IsNullOrEmpty(sitename))
            {
                tip.Message = "请填写网站名称！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(username))
            {
                tip.Message = "请输入用户名！";
                return Json(tip);
            }
            if (username.Trim().Length<5)
            {
                tip.Message = "用户名不能小于4个字符！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(password) || Utils.GetStringLength(password) < 5)
            {
                tip.Message = "登录密码不能为空或者长度小于5！";
                return Json(tip);
            }
            if(password != password2)
            {
                tip.Message = "两次输入密码不一致，请重新输入！";
                return Json(tip);
            }
            string sqltype = "mysql";
            string sqlProviderName = Utils.GetSetting("connectionStrings:dbconn:providerName");
            if (sqlProviderName == "System.Data.SqlClient")
            {
                sqltype = "mssql";
            }

            if(sqltype =="mysql" && string.IsNullOrEmpty(mysqltype))
            {
                tip.Message = "请选择系统Mysql服务器类型！";
                return Json(tip);
            }

            string lockPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}install.lock";
            if (System.IO.File.Exists(lockPath))
            {
                tip.Message = "COMCMS系统已经安装过，如果需要重新安装，请删除程序wwwroot目录下的install.lock文件！";
                return Json(tip);
            }

            //判断需要的sql文件
            string sqlname = "comcms_sqlserver.sql";
            if(sqltype == "mysql")
            {
                if (mysqltype == "linux")
                    sqlname = "comcms_for_linux.sql";
                else
                    sqlname = "comcms_for_windows.sql";
            }

            string sqlPath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}install{Path.DirectorySeparatorChar}";
            string sqlFullPath = Path.Combine(sqlPath, sqlname);

            if (!System.IO.File.Exists(sqlFullPath))
            {
                tip.Message = "缺少初始化SQL文件！";
                return Json(tip);
            }
            string fullsql = System.IO.File.ReadAllText(sqlFullPath);

            Admin.FindAll(fullsql);//执行

            //初始化
            Config cfg = Config.FindById(1);
            if(cfg != null)
            {
                cfg.SiteName = sitename;
                cfg.SiteUrl = siteurl;
                cfg.Update();
            }

            Admin admin = Admin.Find(Admin._.Id == 1);
            if(admin != null)
            {
                admin.UserName = username;
                admin.Salt = Utils.GetRandomChar(10);
                admin.PassWord = Utils.MD5(admin.Salt + password);
                admin.Update();
            }


            //写入Lock文件
            System.IO.File.WriteAllText(lockPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            tip.Message = "导入数据成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/";
            return Json(tip);
        }
        #endregion

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
