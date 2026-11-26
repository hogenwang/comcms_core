using System;
using System.IO;
using System.Text.Json;
using COMCMS.Core;
using COMCMS.Web.Areas.AdminCP.Controllers;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class AdminRbacSecurityTests
    {
        [Theory]
        [InlineData(true, true, false, true)]
        [InlineData(true, false, true, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, true)]
        public void MemberController_SuperAdminRoleGuards_BlockPrivilegeEscalation(
            bool callerIsSuperAdmin, bool targetIsSuperAdmin, bool existingIsSuperAdmin, bool expected)
        {
            Assert.Equal(expected, MemberController.CanManageSuperAdminRole(callerIsSuperAdmin, targetIsSuperAdmin, existingIsSuperAdmin));
            Assert.Equal(expected, MemberController.CanAssignOrModifyAdmin(callerIsSuperAdmin, targetIsSuperAdmin, existingIsSuperAdmin));
        }

        [Fact]
        public void MemberController_SafeDtos_StripPasswordSaltAndSensitiveFields()
        {
            var admin = new Admin
            {
                Id = 7,
                UserName = "editor",
                RealName = "Editor",
                PassWord = "PBKDF2-SECRET-HASH",
                Salt = "RANDOMSALT"
            };
            var member = new Member
            {
                Id = 9,
                UserName = "member1",
                PassWord = "PBKDF2-MEMBER-HASH",
                Salt = "MEMBERSALT",
                WeixinOpenId = "wx_secret_openid",
                WeixinAppOpenId = "wx_app_secret",
                Alipay = "secret_alipay"
            };

            var adminJson = JsonSerializer.Serialize(MemberController.ToSafeAdminDto(admin));
            var memberJson = JsonSerializer.Serialize(MemberController.ToSafeMemberDto(member));

            Assert.DoesNotContain("PBKDF2-SECRET-HASH", adminJson);
            Assert.DoesNotContain("RANDOMSALT", adminJson);
            Assert.DoesNotContain("PassWord", adminJson, StringComparison.OrdinalIgnoreCase);

            Assert.DoesNotContain("PBKDF2-MEMBER-HASH", memberJson);
            Assert.DoesNotContain("MEMBERSALT", memberJson);
            Assert.DoesNotContain("wx_secret_openid", memberJson);
            Assert.DoesNotContain("wx_app_secret", memberJson);
            Assert.DoesNotContain("secret_alipay", memberJson);
        }

        [Theory]
        [InlineData("userfiles", true)]
        [InlineData("attach_2026-01", true)]
        [InlineData("../wwwroot", false)]
        [InlineData("a/b", false)]
        [InlineData("a\\b", false)]
        [InlineData("C:\\temp", false)]
        [InlineData("", false)]
        public void SystemController_AttachPatchValidator_BlocksPathTraversal(string value, bool expected)
        {
            Assert.Equal(expected, SystemController.IsSafeAttachPatch(value));
        }

        [Theory]
        [InlineData("photo.jpg", "renamed.jpg", true)]
        [InlineData("photo.png", "renamed.PNG", true)]
        [InlineData("manual.pdf", "manual_v2.pdf", true)]
        [InlineData("clip.mp4", "clip_v2.mp4", true)]
        [InlineData("manual.pdf", "manual.html", false)]
        [InlineData("photo.jpg", "renamed.html", false)]
        [InlineData("photo.jpg", "renamed.svg", false)]
        [InlineData("photo.jpg", "web.config", false)]
        [InlineData("photo.jpg", ".htaccess", false)]
        [InlineData("photo.jpg", "../escape.jpg", false)]
        [InlineData("doc.txt", "renamed.txt", false)]
        public void AttachController_RenameValidator_EnforcesStaticImageAllowlistAndSameExtension(
            string originalFile, string newName, bool expected)
        {
            var fullPath = Path.Combine(Path.GetTempPath(), originalFile);
            Assert.Equal(expected, AttachController.IsSafeRenameTarget(fullPath, newName));
        }

        [Theory]
        [InlineData("../../evil.png")]
        [InlineData("..\\..\\web.config.jpg")]
        [InlineData("weird<>name?.webp")]
        public void UploadController_SafeImageFileName_StripsTraversalAndSpecialChars(string originalName)
        {
            var fileName = UploadController.BuildSafeImageFileName(originalName, randomMode: 0, targetDirectory: Path.GetTempPath());

            Assert.DoesNotContain("/", fileName);
            Assert.DoesNotContain("\\", fileName);
            Assert.DoesNotContain("..", fileName);
            Assert.False(Path.IsPathRooted(fileName));
        }

        [Theory]
        [InlineData("2026_11_24.log", true)]
        [InlineData("app-error.txt", true)]
        [InlineData("../appsettings.json", false)]
        [InlineData("..\\appsettings.json", false)]
        [InlineData("sub/2026.log", false)]
        [InlineData("2026.json", false)]
        [InlineData("", false)]
        public void PermissionController_LogPathResolver_RestrictsToLogDirectoryAndExtensions(string logName, bool expected)
        {
            var root = Path.Combine(Path.GetTempPath(), "comcms-log-test");
            var allowed = PermissionController.TryResolveLogFile(root, logName, out var fullPath);

            Assert.Equal(expected, allowed);
            if (allowed)
            {
                Assert.StartsWith(Path.GetFullPath(Path.Combine(root, "Log")), fullPath, StringComparison.OrdinalIgnoreCase);
            }
        }

        [Fact]
        public void ArticleAndProductControllers_EnforceCategoryAndSelfOnlyBoundaries()
        {
            var restrictedRole = new AdminRoles
            {
                Id = 2,
                IsSuperAdmin = 0,
                AuthorizedArticleCagegory = "[10, 20]",
                AuthorizedCagegory = "[30, 40]",
                OnlyEditMyselfArticle = 1,
                OnlyEditMyselfProduct = 1
            };
            var editor = new Admin { Id = 5, RoleId = 2 };
            editor["Roles"] = restrictedRole;

            Assert.True(ArticleController.CanAccessArticle(editor, 10, 5, out _));
            Assert.False(ArticleController.CanAccessArticle(editor, 99, 5, out _));
            Assert.False(ArticleController.CanAccessArticle(editor, 10, 6, out _));

            Assert.True(ProductController.CanAccessProduct(editor, 30, 5, out _));
            Assert.False(ProductController.CanAccessProduct(editor, 99, 5, out _));
            Assert.False(ProductController.CanAccessProduct(editor, 30, 6, out _));

            var superRole = new AdminRoles { Id = 1, IsSuperAdmin = 1 };
            var superAdmin = new Admin { Id = 1, RoleId = 1 };
            superAdmin["Roles"] = superRole;
            Assert.True(ArticleController.CanAccessArticle(superAdmin, 99, 6, out _));
            Assert.True(ProductController.CanAccessProduct(superAdmin, 99, 6, out _));
        }

        [Theory]
        [InlineData("/news/1.html", true)]
        [InlineData("https://example.com/ad", true)]
        [InlineData("http://example.com/ad", true)]
        [InlineData("javascript:alert(1)", false)]
        [InlineData("data:text/html,<script>alert(1)</script>", false)]
        [InlineData("vbscript:msgbox(1)", false)]
        [InlineData("//evil.example/ad", false)]
        public void OtherController_UrlValidator_BlocksDangerousSchemes(string url, bool expected)
        {
            Assert.Equal(expected, OtherController.IsSafeHttpOrRelativeUrl(url));
        }
    }
}
