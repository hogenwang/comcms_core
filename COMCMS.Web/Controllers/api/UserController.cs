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
using COMCMS.Web.Filter;
using NewLife.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.WxOpen;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace COMCMS.Web.Controllers.api
{
    [Authorize(Roles ="user")]
    public class UserController : APIBaseController
    {
        private readonly SystemSetting _attachsetting;
        private IWebHostEnvironment _env;
        private AttachConfigEntity attach;
        private SystemSetting _appSettings;
        public UserController(IWebHostEnvironment env, IOptions<SystemSetting> attachsetting, IOptions<SystemSetting> setting)
        {
            attach = Core.Config.GetSystemConfig().AttachConfigEntity;
            _env = env;
            _attachsetting = attachsetting.Value;
            _appSettings = setting.Value;
        }

        #region 判断授权信息
        [HttpGet]
        public object CheckIdentity()
        {
            var user = User;
            //string name = user.Identity.Name;
            if (user == null || string.IsNullOrEmpty(user.Identity.Name))
            {
                reJson.code = 401;//401 为授权错误，需要重新登录
                reJson.message = "授权失败或者过期";
                return reJson;
            }
            reJson.code = 0;
            reJson.message = "授权失败或者过期";
            return reJson;
        }
        #endregion

        #region jwt登录演示
        [HttpGet]
        [AllowAnonymous]
        public object TestLogin(string username,string password)
        {
            //演示jwt登录，不判断逻辑，请自行判断
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtkey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
{
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role,"user")
}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string wtoken = tokenHandler.WriteToken(token);

            reJson.code = 0;
            reJson.detail = new
            {
                token = wtoken
            };
            reJson.message = "登录成功！";
            return reJson;
        }

        [HttpGet]
        public object TestGetMyInfo()
        {
            var user = User;

            reJson.code = 0;
            reJson.detail = new
            {
                username = user.Identity.Name
            };
            reJson.message = "登录成功！";
            return reJson;
        }
        #endregion




    }
}