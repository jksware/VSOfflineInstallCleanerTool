using System;
using System.IO;
using System.Linq;

namespace VsOfflineInstallCleaner
{
    internal class Program
    {
        const string unneededPackagesFolderName = "ToBeRemoved";
        public const string CertificatesDirName = "certificates";

        internal static void Main(string[] args)
        {
            string vsOfflineDirectory = args.Length > 0
                ? args[0]
                : Path.GetDirectoryName(Directory.GetCurrentDirectory());

            Console.Error.WriteLine($"Visual Studio Offline Directory: '{vsOfflineDirectory}'");

            string catalogFileName = "Catalog.json";
            if (args.Length > 1)
                catalogFileName = args[1];

            Console.Error.WriteLine($"CatalogFileName: '{catalogFileName}'");

            var folderNames = CleanVs.GetFolderNames(vsOfflineDirectory);
            Console.Error.WriteLine($"Number of Folder before cleanup : {folderNames.Count}");

            var packageNames = CleanVs.GetPackageNames(Path.Combine(vsOfflineDirectory, catalogFileName));
            Console.Error.WriteLine($"Number of Packages in the catalog File: {packageNames.Count}");

            folderNames.ExceptWith([CertificatesDirName, unneededPackagesFolderName]);
            folderNames.ExceptWith(packageNames);

            long bytesToMove = DirectorySize.GetFolderSize(folderNames.Select(x => Path.Combine(vsOfflineDirectory, x)));
            Console.Error.WriteLine($"Folder to be Cleaned: {folderNames.Count}. Size: {DirectorySize.GetHumanReadableSize(bytesToMove)}");
            Console.Error.WriteLine($"Number of Folder Needs to be Cleaned : {folderNames.Count}");

            Console.Error.WriteLine($@"Unneeded Folder will be moved to '{unneededPackagesFolderName}' Folder");
            CleanVs.MoveFolders(vsOfflineDirectory, folderNames, unneededPackagesFolderName);

            try
            {
                long savedDiskSpaceBytes = DirectorySize.GetFolderSize(Path.Combine(vsOfflineDirectory, unneededPackagesFolderName));
                string humanReadableSize = DirectorySize.GetHumanReadableSize(savedDiskSpaceBytes);
                Console.Error.WriteLine($@"Cleanup process is done, moving about {humanReadableSize}. Remove the '{unneededPackagesFolderName}' directory to save disk space.");
            }
            catch (Exception)
            {
                Console.Error.WriteLine($@"Error when trying to clean directory.");
            }
        }
    }
}