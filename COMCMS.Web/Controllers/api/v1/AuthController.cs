using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using XCode;

namespace COMCMS.Web.Controllers.api.v1
{
    [ApiController]
    [Route("api/v1/auth")]
    [RequestSizeLimit(16 * 1024)]
    public sealed class AuthController : ControllerBase
    {
        private readonly TokenService _tokens;
        private readonly IAntiforgery _antiforgery;
        private readonly PasswordRecoveryService _passwordRecovery;
        private readonly LoginAttemptService _loginAttempts;
        private readonly ILogger<AuthController> _logger;
        private readonly SecurityEventMetrics _securityMetrics;

        public AuthController(TokenService tokens, IAntiforgery antiforgery, PasswordRecoveryService passwordRecovery,
            LoginAttemptService loginAttempts, ILogger<AuthController> logger, SecurityEventMetrics securityMetrics)
        {
            _tokens = tokens;
            _antiforgery = antiforgery;
            _passwordRecovery = passwordRecovery;
            _loginAttempts = loginAttempts;
            _logger = logger;
            _securityMetrics = securityMetrics;
        }

        [AllowAnonymous]
        [HttpGet("antiforgery")]
        public IActionResult Antiforgery()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            return Ok(new { requestToken = tokens.RequestToken });
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting("login")]
        public async Task<ActionResult<TokenResult>> Token([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrEmpty(request.Password))
                return BadRequest(Problem("用户名和密码不能为空。", 400));

            var userName = request.UserName.Trim();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (await _loginAttempts.IsBlockedAsync(userName, ipAddress))
                return StatusCode(429, Problem("登录失败次数过多，请稍后再试。", 429));

            var member = Member.Find(Member._.UserName == userName);
            if (!Member.VerifyPassword(member, request.Password))
            {
                await _loginAttempts.RecordFailureAsync(userName, ipAddress);
                _securityMetrics.AuthenticationAttempt("member", "bearer", false);
                _logger.LogWarning("Member token login failed for account hash {AccountHash} from {RemoteIp}",
                    Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(userName.ToUpperInvariant()))), ipAddress);
                return Unauthorized(Problem("用户名或密码错误。", 401));
            }

            await _loginAttempts.RecordSuccessAsync(userName);
            _securityMetrics.AuthenticationAttempt("member", "bearer", true);
            return Ok(_tokens.CreateSession(member, request.DeviceName));
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting("login")]
        public ActionResult<TokenResult> Refresh([FromBody] RefreshRequest request)
        {
            var result = _tokens.Refresh(request?.RefreshToken);
            if (result == null)
            {
                _securityMetrics.TokenEvent("refresh", "invalid");
                return Unauthorized(Problem("刷新令牌无效、已过期或已被复用。", 401));
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("password/forgot")]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting("login")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request?.Account))
                await _passwordRecovery.RequestAsync(request.Account);
            return Accepted(new { message = "如果账户存在，重置邮件将很快发送。" });
        }

        [AllowAnonymous]
        [HttpPost("password/reset")]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting("login")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.NewPassword) || request.NewPassword.Length is < 10 or > 128)
                return BadRequest(Problem("新密码长度必须为 10 到 128 个字符。", 400));
            if (!_passwordRecovery.Reset(request?.Token, request?.NewPassword))
                return BadRequest(Problem("重置令牌无效或已过期。", 400));
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
        [HttpPost("logout")]
        [IgnoreAntiforgeryToken]
        public IActionResult Logout()
        {
            var session = CurrentSession();
            Revoke(session);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
        [HttpPost("logout-all")]
        [IgnoreAntiforgeryToken]
        public IActionResult LogoutAll()
        {
            var memberId = ComCmsClaimTypes.GetSubjectId(User);
            var sessions = AuthSession.FindAll(AuthSession._.SubjectType == "member" & AuthSession._.SubjectId == memberId & AuthSession._.IsRevoked == 0);
            foreach (var session in sessions) Revoke(session);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
        [HttpGet("sessions")]
        public IActionResult Sessions(int page = 1, int pageSize = 20, bool activeOnly = true)
        {
            return SessionList(page, pageSize, activeOnly);
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer, Roles = "member")]
        [HttpDelete("sessions/{sessionId}")]
        [IgnoreAntiforgeryToken]
        public IActionResult RevokeSession(string sessionId)
        {
            var session = AuthSession.FindBySessionId(sessionId);
            if (session == null) return NotFound();
            if (!IsOwnedMemberSession(session)) return Forbid(AuthenticationSchemes.Bearer);
            Revoke(session);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.MemberCookie, Roles = "member")]
        [HttpGet("cookie/sessions")]
        public IActionResult CookieSessions(int page = 1, int pageSize = 20, bool activeOnly = true)
        {
            return SessionList(page, pageSize, activeOnly);
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.MemberCookie, Roles = "member")]
        [HttpPost("cookie/logout-all")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CookieLogoutAll()
        {
            var memberId = ComCmsClaimTypes.GetSubjectId(User);
            var sessions = AuthSession.FindAll(AuthSession._.SubjectType == "member" & AuthSession._.SubjectId == memberId & AuthSession._.IsRevoked == 0);
            foreach (var session in sessions) Revoke(session);
            await HttpContext.SignOutAsync(AuthenticationSchemes.MemberCookie);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.MemberCookie, Roles = "member")]
        [HttpDelete("cookie/sessions/{sessionId}")]
        [ValidateAntiForgeryToken]
        public IActionResult RevokeCookieSession(string sessionId)
        {
            var session = AuthSession.FindBySessionId(sessionId);
            if (session == null) return NotFound();
            if (!IsOwnedMemberSession(session)) return Forbid(AuthenticationSchemes.MemberCookie);
            Revoke(session);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("cookie")]
        [ValidateAntiForgeryToken]
        [EnableRateLimiting("login")]
        public async Task<IActionResult> CookieLogin([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrEmpty(request.Password))
                return BadRequest(Problem("用户名和密码不能为空。", 400));
            var userName = request.UserName.Trim();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (await _loginAttempts.IsBlockedAsync(userName, ipAddress))
                return StatusCode(429, Problem("登录失败次数过多，请稍后再试。", 429));

            var member = Member.Find(Member._.UserName == userName);
            if (!Member.VerifyPassword(member, request?.Password))
            {
                await _loginAttempts.RecordFailureAsync(userName, ipAddress);
                _securityMetrics.AuthenticationAttempt("member", "cookie", false);
                _logger.LogWarning("Member cookie login failed for account hash {AccountHash} from {RemoteIp}",
                    Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(userName.ToUpperInvariant()))), ipAddress);
                return Unauthorized(Problem("用户名或密码错误。", 401));
            }

            await _loginAttempts.RecordSuccessAsync(userName);
            _securityMetrics.AuthenticationAttempt("member", "cookie", true);
            var sessionResult = _tokens.CreateSession(member, request.DeviceName);
            var stamp = TokenService.CurrentStamp(member);
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Name, member.UserName),
                new Claim(ClaimTypes.Role, "member"),
                new Claim(ComCmsClaimTypes.SubjectType, "member"),
                new Claim(ComCmsClaimTypes.SecurityStamp, stamp),
                new Claim(ComCmsClaimTypes.SessionId, sessionResult.SessionId),
                new Claim("auth_time", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            }, AuthenticationSchemes.MemberCookie);
            await HttpContext.SignInAsync(AuthenticationSchemes.MemberCookie, new ClaimsPrincipal(identity));
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = AuthenticationSchemes.MemberCookie, Roles = "member")]
        [HttpPost("cookie/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CookieLogout()
        {
            Revoke(CurrentSession());
            await HttpContext.SignOutAsync(AuthenticationSchemes.MemberCookie);
            return NoContent();
        }

        private AuthSession CurrentSession() => AuthSession.FindBySessionId(User.FindFirstValue(ComCmsClaimTypes.SessionId));

        private void Revoke(AuthSession session)
        {
            if (session == null || session.IsRevoked == 1) return;
            session.IsRevoked = 1;
            session.RevokedUtc = DateTime.UtcNow;
            session.Update();
            _securityMetrics.SessionRevoked("session");
        }

        private bool IsOwnedMemberSession(AuthSession session) =>
            string.Equals(session.SubjectType, "member", StringComparison.Ordinal) &&
            session.SubjectId == ComCmsClaimTypes.GetSubjectId(User);

        private IActionResult SessionList(int page, int pageSize, bool activeOnly)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);
            var memberId = ComCmsClaimTypes.GetSubjectId(User);
            var where = AuthSession._.SubjectType == "member" & AuthSession._.SubjectId == memberId;
            if (activeOnly)
                where &= AuthSession._.IsRevoked == 0 & AuthSession._.ExpiresUtc > DateTime.UtcNow;
            var total = AuthSession.FindCount(where, null, null, 0, 0);
            var current = User.FindFirstValue(ComCmsClaimTypes.SessionId);
            var sessions = AuthSession.FindAll(where, AuthSession._.LastUsedUtc.Desc(), null, (page - 1) * pageSize, pageSize)
                .Select(item => new SessionResponse
                {
                    SessionId = item.SessionId,
                    DeviceName = item.DeviceName,
                    CreatedUtc = item.CreatedUtc,
                    LastUsedUtc = item.LastUsedUtc,
                    ExpiresUtc = item.ExpiresUtc,
                    IsRevoked = item.IsRevoked == 1,
                    IsCurrent = item.SessionId == current
                });
            return Ok(new { total, page, pageSize, list = sessions });
        }

        private ProblemDetails Problem(string title, int status) => new ProblemDetails
        {
            Title = title,
            Status = status,
            Extensions = { ["traceId"] = HttpContext.TraceIdentifier }
        };
    }

    public sealed class LoginRequest
    {
        [Required, StringLength(50, MinimumLength = 1)]
        public string UserName { get; set; }
        [Required, StringLength(128, MinimumLength = 1)]
        public string Password { get; set; }
        [StringLength(100)]
        public string DeviceName { get; set; }
    }

    public sealed class RefreshRequest
    {
        [Required, StringLength(512, MinimumLength = 32)]
        public string RefreshToken { get; set; }
    }

    public sealed class ForgotPasswordRequest
    {
        [Required, StringLength(100, MinimumLength = 1)]
        public string Account { get; set; }
    }

    public sealed class ResetPasswordRequest
    {
        [Required, StringLength(512, MinimumLength = 32)]
        public string Token { get; set; }
        [Required, StringLength(128, MinimumLength = 10)]
        public string NewPassword { get; set; }
    }

    public sealed class SessionResponse
    {
        public string SessionId { get; set; }
        public string DeviceName { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUsedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsCurrent { get; set; }
    }
}
