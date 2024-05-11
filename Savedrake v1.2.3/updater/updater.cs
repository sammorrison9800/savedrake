using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        string downloadPath = args[0];
        string extractPath = args[1];
        string executablePath = args[2];

        try
        {
            // Extract the update package
            using (ZipArchive archive = ZipFile.OpenRead(downloadPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
                    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                    {
                        entry.ExtractToFile(destinationPath, true);
                    }
                }
            }

            // Delete the update package
            File.Delete(downloadPath);

            // Restart the main application
            Process.Start(executablePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating: {ex.Message}");
        }
    }
}