using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using COMCMS.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class SecurityPipelineTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SecurityPipelineTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });
        }

        [Fact]
        public async Task AdminPage_WithoutCookie_RedirectsToLogin()
        {
            var response = await _client.GetAsync("/AdminCP/Index/Main");

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("https://localhost/AdminCP/Login", response.Headers.Location?.ToString());
        }

        [Fact]
        public async Task Payment_WithoutBearerToken_IsUnauthorized()
        {
            using var content = new StringContent("{\"orderNum\":\"test\"}", Encoding.UTF8, "application/json");
            content.Headers.Add("Idempotency-Key", "integration-test-key");

            var response = await _client.PostAsync("/api/Payment/DoWXAppPayOrder", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task RemovedTestLoginEndpoint_IsNotFound()
        {
            var response = await _client.GetAsync("/api/User/TestLogin");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task BrowserPost_WithoutAntiforgeryToken_IsRejected()
        {
            using var content = new StringContent("name=a&phone=13800138000&content=test", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _client.PostAsync("/Server/DoPostMessage", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task HealthCheck_ReturnsOnlyBasicStatus()
        {
            var response = await _client.GetAsync("/health");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Healthy", body);
        }

        [Fact]
        public async Task PrivateFileDownload_WithoutAuthentication_IsUnauthorized()
        {
            var response = await _client.GetAsync("/api/v1/files/2026/01/private.txt");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task MemberCookieLogin_WithoutAntiforgeryToken_IsRejected()
        {
            using var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/auth/cookie", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
