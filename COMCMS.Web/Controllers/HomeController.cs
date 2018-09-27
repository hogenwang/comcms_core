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

namespace COMCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public static IConfiguration Configuration { get; set; }
        public ConcurrentDictionary<string, string> AllSession = new ConcurrentDictionary<string, string>();
        public HomeController()
        {
            Configuration = new ConfigurationBuilder()
.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
.Build();

        }
        public IActionResult Index()
        {
            CookieOptions op = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10),
                Path = "/",
                HttpOnly = true
            };
            Response.Cookies.Append("abc", "123456", op);//用户名

            

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            CookiesHelper.WriteCookie("abc", Utils.GetRandomChar(20), 10);
            //sessiong
            SessionHelper.WriteSession("bcd", Utils.GetRandomChar(20));
            HttpContext.Session.SetString("abc", "123456");

            return View();
        }

        public IActionResult Contact()
        {
            Link l = Link.Find(Link._.Id == 1);
            l.Title = "😂😂😂😂";
            l.Save();
            return View(l);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 测试
        public IActionResult Test()
        {
            var css = Configuration.GetSection("connectionStrings");
            var list = css.GetChildren();
            if(list !=null && list.Count() > 0)
            {

            }
            string s = "";
            foreach (var item in list)
            {
                string connName = item.Path.Substring(item.Path.IndexOf(":") + 1);
                string connectionString = css.GetSection(connName).GetSection("connectionString").Value;
                string providerName = css.GetSection(connName).GetSection("providerName").Value;
                s += item.Path.Substring(item.Path.IndexOf(":")+1)+ "|"+ Configuration.GetSection(item.Path+ ":connectionString").Value+"|" + Configuration.GetSection(item.Path + ":providerName").Value +"|"+connectionString+"|"+ providerName +"|||" + list.Count();
            }

            return Content(s);
        }

        public IActionResult Test2()
        {
            var settings = "appsettings.json".GetFullPath();
            var css2 = new ConfigurationBuilder().AddJsonFile(settings).Build().GetSection("connectionStrings");

            string s = "";
            if (css2 != null)
            {
                foreach (var item in css2.GetChildren())
                {
                    var name = item.Path.Substring(item.Path.IndexOf(":") + 1);// item["name"];
                    var constr = item["connectionString"];
                    var provider = item["providerName"];
                    s += $"{name}|{constr}|{provider}|||";
                    //var type = DbFactory.GetProviderType(constr, provider);
                    //if (type == null) XTrace.WriteLine("无法识别{0}的提供者{1}！", name, provider);

                    //cs.Add(name, constr);
                    //_connTypes.Add(name, type);
                }

            }

            return Content(s);
        }
        #endregion

        #region MyRegion
        public IActionResult Test3()
        {
            bool isok = false;
            if(AllSession.TryAdd(HttpContext.Session.Id, HttpContext.Session.Id))
            {
                isok = true;
            }

            return Content(isok.ToString() + ":" + JsonConvert.SerializeObject(AllSession));
        }
        #endregion

        #region 测试
        [HttpGet]
        public IActionResult Article(string title)
        {
            return Content(title);
        }
        #endregion


    }
}
