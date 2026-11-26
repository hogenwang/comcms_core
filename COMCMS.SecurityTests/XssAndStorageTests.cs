using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using COMCMS.Common.Security;
using COMCMS.Web.Common;
using COMCMS.Web.Controllers.api.v1;
using COMCMS.Web.Services;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class XssAndStorageTests
    {
        [Fact]
        public void FilesController_Authorization_AllowsAdminAndOwnedMemberPathsOnly()
        {
            var admin = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ComCmsClaimTypes.SubjectType, "admin")
            }, COMCMS.Common.Security.AuthenticationSchemes.AdminCookie));

            var member42 = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "42"),
                new Claim(ComCmsClaimTypes.SubjectType, "member")
            }, COMCMS.Common.Security.AuthenticationSchemes.Bearer));

            Assert.True(FilesController.IsAuthorizedForStorageKey(admin, "2026/01/invoice.pdf"));
            Assert.True(FilesController.IsAuthorizedForStorageKey(admin, "members/42/doc.pdf"));

            Assert.True(FilesController.IsAuthorizedForStorageKey(member42, "members/42/doc.pdf"));
            Assert.False(FilesController.IsAuthorizedForStorageKey(member42, "members/43/doc.pdf"));
            Assert.False(FilesController.IsAuthorizedForStorageKey(member42, "members/420/doc.pdf"));
            Assert.False(FilesController.IsAuthorizedForStorageKey(member42, "2026/01/invoice.pdf"));
            Assert.False(FilesController.IsAuthorizedForStorageKey(member42, "members/42/../43/doc.pdf"));
        }

        [Fact]
        public void FilesController_DownloadNameSanitizer_StripsTraversalAndControlChars()
        {
            var sanitized = FilesController.SanitizeDownloadName("../../evil\r\nheader.pdf", "fallback.bin");

            Assert.DoesNotContain("/", sanitized);
            Assert.DoesNotContain("\\", sanitized);
            Assert.DoesNotContain("\r", sanitized);
            Assert.DoesNotContain("\n", sanitized);
            Assert.DoesNotContain("..", sanitized);
        }

        [Fact]
        public void SafeScriptJson_EscapesHtmlAndScriptClosingTags()
        {
            var content = SafeScriptJson.Serialize(new
            {
                title = "</script><script>alert('xss')</script>",
                amp = "a & b < c > d"
            });

            using var writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            var json = writer.ToString();

            Assert.DoesNotContain("</script>", json, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("<script>", json, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("\\u003C", json, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("https://cdn.example.com/a.png", true)]
        [InlineData("http://cdn.example.com/a.png", true)]
        [InlineData("https://cdn.example.com:8443/a.png", false)]
        [InlineData("http://cdn.example.com:6379/a.png", false)]
        [InlineData("file:///etc/passwd", false)]
        [InlineData("ftp://cdn.example.com/a.png", false)]
        public void RemoteImageService_PermittedUri_AllowsOnlyHttpHttpsStandardPorts(string rawUrl, bool expected)
        {
            Assert.Equal(expected, RemoteImageService.IsPermittedRemoteUri(new Uri(rawUrl)));
        }

        [Theory]
        [InlineData("2002:7f00:1::1")]
        [InlineData("2001:0000::1")]
        [InlineData("2001:db8::1")]
        [InlineData("64:ff9b::127.0.0.1")]
        [InlineData("100.64.0.1")]
        [InlineData("198.18.0.1")]
        [InlineData("203.0.113.5")]
        public void RemoteImageService_TransitionAndSpecialAddresses_AreRejected(string ip)
        {
            Assert.False(RemoteImageService.IsPublicAddress(IPAddress.Parse(ip)));
        }
    }
}
