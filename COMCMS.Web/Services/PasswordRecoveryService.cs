using System;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Common.Security;
using COMCMS.Core;
using COMCMS.Web.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using NewLife.Log;
using XCode;

namespace COMCMS.Web.Services
{
    public sealed class PasswordRecoveryService
    {
        private const string Purpose = "member-password-reset";
        private readonly AuthenticationSettings _settings;
        private readonly IWebHostEnvironment _environment;

        public PasswordRecoveryService(IOptions<AuthenticationSettings> settings, IWebHostEnvironment environment)
        {
            _settings = settings.Value;
            _environment = environment;
        }

        public async Task RequestAsync(string account)
        {
            if (!Uri.TryCreate(_settings.PasswordResetUrl, UriKind.Absolute, out var resetUri) ||
                (resetUri.Scheme != Uri.UriSchemeHttps && !_environment.IsDevelopment())) return;
            var member = FindMember(account);
            if (member == null || member.IsLock == 1 || string.IsNullOrWhiteSpace(member.Email)) return;

            var now = DateTime.UtcNow;
            var recent = AuthOneTimeToken.FindAll(
                AuthOneTimeToken._.SubjectType == "member" &
                AuthOneTimeToken._.SubjectId == member.Id &
                AuthOneTimeToken._.Purpose == Purpose &
                AuthOneTimeToken._.IsUsed == 0)
                .Any(item => item.CreatedUtc > now.AddMinutes(-2));
            if (recent) return;

            InvalidateOutstandingTokens(member.Id, now);
            var rawToken = OneTimeTokenService.CreateToken();
            new AuthOneTimeToken
            {
                TokenHash = OneTimeTokenService.HashToken(rawToken),
                SubjectType = "member",
                SubjectId = member.Id,
                Purpose = Purpose,
                CreatedUtc = now,
                ExpiresUtc = now.AddMinutes(15),
                UsedUtc = DateTime.UnixEpoch,
                IsUsed = 0
            }.Insert();

            var resetBaseUrl = resetUri.ToString();
            var separator = resetBaseUrl.Contains('?') ? "&" : "?";
            var resetUrl = $"{resetBaseUrl}{separator}token={Uri.EscapeDataString(rawToken)}";
            await SendAsync(member.Email, resetUrl);
        }

        public bool Reset(string rawToken, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(rawToken) || string.IsNullOrWhiteSpace(newPassword) || newPassword.Length is < 10 or > 128) return false;
            var hash = OneTimeTokenService.HashToken(rawToken);
            using var transaction = new EntityTransaction<AuthOneTimeToken>();
            var token = AuthOneTimeToken.Find(AuthOneTimeToken._.TokenHash == hash & AuthOneTimeToken._.Purpose == Purpose);
            if (token == null || token.IsUsed == 1 || token.ExpiresUtc <= DateTime.UtcNow) return false;

            var now = DateTime.UtcNow;
            var claimed = AuthOneTimeToken.Update(
                new[] { AuthOneTimeToken.__.IsUsed, AuthOneTimeToken.__.UsedUtc },
                new object[] { 1, now },
                new[] { AuthOneTimeToken.__.Id, AuthOneTimeToken.__.IsUsed },
                new object[] { token.Id, 0 });
            if (claimed != 1) return false;

            var member = Member.FindById(token.SubjectId);
            if (member == null || member.IsLock == 1) return false;
            member.PassWord = PasswordHashService.HashPassword(newPassword);
            member.Update();
            InvalidateOutstandingTokens(member.Id, now);
            RevokeAllSessions(member.Id, now);
            transaction.Commit();
            return true;
        }

        private static Member FindMember(string account)
        {
            if (string.IsNullOrWhiteSpace(account)) return null;
            var value = account.Trim();
            return Member.Find(Member._.UserName == value) ?? Member.Find(Member._.Email == value);
        }

        private static void RevokeAllSessions(int memberId, DateTime now)
        {
            var sessions = AuthSession.FindAll(AuthSession._.SubjectType == "member" & AuthSession._.SubjectId == memberId & AuthSession._.IsRevoked == 0);
            foreach (var session in sessions)
            {
                session.IsRevoked = 1;
                session.RevokedUtc = now;
                session.Update();
            }
        }

        private static void InvalidateOutstandingTokens(int memberId, DateTime now)
        {
            var tokens = AuthOneTimeToken.FindAll(
                AuthOneTimeToken._.SubjectType == "member" &
                AuthOneTimeToken._.SubjectId == memberId &
                AuthOneTimeToken._.Purpose == Purpose &
                AuthOneTimeToken._.IsUsed == 0);
            foreach (var token in tokens)
            {
                token.IsUsed = 1;
                token.UsedUtc = now;
                token.Update();
            }
        }

        private static async Task SendAsync(string email, string resetUrl)
        {
            try
            {
                var smtp = Config.GetSystemConfig().SMTPConfigEntity;
                if (string.IsNullOrWhiteSpace(smtp.SmtpHost) || string.IsNullOrWhiteSpace(smtp.SmtpEmail) ||
                    string.IsNullOrWhiteSpace(smtp.SmtpEmailPwd) || !int.TryParse(smtp.SmtpProt, out var port))
                {
                    XTrace.WriteLine("Password reset email was not sent because SMTP is not configured.");
                    return;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtp.PostUserName, smtp.SmtpEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "重置账户密码";
                message.Body = new TextPart("plain")
                {
                    Text = $"请在 15 分钟内打开以下链接重置密码：\r\n{resetUrl}\r\n若非本人操作，请忽略此邮件。"
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(smtp.SmtpHost, port, smtp.IsSSL == 1);
                await client.AuthenticateAsync(smtp.SmtpEmail, smtp.SmtpEmailPwd);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
        }
    }
}
