using System.IO;
using Microsoft.AspNetCore.Http;

namespace NewLife.UserGroup.WebUploader
{
    public static class UploaderFileHelper
    {
        public static void SaveFile(this IFormFile file, string targetDir, string fileName = null)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var filePath = Path.Combine(targetDir, fileName ?? file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }

        public static (bool, string) Merge(this string tempDir, string targetDir, string fileName, string md5, int chunks)
        {
            tempDir = Path.Combine(tempDir, md5);

            if (!Directory.Exists(tempDir))
            {
                return (false, "文件信息不存在");
            }

            string[] files = Directory.GetFiles(tempDir, "*.part");

            if (files.Length != chunks)
            {
                return (false, "文件不完整");
            }

            var filePath = Path.Combine(targetDir, fileName);

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    var partPath = Path.Combine(tempDir, $"{i}.part");
                    byte[] array = File.ReadAllBytes(partPath);
                    fileStream.Write(array, 0, array.Length);
                }

                fileStream.Flush();
            }

            Directory.Delete(tempDir, true);

            return (true, null);
        }

        public static void SaveChunk(this IFormFile file, string targetDir, string md5, int chunk)
        {
            var fileName = $"{chunk}.parttmp";
            targetDir = Path.Combine(targetDir, md5);
            file.SaveFile(targetDir, fileName);

            var srcFilePath = Path.Combine(targetDir, fileName);
            var destFilePath = Path.Combine(targetDir, $"{chunk}.part");
            // 重命名
            File.Move(srcFilePath, destFilePath);
        }

        public static void SaveFileOrChunkFile(this IFormFile file, string targetDir, string tempDir, 
            string md5, int? chunks, int? chunk)
        {
            // 启用了分块
            if (chunks != null && chunk != null && chunks > 1)
            {
                file.SaveChunk(tempDir, md5, chunk.Value);
            }
            else
            {
                file.SaveFile(targetDir);
            }
        }
    }
}
