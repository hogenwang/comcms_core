using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using COMCMS.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class PublicMediaStorageTests
    {
        [Fact]
        public async Task ValidMp4_IsStoredOutsideWebRoot()
        {
            var bytes = new byte[]
            {
                0, 0, 0, 24, (byte)'f', (byte)'t', (byte)'y', (byte)'p',
                (byte)'i', (byte)'s', (byte)'o', (byte)'m', 0, 0, 0, 0,
                (byte)'i', (byte)'s', (byte)'o', (byte)'m', (byte)'m', (byte)'p', (byte)'4', (byte)'2'
            };
            var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "upload", "sample.mp4")
            {
                Headers = new HeaderDictionary(),
                ContentType = "video/mp4"
            };
            var storage = CreateStorage(out var root);

            var stored = await storage.SaveVideoAsync(file);

            Assert.True(storage.TryResolve(stored.StorageKey, out var path, out var contentType));
            Assert.StartsWith(Path.Combine(root, "App_Data", "media"), path, StringComparison.OrdinalIgnoreCase);
            Assert.Equal("video/mp4", contentType);
            Assert.True(File.Exists(path));
        }

        [Theory]
        [InlineData("MZpayload")]
        [InlineData("not a video")]
        public async Task InvalidMp4Header_IsRejected(string content)
        {
            var bytes = Encoding.ASCII.GetBytes(content);
            var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "upload", "sample.mp4")
            {
                Headers = new HeaderDictionary(),
                ContentType = "video/mp4"
            };

            await Assert.ThrowsAsync<InvalidDataException>(() => CreateStorage(out _).SaveVideoAsync(file));
        }

        [Fact]
        public async Task OggAudioWithoutTheora_IsRejected()
        {
            var bytes = Encoding.ASCII.GetBytes("OggS......OpusHead audio payload");
            var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "upload", "audio.ogg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "video/ogg"
            };

            await Assert.ThrowsAsync<InvalidDataException>(() => CreateStorage(out _).SaveVideoAsync(file));
        }

        [Theory]
        [InlineData("../outside.mp4")]
        [InlineData("../../windows/system.ini")]
        [InlineData("C:/Windows/system.ini")]
        public void TraversalAndNonMediaPaths_AreRejected(string key)
        {
            var storage = CreateStorage(out _);

            Assert.False(storage.TryResolve(key, out _, out _));
        }

        private static PublicMediaStorage CreateStorage(out string root)
        {
            root = Path.Combine(Path.GetTempPath(), "comcms-media-tests", Guid.NewGuid().ToString("N"));
            var environment = new TestEnvironment { ContentRootPath = root, WebRootPath = Path.Combine(root, "wwwroot") };
            return new PublicMediaStorage(environment);
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
