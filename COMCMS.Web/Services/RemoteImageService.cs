using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using SkiaSharp;

namespace COMCMS.Web.Services
{
    public sealed class RemoteImageService
    {
        public const string ClientName = "RemoteImages";
        public const int MaxImagesPerImport = 20;
        private const int MaxImageBytes = 10 * 1024 * 1024;
        private static readonly TimeSpan MaxImportDuration = TimeSpan.FromSeconds(30);
        private readonly IHttpClientFactory _clients;
        private readonly IWebHostEnvironment _environment;

        public RemoteImageService(IHttpClientFactory clients, IWebHostEnvironment environment)
        {
            _clients = clients;
            _environment = environment;
        }

        public async Task<string> ImportAsync(string html, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(html)) return string.Empty;
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(MaxImportDuration);

            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync($"<body>{html}</body>", timeoutCts.Token);
            int importedCount = 0;
            foreach (var image in document.Images.ToArray())
            {
                if (importedCount >= MaxImagesPerImport || timeoutCts.IsCancellationRequested) break;
                if (!Uri.TryCreate(image.Source, UriKind.Absolute, out var uri) ||
                    !IsPermittedRemoteUri(uri)) continue;

                importedCount++;
                var localUrl = await DownloadAsync(uri, timeoutCts.Token);
                if (localUrl != null) image.Source = localUrl;
            }
            return document.Body?.InnerHtml ?? html;
        }

        public static SocketsHttpHandler CreateHandler() => new SocketsHttpHandler
        {
            AllowAutoRedirect = false,
            ConnectTimeout = TimeSpan.FromSeconds(5),
            ConnectCallback = ConnectToPublicAddressAsync
        };

        public static bool IsPermittedRemoteUri(Uri uri)
        {
            if (uri == null) return false;
            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps) return false;
            return uri.Port == 80 || uri.Port == 443;
        }

        public static bool IsPublicAddress(IPAddress address)
        {
            if (address == null) return false;
            if (address.IsIPv4MappedToIPv6) address = address.MapToIPv4();
            if (IPAddress.IsLoopback(address) || address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any) ||
                address.Equals(IPAddress.None) || address.Equals(IPAddress.IPv6None)) return false;

            var bytes = address.GetAddressBytes();
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return bytes[0] != 0 && bytes[0] != 10 && bytes[0] != 127 && bytes[0] < 224 &&
                       !(bytes[0] == 100 && bytes[1] is >= 64 and <= 127) &&
                       !(bytes[0] == 169 && bytes[1] == 254) &&
                       !(bytes[0] == 172 && bytes[1] is >= 16 and <= 31) &&
                       !(bytes[0] == 192 && bytes[1] == 0 && bytes[2] is 0 or 2) &&
                       !(bytes[0] == 192 && bytes[1] == 168) &&
                       !(bytes[0] == 198 && bytes[1] is 18 or 19) &&
                       !(bytes[0] == 198 && bytes[1] == 51 && bytes[2] == 100) &&
                       !(bytes[0] == 203 && bytes[1] == 0 && bytes[2] == 113);
            }

            if (address.AddressFamily != AddressFamily.InterNetworkV6) return false;
            if (address.IsIPv6LinkLocal || address.IsIPv6SiteLocal || address.IsIPv6Multicast ||
                (bytes[0] & 0xFE) == 0xFC) return false;

            // 拦截 6to4 (2002::/16), Teredo (2001:0000::/32), NAT64 (64:ff9b::/96), Documentation (2001:db8::/32)
            if (bytes[0] == 0x20 && bytes[1] == 0x02) return false;
            if (bytes[0] == 0x20 && bytes[1] == 0x01 && bytes[2] == 0x00 && bytes[3] == 0x00) return false;
            if (bytes[0] == 0x20 && bytes[1] == 0x01 && bytes[2] == 0x0D && bytes[3] == 0xB8) return false;
            if (bytes[0] == 0x00 && bytes[1] == 0x64 && bytes[2] == 0xFF && bytes[3] == 0x9B) return false;

            return true;
        }

        private async Task<string> DownloadAsync(Uri uri, CancellationToken cancellationToken)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                using var response = await _clients.CreateClient(ClientName)
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                if (!response.IsSuccessStatusCode || response.Content.Headers.ContentLength > MaxImageBytes) return null;

                await using var input = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var imageData = new MemoryStream();
                var buffer = new byte[81920];
                int read;
                while ((read = await input.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    if (imageData.Length + read > MaxImageBytes) return null;
                    await imageData.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                }
                imageData.Position = 0;
                using var codec = SKCodec.Create(imageData);
                var extension = codec?.EncodedFormat switch
                {
                    SKEncodedImageFormat.Jpeg => ".jpg",
                    SKEncodedImageFormat.Png => ".png",
                    SKEncodedImageFormat.Gif => ".gif",
                    SKEncodedImageFormat.Bmp => ".bmp",
                    SKEncodedImageFormat.Webp => ".webp",
                    _ => null
                };
                if (extension == null) return null;

                var relativeDirectory = Path.Combine("userfiles", "images", "auto", DateTime.UtcNow.ToString("yyyy"), DateTime.UtcNow.ToString("MM"));
                var directory = Path.GetFullPath(Path.Combine(_environment.WebRootPath, relativeDirectory));
                if (!IsWithinRoot(_environment.WebRootPath, directory)) return null;
                Directory.CreateDirectory(directory);
                var fileName = $"{Guid.NewGuid():N}{extension}";
                imageData.Position = 0;
                await using var output = new FileStream(Path.Combine(directory, fileName), FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true);
                await imageData.CopyToAsync(output, cancellationToken);
                return $"/{relativeDirectory.Replace('\\', '/')}/{fileName}";
            }
            catch (Exception exception) when (exception is HttpRequestException or IOException or OperationCanceledException or SocketException or ArgumentException or NotSupportedException)
            {
                return null;
            }
        }

        private static async ValueTask<Stream> ConnectToPublicAddressAsync(SocketsHttpConnectionContext context, CancellationToken cancellationToken)
        {
            if (context.DnsEndPoint.Port is not (80 or 443))
                throw new HttpRequestException("Only ports 80 and 443 are permitted for remote image downloads.");

            var addresses = await Dns.GetHostAddressesAsync(context.DnsEndPoint.Host, cancellationToken);
            Exception lastError = null;
            foreach (var address in addresses.Where(IsPublicAddress))
            {
                var socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    await socket.ConnectAsync(new IPEndPoint(address, context.DnsEndPoint.Port), cancellationToken);
                    return new NetworkStream(socket, ownsSocket: true);
                }
                catch (Exception ex) when (ex is SocketException or OperationCanceledException)
                {
                    socket.Dispose();
                    lastError = ex;
                }
            }
            throw new HttpRequestException("Remote image host did not resolve to a permitted public address.", lastError);
        }

        private static bool IsWithinRoot(string root, string candidate)
        {
            var relative = Path.GetRelativePath(Path.GetFullPath(root), candidate);
            return !Path.IsPathRooted(relative) && relative != ".." && !relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal);
        }
    }
}
