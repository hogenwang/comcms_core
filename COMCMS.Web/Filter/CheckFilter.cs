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

                ReJson re = new ReJson(40000, error);
                context.Result = new ContentResult()
                {
                    Content = re.ToJson(),
                    ContentType = "application/json"
                };
                //throw new Exception(error);
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

        #region 详细验证
        private ReJson ValiSignature(HttpContext context)
        {
            var Request = context.Request;
            SortedDictionary<string, string> pars = new SortedDictionary<string, string>();
            string signature = "";
            string method = Request.Method.ToUpper();
            if (method == "GET")
            {
                var queryStrings = Request.Query;
                List<string> keys = new List<string>();
                //string[] keys = HttpContext.Current.Request.QueryString.AllKeys;
                if (queryStrings == null)
                {
                    return new ReJson(40000, "参数错误！");
                }
                foreach (var item in queryStrings.Keys)
                {
                    keys.Add(item);
                }
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
            }
            else if(method == "POST")
            {
                var stream = context.Request.Form;
                foreach (var item in stream)
                {
                    if (item.Key != "signature")
                    {
                        pars.Add(item.Key, item.Value);
                    }
                    else
                    {
                        signature = item.Value;
                    }
                }
            }
            else
            {
                return new ReJson(50000, "不支持的HTTP方法！");
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
            if (postTime < DateTime.UtcNow.AddSeconds(-120))//30秒有效期
            {
                return new ReJson(40004, "signature 错误！", 1);
            }

            return null;
        }
        #endregion


    }
}
