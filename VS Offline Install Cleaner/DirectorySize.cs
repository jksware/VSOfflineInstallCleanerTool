using System;

namespace VsOfflineInstallCleaner
{
    internal static class DirectorySize
    {
        public static string GetFolderSize(string folderPath)
        {
            Scripting.FileSystemObject fso = new Scripting.FileSystemObject();
            Scripting.Folder folder = fso.GetFolder(folderPath);
            dynamic dirSize = folder.Size;
            long dirSizeInt = Convert.ToInt64(dirSize);
            string dirSizeString = BytesToString(dirSizeInt);
            return dirSizeString;
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