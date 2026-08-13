using Ganss.Xss;

namespace COMCMS.Common.Security
{
    public interface IContentSanitizer
    {
        string Sanitize(string html);
    }

    public sealed class ContentSanitizer : IContentSanitizer
    {
        private readonly HtmlSanitizer _sanitizer;

        public ContentSanitizer()
        {
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedTags.Add("video");
            _sanitizer.AllowedTags.Add("source");
            _sanitizer.AllowedAttributes.Add("controls");
            _sanitizer.AllowedAttributes.Add("poster");
            _sanitizer.AllowedAttributes.Add("preload");
            _sanitizer.AllowedAttributes.Add("playsinline");
            _sanitizer.AllowedAttributes.Add("type");
            _sanitizer.AllowedAttributes.Add("width");
            _sanitizer.AllowedAttributes.Add("height");
            _sanitizer.AllowedAttributes.Add("target");
            _sanitizer.AllowedCssProperties.Remove("behavior");
            _sanitizer.AllowedCssProperties.Remove("-moz-binding");
        }

        public string Sanitize(string html) => string.IsNullOrEmpty(html) ? html : _sanitizer.Sanitize(html);
    }
}
