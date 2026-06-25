using System;
using System.IO;

namespace NPTP.GamedevAutomationsUnity
{
    internal static class Utilities
    {
        internal static string CreateValidFilePath(string filePath)
        {
            string fullPath = Path.GetFullPath(filePath);
            string dir = Path.GetDirectoryName(fullPath);
            
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return fullPath;
        }
        
        internal static void DeleteLocalFolderIfExists(string localFolderName, string outputFolder)
        {
            string fullPath = Path.Combine(outputFolder, localFolderName);
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
        }

        public static bool HasCLIArg(string argName)
        {
            string[] args = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(args, $"-{argName}");
            return index >= 0 && index < args.Length;
        }

        public static bool TryGetCLIArgValue(string argName, out string value)
        {
            string[] args = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(args, $"-{argName}");
            value = index >= 0 && index < args.Length - 1
                ? args[index + 1]
                : null;

            return value != null;
        }
    }
}