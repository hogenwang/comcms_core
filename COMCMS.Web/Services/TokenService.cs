using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using XCode;

namespace COMCMS.Web.Services
{
    public sealed class TokenService
    {
        private readonly JwtKeyProvider _keyProvider;
        private readonly AuthenticationSettings _settings;
        private readonly ILogger<TokenService> _logger;
        private readonly SecurityEventMetrics _securityMetrics;

        public TokenService(JwtKeyProvider keyProvider, IOptions<AuthenticationSettings> settings,
            ILogger<TokenService> logger, SecurityEventMetrics securityMetrics)
        {
            _keyProvider = keyProvider;
            _settings = settings.Value;
            _logger = logger;
            _securityMetrics = securityMetrics;
        }

        public TokenResult CreateSession(Member member, string deviceName)
        {
            var now = DateTime.UtcNow;
            var refreshToken = CreateRefreshToken();
            var stamp = CurrentStamp(member);
            var session = new AuthSession
            {
                SessionId = Guid.NewGuid().ToString(),
                SubjectType = "member",
                SubjectId = member.Id,
                TokenFamily = Guid.NewGuid().ToString(),
                RefreshTokenHash = HashRefreshToken(refreshToken),
                SecurityStamp = stamp,
                DeviceName = string.IsNullOrWhiteSpace(deviceName) ? "unknown" : deviceName.Trim()[..Math.Min(deviceName.Trim().Length, 100)],
                CreatedUtc = now,
                LastUsedUtc = now,
                ExpiresUtc = now.AddDays(_settings.RefreshTokenDays),
                RevokedUtc = DateTime.UnixEpoch,
                IsRevoked = 0
            };
            session.Insert();
            _securityMetrics.TokenEvent("issue", "success");
            return CreateResult(member, session, refreshToken);
        }

        public TokenResult Refresh(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return null;
            var hash = HashRefreshToken(refreshToken);
            using var transaction = new EntityTransaction<AuthSession>();
            var session = AuthSession.FindByRefreshHash(hash);
            if (session == null) return null;

            if (!string.Equals(session.RefreshTokenHash, hash, StringComparison.Ordinal) || !AuthSession.IsActive(session))
            {
                _logger.LogWarning("Refresh token reuse or inactive session detected for session {SessionId}, family {TokenFamily}; revoking family.", session.SessionId, session.TokenFamily);
                AuthSession.RevokeFamily(session.TokenFamily);
                transaction.Commit();
                _securityMetrics.TokenEvent("refresh", "reuse");
                _securityMetrics.SessionRevoked("family");
                return null;
            }

            var member = Member.FindById(session.SubjectId);
            var currentStamp = member == null ? null : CurrentStamp(member);
            if (!string.Equals(session.SubjectType, "member", StringComparison.Ordinal) ||
                member == null || member.IsLock == 1 || !SecurityStampService.Equals(session.SecurityStamp, currentStamp))
            {
                _logger.LogWarning("Refresh rejected because the member session {SessionId} is invalidated by account state; revoking family.", session.SessionId);
                AuthSession.RevokeFamily(session.TokenFamily);
                transaction.Commit();
                _securityMetrics.TokenEvent("refresh", "account-invalid");
                _securityMetrics.SessionRevoked("family");
                return null;
            }

            var replacementToken = CreateRefreshToken();
            var now = DateTime.UtcNow;
            var replacementSessionId = Guid.NewGuid().ToString();
            var claimed = AuthSession.Update(
                new[] { AuthSession.__.IsRevoked, AuthSession.__.RevokedUtc, AuthSession.__.LastUsedUtc, AuthSession.__.ReplacedBySessionId },
                new object[] { 1, now, now, replacementSessionId },
                new[] { AuthSession.__.Id, AuthSession.__.IsRevoked },
                new object[] { session.Id, 0 });
            if (claimed != 1)
            {
                _logger.LogWarning("Concurrent refresh detected for session {SessionId}, family {TokenFamily}; revoking family.", session.SessionId, session.TokenFamily);
                AuthSession.RevokeFamily(session.TokenFamily);
                transaction.Commit();
                _securityMetrics.TokenEvent("refresh", "concurrent-reuse");
                _securityMetrics.SessionRevoked("family");
                return null;
            }

            var replacementSession = new AuthSession
            {
                SessionId = replacementSessionId,
                SubjectType = session.SubjectType,
                SubjectId = session.SubjectId,
                TokenFamily = session.TokenFamily,
                RefreshTokenHash = HashRefreshToken(replacementToken),
                SecurityStamp = session.SecurityStamp,
                DeviceName = session.DeviceName,
                CreatedUtc = now,
                LastUsedUtc = now,
                ExpiresUtc = session.ExpiresUtc,
                RevokedUtc = DateTime.UnixEpoch,
                IsRevoked = 0
            };
            replacementSession.Insert();
            transaction.Commit();
            _securityMetrics.TokenEvent("refresh", "success");
            return CreateResult(member, replacementSession, replacementToken);
        }

        public bool ValidateActiveSession(ClaimsPrincipal principal)
        {
            var subjectId = ComCmsClaimTypes.GetSubjectId(principal);
            var session = AuthSession.FindBySessionId(principal.FindFirstValue(ComCmsClaimTypes.SessionId));
            if (!AuthSession.IsActive(session) ||
                !string.Equals(session.SubjectType, "member", StringComparison.Ordinal) ||
                session.SubjectId != subjectId ||
                !string.Equals(principal.FindFirstValue(ComCmsClaimTypes.SubjectType), "member", StringComparison.Ordinal))
                return false;
            var member = Member.FindById(session.SubjectId);
            return member != null && member.IsLock != 1 &&
                   SecurityStampService.Equals(session.SecurityStamp, CurrentStamp(member)) &&
                   SecurityStampService.Equals(session.SecurityStamp, principal.FindFirstValue(ComCmsClaimTypes.SecurityStamp));
        }

        public static string CurrentStamp(Member member) =>
            SecurityStampService.Compute("member", member.Id, member.PassWord, member.RoleId, member.IsLock);

        private TokenResult CreateResult(Member member, AuthSession session, string refreshToken)
        {
            var expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Name, member.UserName),
                new Claim(ClaimTypes.Role, "member"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ComCmsClaimTypes.SessionId, session.SessionId),
                new Claim(ComCmsClaimTypes.SubjectType, "member"),
                new Claim(ComCmsClaimTypes.SecurityStamp, session.SecurityStamp)
            };
            var token = new JwtSecurityToken(
                _settings.Issuer,
                _settings.Audience,
                claims,
                DateTime.UtcNow,
                expires,
                _keyProvider.CreateSigningCredentials());

            return new TokenResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                TokenType = "Bearer",
                ExpiresIn = (int)TimeSpan.FromMinutes(_settings.AccessTokenMinutes).TotalSeconds,
                SessionId = session.SessionId
            };
        }

        private static string CreateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        private static string HashRefreshToken(string value) => Convert.ToHexString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(value)));
    }

    public sealed class TokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string SessionId { get; set; }
    }
}
