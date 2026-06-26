using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public static string CollectionToString<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return "null";
            }

            T[] array = collection.ToArray();
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString());
                if (i < array.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append('[');
            return sb.ToString();
        }
    }
}