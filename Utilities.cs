using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace NPTP.GamedevAutomationsUnityHelper
{
    internal static class Utilities
    {
        internal static void DeleteLocalFolderIfExists(string localFolderName, string outputFolder)
        {
            string fullPath = Path.Combine(outputFolder, localFolderName);
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
        }

        internal static T ParseJsonFromCommandLine<T>(string argument)
        {
            string base64EncodedArgument = GetArg($"-{argument}");
            if (string.IsNullOrEmpty(base64EncodedArgument))
            {
                throw new Exception($"Missing -{argument} JSON argument.");
            }

            string json = Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedArgument));
            return JsonUtility.FromJson<T>(json);
        }

        private static string GetArg(string name)
        {
            string[] args = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(args, name);
            return index >= 0 && index < args.Length - 1
                ? args[index + 1]
                : null;
        }
    }
}