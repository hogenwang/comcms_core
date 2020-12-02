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

        #region 用户授权信息同步
        /// <summary>
        /// 同步微信信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ReJson SyncWeixinInfo(string sessionkey, string userInfo, int inviteid = 0)
        {
            if (string.IsNullOrEmpty(sessionkey))
            {
                return new ReJson(5003,"登录状态失败");
            }
            WXAppSession entity = WXAppSession.Find(WXAppSession._.Key == sessionkey);
            if (entity == null)
            {
                return new ReJson("未找到登录状态！");
            }
            if (string.IsNullOrEmpty(userInfo))
            {
                return new ReJson("用户信息错误！");
            }
            WXAppUserInfo info = JsonConvert.DeserializeObject<WXAppUserInfo>(userInfo);
            if (string.IsNullOrEmpty(info.avatarUrl))
            {
                return new ReJson("微信用户信息错误！");
            }
            //获取用户
            Member my = Member.FindById(entity.UId);
            if (my == null)
            {
                return new ReJson(40000, "系统找不到本用户！");
            }
            //同步用户信息
            my.Country = info.country;
            my.UserImg = info.avatarUrl;
            my.City = info.city;
            my.Province = info.province;
            my.Sex = info.gender;
            my.Nickname = info.nickName;
            my.LastLoginTime = DateTime.Now;
            if (inviteid > 0 & my.Parent == 0)
            {
                Member inviter = Member.Find(Member._.Id == inviteid);
                if (inviter != null)
                {
                    my.Parent = inviter.Id;
                    my.Grandfather = inviter.Parent;
                }
            }
            my.Update();

            var detail = new { sessionkey = entity.Key, username = info.nickName, userimg = info.avatarUrl };
            return new ReJson(0, "同步信息成功！", detail);
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