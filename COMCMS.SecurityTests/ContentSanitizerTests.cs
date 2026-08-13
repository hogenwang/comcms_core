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
    }
}
