using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using COMCMS.Common;
using System.Text;
using System.IO;

namespace COMCMS.Core
{
    /// <summary>
    /// 系统JSON提示实体类
    /// </summary>
    [Serializable]
    public class JsonTip
    {
        public JsonTip() { }
        /// <summary>
        /// 成功状态
        /// </summary>
        public static readonly string SUCCESS = "success";
        /// <summary>
        /// 失败状态
        /// </summary>
        public static readonly string ERROR = "error";
        /// <summary>
        /// 请求返回状态 默认 error（错误）;成功：success 
        /// </summary>
        public string Status { get; set; } = ERROR;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// Id 可选
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// 返回URL
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 其他详情
        /// </summary>
        public object Detail { get; set; }

        /// <summary>
        /// 获取JSON字符串
        /// </summary>
        /// <param name="json">JsonTip实体类</param>
        /// <returns></returns>
        public static string GetJsonString(JsonTip json)
        {
            if (json != null)
                return JsonConvert.SerializeObject(json);
            else
                return string.Empty;
        }
    }

    //public static class Extension
    //{
    //    public static void EchoTip(this JsonTip json)
    //    {
    //        string strJson = JsonTip.GetJsonString(json);
            
    //        Byte[] bytes = Encoding.UTF8.GetBytes(strJson);
    //        Stream stream = new MemoryStream(bytes);
    //        MyHttpContext.Current.Response.ContentType = "text/plain";
    //        MyHttpContext.Current.Response.Body = stream;
    //    }
    //}
}
