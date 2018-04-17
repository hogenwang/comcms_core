using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace COMCMS.Web.Controllers
{
    public class StatusCodeController : Controller
    {
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            NewLife.Log.XTrace.WriteLine($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
            ViewBag.url = "";
            return View(statusCode);
        }
    }
}