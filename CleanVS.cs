using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VsOfflineInstallCleaner
{
    public static class CleanVs
    {
        internal static void MoveFolders(string vsOfflineDirectory, IEnumerable<string> sources, string destinationFolder)
        {
            destinationFolder = Path.Combine(vsOfflineDirectory, destinationFolder);

            bool exists = Directory.Exists(destinationFolder);

            if (!exists)
                Directory.CreateDirectory(destinationFolder);

            foreach (string path in sources)
            {
                string sourcePath = Path.Combine(vsOfflineDirectory, path);
                string destinationPath = Path.Combine(destinationFolder, path);

                try
                {
                    Console.Error.WriteLine($"Moving '{path}'...");
                    Directory.Move(sourcePath, destinationPath);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine($"Error moving '{path}' to '{destinationFolder}'.");
                }
            }
        }

        internal static List<string> GetPackageNames(string catalogFileName)
        {
            string catalogFileContent = File.ReadAllText(catalogFileName);

            var catalog = JsonConvert.DeserializeObject<Catalog>(catalogFileContent);

            List<string> result = [];

            foreach (var package in catalog.Packages)
            {
                string packageName = package.Id;

                if (!string.IsNullOrEmpty(package.Version))
                    packageName += $",version={package.Version}";

                if (!string.IsNullOrEmpty(package.Chip))
                    packageName += $",chip={package.Chip}";

                if (!string.IsNullOrEmpty(package.Language))
                    packageName += $",language={package.Language}";

                if (!string.IsNullOrEmpty(package.Branch))
                    packageName += $",branch={package.Branch}";

                if (!string.IsNullOrEmpty(package.ProductArch))
                    packageName += $",productarch={package.ProductArch}";

                if (!string.IsNullOrEmpty(package.MachineArch))
                    packageName += $",machinearch={package.MachineArch}";

                result.Add(packageName);
            }

            return result;
        }

        internal static HashSet<string> GetFolderNames(string vsOfflineDirectory)
        {
            return Directory
                .GetDirectories(vsOfflineDirectory)
                .Select(folderpath => new DirectoryInfo(folderpath).Name)
                .ToHashSet();
        }
    }
}
