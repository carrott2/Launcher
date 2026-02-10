using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace BedrockCosmosUpdater
{
    internal class FileOperations
    {
        private readonly HttpClient httpClient;

        internal FileOperations()
        {
            httpClient = new HttpClient();
        }

        internal async Task DownloadFileAsync(string fileUrl, string downloadPath)
        {
            Console.WriteLine($"Downloading file at {fileUrl}");

            using (var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                }
            }

            Console.WriteLine($"Successfully downloaded file to {downloadPath}");
        }

        internal async Task ExtractFileAsync(string zipFilePath, string extractPath, bool deleteAfterExtracting)
        {
            if (!Directory.Exists(extractPath))
                Directory.CreateDirectory(extractPath);

            Console.WriteLine($"Extracting {Path.GetFileName(zipFilePath)}...");

            await Task.Run(() =>
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string destinationFilePath = Path.Combine(extractPath, entry.FullName);

                        if (entry.FullName.EndsWith("/"))
                            Directory.CreateDirectory(destinationFilePath);
                        else
                            entry.ExtractToFile(destinationFilePath, overwrite: true);
                    }
                }

                if (deleteAfterExtracting)
                {
                    File.Delete(zipFilePath);
                    Console.WriteLine($"Removed {Path.GetFileName(zipFilePath)} file.");
                }
            });

            Console.WriteLine($"Successfully extracted {Path.GetFileName(zipFilePath)} to " +
                $"{extractPath}");
        }

        internal async Task MoveDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(sourceDir))
                throw new DirectoryNotFoundException($"Source directory not found: {sourceDir}");

            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));

                if (File.Exists(destFile))
                    File.Delete(destFile);

                File.Move(file, destFile);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                await MoveDirectory(dir, destSubDir);
            }

            Directory.Delete(sourceDir, true);
        }

        internal void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
