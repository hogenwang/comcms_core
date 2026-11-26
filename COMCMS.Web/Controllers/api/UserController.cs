using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using COMCMS.Common.Security;

namespace COMCMS.Web.Controllers.api
{
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
    public class UserController : APIBaseController
    {
        #region 判断授权信息
        [HttpGet]
        public object CheckIdentity()
        {
            var user = User;
            if (user == null || string.IsNullOrEmpty(user.Identity?.Name))
            {
                reJson.code = 401;
                reJson.message = "授权失败或者过期";
                return reJson;
            }
            reJson.code = 0;
            reJson.message = "授权有效";
            return reJson;
        }
        #endregion
    }
}
