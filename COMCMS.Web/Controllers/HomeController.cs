using System;
using System.Diagnostics;
using System.IO;
using COMCMS.Common;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Common;
using COMCMS.Web.Models;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace COMCMS.Web.Controllers
{
    public class HomeController : HomeBaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly PublicContentService _contentService;

        public HomeController(IWebHostEnvironment env, PublicContentService contentService)
        {
            _env = env;
            _contentService = contentService;
        }

        #region 首页
        public IActionResult Index()
        {
            bool hasData = AdminMenu.FindCount(null, null, null, 0, 0) > 0;
            if (!hasData && _env.IsDevelopment())
            {
                return Redirect("/Home/Install");
            }

            ViewBag.cfg = cfg;
            ViewBag.about = Article.FindById(1);
            return View("~/Views/Home/Index.cshtml");
        }
        #endregion

        #region 显示默认错误页面
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region 初始化安装（仅 Development 环境可用）
        [HttpGet]
        public IActionResult Install()
        {
            if (!_env.IsDevelopment())
                return NotFound();

            string lockPath = Path.Combine(_env.WebRootPath, "install.lock");
            if (System.IO.File.Exists(lockPath))
            {
                return EchoTip("COMCMS系统已经安装过，如果需要重新安装，请删除程序wwwroot目录下的install.lock文件！", false, "/");
            }

            string sqltype = "mysql";
            string sqlProviderName = Utils.GetSetting("connectionStrings:dbconn:providerName");
            if (string.Equals(sqlProviderName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(sqlProviderName, "Microsoft.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
            {
                sqltype = "mssql";
            }

            bool hasData = AdminMenu.FindCount(null, null, null, 0, 0) > 0;
            ViewBag.hasData = hasData;
            ViewBag.sqltype = sqltype;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoInstall()
        {
            if (!_env.IsDevelopment())
                return NotFound();

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

            if (username.Trim().Length < 5)
            {
                tip.Message = "用户名不能小于5个字符！";
                return Json(tip);
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 12 || password.Length > 128)
            {
                tip.Message = "管理员密码长度必须为 12 到 128 个字符！";
                return Json(tip);
            }

            if (password != password2)
            {
                tip.Message = "两次输入密码不一致，请重新输入！";
                return Json(tip);
            }

            string sqltype = "mysql";
            string sqlProviderName = Utils.GetSetting("connectionStrings:dbconn:providerName");
            if (string.Equals(sqlProviderName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(sqlProviderName, "Microsoft.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
            {
                sqltype = "mssql";
            }

            if (sqltype == "mysql" && string.IsNullOrEmpty(mysqltype))
            {
                tip.Message = "请选择系统Mysql服务器类型！";
                return Json(tip);
            }

            string lockPath = Path.Combine(_env.WebRootPath, "install.lock");
            if (System.IO.File.Exists(lockPath))
            {
                tip.Message = "COMCMS系统已经安装过，如果需要重新安装，请删除程序wwwroot目录下的install.lock文件！";
                return Json(tip);
            }

            string sqlname = "comcms_sqlserver.sql";
            if (sqltype == "mysql")
            {
                sqlname = string.Equals(mysqltype, "linux", StringComparison.OrdinalIgnoreCase)
                    ? "comcms_mysql_linux.sql"
                    : "comcms_mysql.sql";
            }

            string sqlPath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "..", "database", "install"));
            string sqlFullPath = Path.Combine(sqlPath, sqlname);

            if (!System.IO.File.Exists(sqlFullPath))
            {
                tip.Message = "缺少初始化SQL文件！";
                return Json(tip);
            }

            string fullsql;
            try
            {
                fullsql = System.IO.File.ReadAllText(sqlFullPath);
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = $"读取初始化数据失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }

            if (sqltype == "mssql")
            {
                fullsql = fullsql.Replace("GO", ";");
            }

            try
            {
                Admin.FindAll(fullsql);
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = $"初始化数据库失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }

            Config siteCfg = Config.FindById(1);
            if (siteCfg != null)
            {
                try
                {
                    siteCfg.SiteName = sitename;
                    siteCfg.SiteUrl = siteurl;
                    siteCfg.Update();
                }
                catch (Exception ex)
                {
                    NewLife.Log.XTrace.WriteException(ex);
                    tip.Message = $"初始化配置失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                    return Json(tip);
                }
            }

            Admin admin = Admin.Find(Admin._.Id == 1);
            try
            {
                if (admin == null)
                {
                    admin = new Admin
                    {
                        UserName = username,
                        RealName = username,
                        UserLevel = 100,
                        RoleId = 1,
                        GroupId = 0,
                        LastLoginTime = DateTime.Now,
                        ThisLoginTime = DateTime.Now,
                        LastLoginIP = Utils.GetIP(),
                        ThisLoginIP = Utils.GetIP(),
                        IsLock = 0
                    };
                }
                else
                {
                    admin.UserName = username;
                }
                admin.Salt = Utils.GetRandomChar(10);
                admin.PassWord = PasswordHashService.HashPassword(password);
                admin.Save();
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = "创建管理员账户时发生错误，请检查服务端日志。";
                return Json(tip);
            }

            try
            {
                System.IO.File.WriteAllText(lockPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                NewLife.Log.XTrace.WriteException(ex);
                tip.Message = $"完成安装失败，请使用 TraceId {HttpContext.TraceIdentifier} 联系管理员。";
                return Json(tip);
            }

            tip.Message = "导入数据成功！";
            tip.Status = JsonTip.SUCCESS;
            tip.ReturnUrl = "/";
            return Json(tip);
        }
        #endregion

        #region 系统文章栏目、商品栏目路由
        private IActionResult RenderCategoryByPath(string fullPath, int page)
        {
            ArticleCategory articleCategory = ArticleCategory.FindByFilePath(fullPath);
            if (articleCategory != null)
                return RenderContentResult(_contentService.BuildArticleCategory(articleCategory.Id, page, cfg));

            Category category = Category.FindByFilePath(fullPath);
            if (category != null)
                return RenderContentResult(_contentService.BuildProductCategory(category.Id, page, cfg));

            return EchoTip("系统找不到本栏目！");
        }

        [Route("{path:regex(^(?!swagger\\b)[[a-zA-Z0-9-]]+$)}/index.html")]
        [Route("{path:regex(^(?!swagger\\b)[[a-zA-Z0-9-]]+$)}/index-{page:int}.html")]
        public IActionResult ShowCategory(string path, int page = 1) =>
            RenderCategoryByPath("/" + path, page);

        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/index.html")]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/index-{page:int}.html")]
        public IActionResult ShowCategory(string path, string path2, int page = 1) =>
            RenderCategoryByPath($"/{path}/{path2}", page);

        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/index.html")]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/index-{page:int}.html")]
        public IActionResult ShowCategory(string path, string path2, string path3, int page = 1) =>
            RenderCategoryByPath($"/{path}/{path2}/{path3}", page);

        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/{path4:regex(^[[a-zA-Z0-9-]]+$)}/index.html")]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/{path4:regex(^[[a-zA-Z0-9-]]+$)}/index-{page:int}.html")]
        public IActionResult ShowCategory(string path, string path2, string path3, string path4, int page = 1) =>
            RenderCategoryByPath($"/{path}/{path2}/{path3}/{path4}", page);
        #endregion

        #region 文章详情、商品详情
        [HttpGet]
        [Route("{path:regex(^(?!swagger\\b)[[a-zA-Z0-9-]]+$)}/{filename:regex(^(?!index\\b)[[a-zA-Z0-9-]]+$)}.html")]
        public IActionResult ShowDetail(string path, string filename)
        {
            string fullPath = $"/{path}";
            string htmlFilename = filename + ".html";

            ArticleCategory articleCategory = ArticleCategory.FindByFilePath(fullPath);
            if (articleCategory != null)
            {
                Article article = Utils.IsInt(filename) ? Article.FindById(int.Parse(filename)) : null;
                if (article == null)
                    article = Article.FindByFileName(htmlFilename, articleCategory.Id);

                if (article != null)
                    return RenderContentResult(_contentService.BuildArticleDetail(article.Id, cfg));
            }

            Category category = Category.FindByFilePath(fullPath);
            if (category != null)
            {
                Product product = Utils.IsInt(filename) ? Product.FindById(int.Parse(filename)) : null;
                if (product == null)
                    product = Product.FindByFileName(htmlFilename, category.Id);

                if (product != null)
                    return RenderContentResult(_contentService.BuildProductDetail(product.Id, cfg));
            }

            return EchoTip("系统找不到本详情！");
        }

        [HttpGet]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{filename:regex(^(?!index\\b)[[a-zA-Z0-9-]]+$)}.html")]
        public IActionResult ShowDetail(string path, string path2, string filename) =>
            ShowDetail($"{path}/{path2}", filename);

        [HttpGet]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/{filename:regex(^(?!index\\b)[[a-zA-Z0-9-]]+$)}.html")]
        public IActionResult ShowDetail(string path, string path2, string path3, string filename) =>
            ShowDetail($"{path}/{path2}/{path3}", filename);

        [HttpGet]
        [Route("{path:regex(^[[a-zA-Z0-9-]]+$)}/{path2:regex(^[[a-zA-Z0-9-]]+$)}/{path3:regex(^[[a-zA-Z0-9-]]+$)}/{path4:regex(^[[a-zA-Z0-9-]]+$)}/{filename:regex(^(?!index\\b)[[a-zA-Z0-9-]]+$)}.html")]
        public IActionResult ShowDetail(string path, string path2, string path3, string path4, string filename) =>
            ShowDetail($"{path}/{path2}/{path3}/{path4}", filename);
        #endregion

        #region sitemap
        [Route("sitemap.html")]
        [HttpGet]
        public IActionResult SitemapHTML()
        {
            ViewBag.cfg = cfg;
            return View();
        }

        [HttpGet]
        [Route("sitemap.xml")]
        public IActionResult SitemapXML()
        {
            Response.ContentType = "text/xml";
            return View();
        }
        #endregion
    }
}
