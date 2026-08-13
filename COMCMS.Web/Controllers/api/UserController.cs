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
using COMCMS.Common.Security;

namespace COMCMS.Web.Controllers.api
{
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
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

    }
}
