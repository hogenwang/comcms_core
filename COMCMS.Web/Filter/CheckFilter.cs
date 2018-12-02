using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Common;
using COMCMS.Web.Controllers.api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace COMCMS.Web.Filter
{
    public class CheckFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = string.Empty;

                foreach (var key in context.ModelState.Keys)
                {
                    var state = context.ModelState[key];
                    if (state.Errors.Any())
                    {
                        error = string.Join("\r\n", state.Errors.ToList().ConvertAll(item => item.ErrorMessage));
                        if (error.Equals(string.Empty))
                            error = string.Join("\r\n", state.Errors.ToList().ConvertAll(item => item.Exception.Message));
                        break;
                    }
                }

                throw new Exception(error);
            }

            var notVali = ValiSignature(context.HttpContext);

            if (notVali != null)
            {
                // 过滤器不要抛出错误，也不要向响应流写东西，应该直接赋值Result进行短路
                context.Result = new ContentResult()
                {
                    Content = notVali.ToJson(),
                    ContentType = "application/json"
                };
            }
        }

        private ReJson ValiSignature(HttpContext context)
        {
            var Request = context.Request;

            var queryStrings = Request.Query;
            List<string> keys = new List<string>();
            //string[] keys = HttpContext.Current.Request.QueryString.AllKeys;
            if (queryStrings == null)
            {
                throw new Exception("queryStrings Is Null");
            }
            foreach (var item in queryStrings.Keys)
            {
                keys.Add(item);
            }
            SortedDictionary<string, string> pars = new SortedDictionary<string, string>();
            string signature = "";
            foreach (var k in keys)
            {
                if (k != "signature")
                {
                    string v = Request.Query[k];
                    pars.Add(k, v);
                }
                else
                {
                    signature = Request.Query[k];
                }
            }
            //没有签名返回错误
            if (string.IsNullOrEmpty(signature))
                return new ReJson(40004, "signature 为空！");

            if (!MySign.CheckSign(pars, signature))
            {

                return new ReJson(40004, "signature 错误！");
            }
            //判断是否超时
            string timeStamp = pars["timeStamp"];
            //判断时间有效性
            DateTime postTime = Utils.StampToDateTime(timeStamp);
            if (postTime < DateTime.Now.AddSeconds(-120))//30秒有效期
            {
                return new ReJson(40004, "signature 错误！", 1);
            }

            return null;
        }
    }
}
