using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using COMCMS.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace COMCMS.Web.Services
{
    public sealed class PrivateFileStorage
    {
        private static readonly byte[][] ForbiddenHeaders =
        {
            new byte[] { 0x4D, 0x5A },
            new byte[] { 0x7F, 0x45, 0x4C, 0x46 },
            new byte[] { 0x46, 0x57, 0x53 },
            new byte[] { 0x43, 0x57, 0x53 },
            new byte[] { 0x5A, 0x57, 0x53 }
        };
        private readonly string _root;

        public PrivateFileStorage(IWebHostEnvironment environment, IOptions<SecuritySettings> settings)
        {
            var configured = settings.Value.PrivateUploadRoot;
            _root = Path.GetFullPath(string.IsNullOrWhiteSpace(configured)
                ? Path.Combine(environment.ContentRootPath, "App_Data", "uploads")
                : (Path.IsPathRooted(configured) ? configured : Path.Combine(environment.ContentRootPath, configured)));
            var webRoot = Path.GetFullPath(environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot"));
            var relativeToWebRoot = Path.GetRelativePath(webRoot, _root);
            if (relativeToWebRoot == "." || (!Path.IsPathRooted(relativeToWebRoot) && relativeToWebRoot != ".." &&
                !relativeToWebRoot.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal)))
                throw new InvalidOperationException("Private upload storage must be outside WebRoot.");
        }

        public async Task<StoredFile> SaveAsync(IFormFile file, string extension)
        {
            extension = (extension ?? string.Empty).ToLowerInvariant();
            if (extension.Length is < 2 or > 10 || extension[0] != '.' || extension.Skip(1).Any(character => !char.IsLetterOrDigit(character)))
                throw new InvalidDataException("Invalid file extension.");

            await ValidateHeaderAsync(file);
            var relativeDirectory = Path.Combine(DateTime.UtcNow.ToString("yyyy"), DateTime.UtcNow.ToString("MM"));
            var directory = Path.Combine(_root, relativeDirectory);
            Directory.CreateDirectory(directory);
            var storedName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(directory, storedName);
            await using var output = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true);
            await file.CopyToAsync(output);
            return new StoredFile(Path.Combine(relativeDirectory, storedName).Replace('\\', '/'), Path.GetFileName(file.FileName));
        }

        public bool TryResolve(string storageKey, out string fullPath)
        {
            fullPath = null;
            if (string.IsNullOrWhiteSpace(storageKey)) return false;
            var candidate = Path.GetFullPath(Path.Combine(_root, storageKey.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar)));
            var relative = Path.GetRelativePath(_root, candidate);
            if (Path.IsPathRooted(relative) || relative == ".." || relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                return false;
            fullPath = candidate;
            return true;
        }

        public static string BuildUrl(StoredFile stored) =>
            $"/api/v1/files/{stored.StorageKey}?name={Uri.EscapeDataString(stored.OriginalName)}";

        private static async Task ValidateHeaderAsync(IFormFile file)
        {
            var buffer = new byte[Math.Min(512, (int)file.Length)];
            await using var input = file.OpenReadStream();
            var read = await input.ReadAsync(buffer);
            if (ForbiddenHeaders.Any(header => read >= header.Length && buffer.AsSpan(0, header.Length).SequenceEqual(header)))
                throw new InvalidDataException("Active or executable content is not allowed.");

            var text = System.Text.Encoding.UTF8.GetString(buffer, 0, read).TrimStart('\uFEFF', ' ', '\t', '\r', '\n');
            if (text.StartsWith("<script", StringComparison.OrdinalIgnoreCase) ||
                text.StartsWith("<!doctype html", StringComparison.OrdinalIgnoreCase) ||
                text.StartsWith("<html", StringComparison.OrdinalIgnoreCase) ||
                text.StartsWith("<svg", StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Active content is not allowed.");
        }
    }

    public sealed record StoredFile(string StorageKey, string OriginalName);
}
