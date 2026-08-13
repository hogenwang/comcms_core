using COMCMS.Common.Security;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class ContentSanitizerTests
    {
        private readonly ContentSanitizer _sanitizer = new ContentSanitizer();

        [Fact]
        public void DangerousActiveContent_IsRemoved()
        {
            const string input = "<p onclick=\"alert(1)\">ok</p><script>alert(2)</script><iframe src=\"https://evil.test\"></iframe>";

            var output = _sanitizer.Sanitize(input);

            Assert.Contains("<p>ok</p>", output);
            Assert.DoesNotContain("onclick", output);
            Assert.DoesNotContain("script", output);
            Assert.DoesNotContain("iframe", output);
        }

        [Fact]
        public void JavascriptUrl_IsRemoved()
        {
            var output = _sanitizer.Sanitize("<a href=\"javascript:alert(1)\">link</a>");

            Assert.Contains(">link</a>", output);
            Assert.DoesNotContain("javascript:", output);
        }

        [Fact]
        public void LocalVideo_IsPreservedWithoutActiveAttributes()
        {
            const string input = "<video controls preload=\"metadata\" playsinline src=\"/media/2026/08/test.mp4\" onerror=\"alert(1)\"></video>";

            var output = _sanitizer.Sanitize(input);

            Assert.Contains("<video", output);
            Assert.Contains("controls", output);
            Assert.Contains("preload=\"metadata\"", output);
            Assert.Contains("src=\"/media/2026/08/test.mp4\"", output);
            Assert.DoesNotContain("onerror", output);
        }
    }
}
