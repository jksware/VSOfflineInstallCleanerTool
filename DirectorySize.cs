using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VsOfflineInstallCleaner
{
    internal static class DirectorySize
    {
        public static long GetFolderSize(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Sum(x => x.Length);
        }

        public static long GetFolderSize(IEnumerable<string> filePaths)
        {
            return filePaths.Sum(GetFolderSize);
        }

        public static string GetHumanReadableSize(long bytes)
        {
            ReadOnlySpan<string> suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];

            bytes = Math.Abs(bytes);
            int logBase1024 = (int)long.Log2(bytes) / 10;
            int logBase2 = logBase1024 * 10;

            float significand = MathF.Round(bytes / (float)(1L << logBase2), 1);
            return significand + suffixes[logBase1024];
        }
    }
}