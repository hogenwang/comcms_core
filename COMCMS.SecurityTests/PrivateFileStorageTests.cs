using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using COMCMS.Web.Models;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class PrivateFileStorageTests
    {
        [Theory]
        [InlineData("../outside.txt")]
        [InlineData("../../windows/system.ini")]
        [InlineData("C:/Windows/system.ini")]
        public void TraversalAndAbsolutePaths_AreRejected(string key)
        {
            var storage = CreateStorage();

            Assert.False(storage.TryResolve(key, out _));
        }

        [Theory]
        [InlineData("MZpayload", ".pdf")]
        [InlineData("FWSpayload", ".txt")]
        [InlineData("<script>alert(1)</script>", ".txt")]
        [InlineData("<svg onload='alert(1)'>", ".txt")]
        public async Task ExecutableAndActiveHeaders_AreRejected(string content, string extension)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var formFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "file", "upload" + extension);

            await Assert.ThrowsAsync<InvalidDataException>(() => CreateStorage().SaveAsync(formFile, extension));
        }

        private static PrivateFileStorage CreateStorage()
        {
            var root = Path.Combine(Path.GetTempPath(), "comcms-security-tests", Guid.NewGuid().ToString("N"));
            var environment = new TestEnvironment { ContentRootPath = root, WebRootPath = Path.Combine(root, "wwwroot") };
            return new PrivateFileStorage(environment, Options.Create(new SecuritySettings { PrivateUploadRoot = Path.Combine(root, "uploads") }));
        }

        private sealed class TestEnvironment : IWebHostEnvironment
        {
            public string ApplicationName { get; set; } = "COMCMS.SecurityTests";
            public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
            public string WebRootPath { get; set; }
            public string EnvironmentName { get; set; } = "Development";
            public string ContentRootPath { get; set; }
            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        }
    }
}
