using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common;
using Microsoft.AspNetCore.Mvc;
using NewLife.Caching;
using NewLife.Log;
namespace COMCMS.Web.Controllers
{
    public class VerifyCodeController : Controller
    {
        /// <summary>
                /// 数字验证码
                /// </summary>
                /// <returns></returns>
        public FileContentResult NumberVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            //写入Session
            SessionHelper.WriteSession(VerifyCodeHelper.GetSingleObj().SESSION_KEY, code);
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().GetVerifyCode(code, 120, 32);
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
                /// 字母验证码
                /// </summary>
                /// <returns></returns>
        public FileContentResult CharVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.AbcVerifyCode);
            //写入Session
            SessionHelper.WriteSession(VerifyCodeHelper.GetSingleObj().SESSION_KEY, code);
            var bitmap = VerifyCodeHelper.GetSingleObj().GetVerifyCode(code, 120, 32);
            //MemoryStream stream = new MemoryStream();
            //bitmap.Save(stream, ImageFormat.Png);
            return File(bitmap, "image/png");
        }

        /// <summary>
                /// 混合验证码
                /// </summary>
                /// <returns></returns>
        public FileContentResult MixVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            SessionHelper.WriteSession(VerifyCodeHelper.GetSingleObj().SESSION_KEY, code);
            var bitmap = VerifyCodeHelper.GetSingleObj().GetVerifyCode(code, 120, 32);
            //MemoryStream stream = new MemoryStream();
            //bitmap.Save(stream, ImageFormat.Gif);
            return File(bitmap, "image/gif");
        }
    }
}