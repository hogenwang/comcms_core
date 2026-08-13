using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace COMCMS.Web.Services
{
    public sealed class PublicMediaStorage
    {
        public const long MaxVideoSize = 50L * 1024 * 1024;

        private static readonly IReadOnlyDictionary<string, string> MediaTypes =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                [".mp4"] = "video/mp4",
                [".webm"] = "video/webm",
                [".ogv"] = "video/ogg",
                [".ogg"] = "video/ogg"
            };

        private readonly string _root;

        public PublicMediaStorage(IWebHostEnvironment environment)
        {
            _root = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "App_Data", "media"));
        }

        public async Task<StoredMedia> SaveVideoAsync(IFormFile file)
        {
            if (file == null || file.Length <= 0 || file.Length > MaxVideoSize)
                throw new InvalidDataException("视频为空或超过 50 MiB。");

            var extension = Path.GetExtension(Path.GetFileName(file.FileName)).ToLowerInvariant();
            if (!MediaTypes.TryGetValue(extension, out var mediaType))
                throw new InvalidDataException("仅支持 MP4、WebM 和 Ogg 视频。");

            if (!string.Equals(file.ContentType, mediaType, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("视频扩展名与 MIME 类型不匹配。");

            await ValidateVideoHeaderAsync(file, extension);

            var relativeDirectory = Path.Combine(DateTime.UtcNow.ToString("yyyy"), DateTime.UtcNow.ToString("MM"));
            var directory = Path.Combine(_root, relativeDirectory);
            Directory.CreateDirectory(directory);
            var storedName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(directory, storedName);
            await using var output = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true);
            await file.CopyToAsync(output);

            return new StoredMedia(
                Path.Combine(relativeDirectory, storedName).Replace('\\', '/'),
                Path.GetFileName(file.FileName),
                mediaType);
        }

        public bool TryResolve(string storageKey, out string fullPath, out string contentType)
        {
            fullPath = null;
            contentType = null;
            if (string.IsNullOrWhiteSpace(storageKey)) return false;

            var candidate = Path.GetFullPath(Path.Combine(
                _root,
                storageKey.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar)));
            var relative = Path.GetRelativePath(_root, candidate);
            if (Path.IsPathRooted(relative) || relative == ".." ||
                relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                return false;

            if (!MediaTypes.TryGetValue(Path.GetExtension(candidate), out contentType)) return false;
            fullPath = candidate;
            return true;
        }

        public static string BuildUrl(StoredMedia stored) => $"/media/{stored.StorageKey}";

        private static async Task ValidateVideoHeaderAsync(IFormFile file, string extension)
        {
            var buffer = new byte[Math.Min(4096, checked((int)file.Length))];
            await using var input = file.OpenReadStream();
            var read = await input.ReadAsync(buffer);

            var valid = extension switch
            {
                ".mp4" => IsMp4Header(buffer.AsSpan(0, read), file.Length),
                ".webm" => read >= 8 && buffer.AsSpan(0, 4).SequenceEqual(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }) &&
                           ContainsAscii(buffer.AsSpan(0, read), "webm"),
                ".ogv" or ".ogg" => read >= 10 && buffer.AsSpan(0, 4).SequenceEqual("OggS"u8) &&
                                      ContainsAscii(buffer.AsSpan(0, read), "theora"),
                _ => false
            };

            if (!valid) throw new InvalidDataException("视频文件特征校验失败。");
        }

        private static bool ContainsAscii(ReadOnlySpan<byte> source, string value)
        {
            var target = System.Text.Encoding.ASCII.GetBytes(value);
            return source.IndexOf(target) >= 0;
        }

        private static bool IsMp4Header(ReadOnlySpan<byte> source, long fileLength)
        {
            if (source.Length < 12 || !source.Slice(4, 4).SequenceEqual("ftyp"u8)) return false;
            var boxSize = BinaryPrimitives.ReadUInt32BigEndian(source.Slice(0, 4));
            return boxSize >= 8 && boxSize <= fileLength;
        }
    }

    public sealed record StoredMedia(string StorageKey, string OriginalName, string ContentType);
}
