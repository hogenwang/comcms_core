using System.Security.Claims;

namespace COMCMS.Common.Security
{
    public static class AuthenticationSchemes
    {
        public const string AdminCookie = "AdminCookie";
        public const string MemberCookie = "MemberCookie";
        public const string Bearer = "Bearer";
    }

    public static class ComCmsClaimTypes
    {
        public const string SubjectType = "subject_type";
        public const string SecurityStamp = "security_stamp";
        public const string SessionId = "sid";
        public const string AdminRoleId = "admin_role_id";
        public const string LoginLogId = "login_log_id";

        public static int GetSubjectId(ClaimsPrincipal principal)
        {
            return int.TryParse(principal?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
        }
    }
}
