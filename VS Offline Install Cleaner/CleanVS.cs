using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace VsOfflineInstallCleaner
{
    public static class CleanVs
    {
        internal static void MoveFolders(string vsOfflineDirectory, IEnumerable<string> sources, string destinationFolder)
        {
            bool exists = Directory.Exists($@"{vsOfflineDirectory}\{unneededPackagesfolderName}");

            if (!exists)
                Directory.CreateDirectory($@"{vsOfflineDirectory}\{unneededPackagesfolderName}");

            Directory.CreateDirectory($@"{vsOfflineDirectory}\{unneededPackagesfolderName}");

            foreach (string packageFolderName in pakagesTobeMoved)
            {
                string sourceDirName = $@"{vsOfflineDirectory}\{packageFolderName}";
                string destinationDirName = $@"{vsOfflineDirectory}\{unneededPackagesfolderName}\{packageFolderName}";

                try
                {
                    Directory.Move(sourceDirName, destinationDirName);
                }
                catch (System.Exception)
                {
                    // ignored
                }
            }
        }
        internal HashSet<string> GetPackageNames(string catalogFileName)
        {

        internal static List<string> GetPackageNames(string catalogFileName)
        {
            string catalogFileContent = File.ReadAllText(catalogFileName);

            Catalog catalog = JsonConvert.DeserializeObject<Catalog>(catalogFileContent);

            List<string> packageNames = new List<string>();

            foreach (Package package in catalog.Packages)
            {
                string currentpackageName = package.Id;

                if (!string.IsNullOrEmpty(package.Version))
                    currentpackageName += $",version={package.Version}";

                if (!string.IsNullOrEmpty(package.Chip))
                    currentpackageName += $",chip={package.Chip}";

                if (!string.IsNullOrEmpty(package.Language))
                    currentpackageName += $",language={package.Language}";

                if (!string.IsNullOrEmpty(package.Branch))
                    currentpackageName += $",branch={package.Branch}";

                if (!string.IsNullOrEmpty(package.Productarch))
                    currentpackageName += $",productarch={package.Productarch}";

                if (!string.IsNullOrEmpty(package.Machinearch))
                    currentpackageName += $",machinearch={package.Machinearch}";

                packageNames.Add(currentpackageName);
            }
            return packageNames.ToHashSet();
        }

        internal static HashSet<string> GetFolderNames(string vsOfflineDirectory)
        {
            var vsFolderNames = Directory.GetDirectories(vsOfflineDirectory)
                .Select(folderpath => new DirectoryInfo(folderpath).Name);

            return vsFolderNames.ToHashSet();
        }
    }
}
