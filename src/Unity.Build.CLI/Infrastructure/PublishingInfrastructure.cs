using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Unity.Build.CLI.Infrastructure
{
    internal static class PublishingInfrastructure
    {
        internal static void ArchiveDirectories(string outputFileName, IReadOnlyCollection<string> directoriesToZip)
        {
            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                Console.Error.WriteLine("It was not possible to proceed with the archive sequence because the path to the output archive file is missing.");
            }

            using var zip = ZipFile.Open(outputFileName, ZipArchiveMode.Create);

            foreach (var directoryPath in directoriesToZip)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Console.Error.WriteLine($"It was not possible to proceed with the archive sequence for {directoryPath} because it couldn't be found on the host machine.");
                    continue;
                }

                foreach (var file in Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories))
                {
                    zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                }
            }
        }
    }
}
