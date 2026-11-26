using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web;
using COMCMS.Web.Controllers;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class PublicEndpointSecurityTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PublicEndpointSecurityTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });
        }

        [Theory]
        [InlineData("/Home/AlertAndRedirect?str=x&url=https://evil.example")]
        [InlineData("/Home/AlertAndGoBack?str=x")]
        [InlineData("/Home/EchoTip?Message=x")]
        [InlineData("/Article/AlertAndRedirect?str=x&url=https://evil.example")]
        [InlineData("/Product/AlertAndGoBack?str=x")]
        public async Task BaseControllerHelperMethods_AreNotRoutableActions(string path)
        {
            var response = await _client.GetAsync(path);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task StatusCodeEndpoint_DirectRequest_DoesNotThrow500()
        {
            var response = await _client.GetAsync("/StatusCode/404");

            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task SwfAssets_AreBlockedWith404()
        {
            var response = await _client.GetAsync("/lib/webuploader/Uploader.swf");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AdminLoginPage_RendersScopedTechThemeWithoutLegacyLoginCss()
        {
            var response = await _client.GetAsync("/AdminCP/Login");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("admin-login-page", html, StringComparison.Ordinal);
            Assert.Contains("login-scoped.css", html, StringComparison.Ordinal);
            Assert.DoesNotContain("/hplus/css/login.css", html, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task AdminLogout_RequiresAdminCookieSchemeSoAntiforgeryMatchesAuthenticatedPrincipal()
        {
            using var content = new FormUrlEncodedContent(Array.Empty<KeyValuePair<string, string>>());
            var response = await _client.PostAsync("/AdminCP/Login/Logout", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("https://localhost/AdminCP/Login", response.Headers.Location?.ToString());
        }

        [Theory]
        [InlineData("/AdminCP/Upload/UploadImage")]
        [InlineData("/AdminCP/Upload/UploadFile")]
        [InlineData("/AdminCP/Upload/UploadVideo")]
        [InlineData("/AdminCP/Upload/JoditUpload")]
        [InlineData("/AdminCP/Attach/SaveEditedImage")]
        public async Task UploadEndpoints_WithoutAdminAuth_AreRejected(string path)
        {
            using var content = new MultipartFormDataContent();
            var response = await _client.PostAsync(path, content);

            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory]
        [InlineData("/AdminCP/Attach/Index")]
        [InlineData("/AdminCP/Attach/AttachList")]
        [InlineData("/AdminCP/Attach/PdfViewer?path=test.pdf")]
        [InlineData("/AdminCP/Attach/ImageEditor?path=test.jpg")]
        public async Task AttachPages_WithoutAdminAuth_RedirectToLogin(string path)
        {
            var response = await _client.GetAsync(path);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("https://localhost/AdminCP/Login", response.Headers.Location?.ToString());
        }

        [Fact]
        public void HomeController_InstallAndDoInstall_ReturnNotFoundOutsideDevelopment()
        {
            var prodEnv = new StubWebHostEnvironment("Production");
            var hitCounter = new HitCounterService(NullLogger<HitCounterService>.Instance);
            var controller = new HomeController(prodEnv, new PublicContentService(hitCounter));

            Assert.IsType<NotFoundResult>(controller.Install());
            Assert.IsType<NotFoundResult>(controller.DoInstall());
        }

        [Fact]
        public void WeixinTimestampGuard_RejectsStaleMissingAndFutureTimestamps()
        {
            var nowUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Assert.True(WeixinController.IsTimestampFresh(nowUnix.ToString()));
            Assert.False(WeixinController.IsTimestampFresh((nowUnix - 600).ToString()));
            Assert.False(WeixinController.IsTimestampFresh((nowUnix + 600).ToString()));
            Assert.False(WeixinController.IsTimestampFresh(""));
            Assert.False(WeixinController.IsTimestampFresh("not-a-number"));
        }

        [Fact]
        public void PaginationHelper_EncodesXssAndHandlesFormatBracesAndZeroPageSize()
        {
            var html = Pages.BuildPageHtml(
                pageCount: 5,
                page: 2,
                url: "/Article/Index",
                isRewrite: 0,
                maxShowPage: 5,
                filepath: "",
                rawQueryString: "?q={99}&x=\"><script>alert(1)</script>",
                rawId: "1");

            Assert.DoesNotContain("<script>", html, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("javascritp:", html, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("page=3", html, StringComparison.Ordinal);
        }

        [Fact]
        public void UtilsGetServerUrl_PrefersConfiguredPublicBaseUrl()
        {
            var originalConfig = Utils.Configuration;
            try
            {
                var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["Security:PublicBaseUrl"] = "https://cms.example.com/subpath/"
                    })
                    .Build();
                Utils.AddUtils(config);

                Assert.Equal("https://cms.example.com", Utils.GetServerUrl());
            }
            finally
            {
                Utils.AddUtils(originalConfig);
            }
        }

        [Theory]
        [InlineData("Index.cshtml", "Index.cshtml")]
        [InlineData("Custom_List-2.cshtml", "Custom_List-2.cshtml")]
        [InlineData("../Shared/_Layout.cshtml", "Index.cshtml")]
        [InlineData("..\\..\\secret.cshtml", "Index.cshtml")]
        [InlineData("evil.html", "Index.cshtml")]
        [InlineData("", "Index.cshtml")]
        public void PublicContentService_TemplateResolver_RejectsPathTraversal(string candidate, string expected)
        {
            Assert.Equal(expected, PublicContentService.ResolveSafeTemplateName(candidate, "Index.cshtml"));
        }

        [Fact]
        public void PublicContentService_IpAllowlist_HandlesCrLfAndRanges()
        {
            const string allowList = "10.0.0.1\r\n192.168.1.10-192.168.1.20\r\n";

            Assert.True(PublicContentService.IsIpAllowed(allowList, "10.0.0.1"));
            Assert.True(PublicContentService.IsIpAllowed(allowList, "192.168.1.15"));
            Assert.False(PublicContentService.IsIpAllowed(allowList, "192.168.1.25"));
            Assert.False(PublicContentService.IsIpAllowed(allowList, ""));
        }

        [Theory]
        [InlineData("/news/1.html", true)]
        [InlineData("https://example.com/path", true)]
        [InlineData("javascript:alert(1)", false)]
        [InlineData("//evil.example/path", false)]
        [InlineData("/\\evil.example", false)]
        public void PublicContentService_RedirectValidator_RejectsUnsafeSchemes(string url, bool expected)
        {
            Assert.Equal(expected, PublicContentService.IsSafeRedirectUrl(url));
        }

        private sealed class StubWebHostEnvironment : IWebHostEnvironment
        {
            public StubWebHostEnvironment(string environmentName)
            {
                EnvironmentName = environmentName;
            }

            public string EnvironmentName { get; set; }
            public string ApplicationName { get; set; } = "COMCMS.Web";
            public string WebRootPath { get; set; } = AppContext.BaseDirectory;
            public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
            public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        }
    }
}
