using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NPTP.GamedevAutomationsUnityHelper
{
    public static class Builder
    {
        private const int SUCCESS_CODE = 0;
        private const int FAILURE_CODE = 1;
        private const string BUILD_CONFIG_CLI_ARGUMENT = "build_config";
        private const string COMPRESSION_METHOD_DEFAULT = "Default";
        private const string COMPRESSION_METHOD_LZ4 = "LZ4";
        private const string COMPRESSION_METHOD_LZ4HC = "LZ4HC";

        /// <summary>
        /// Used by automation build tool, called from the CLI - not meant to be called by other C# project code.
        /// </summary>
        public static void Build()
        {
            try
            {
                ExternalBuildConfig buildConfig = ParseJsonFromCommandLine<ExternalBuildConfig>(BUILD_CONFIG_CLI_ARGUMENT);
                
                BuildOptions buildOptions = 0;
                if (buildConfig.DevelopmentBuild) buildOptions |= BuildOptions.Development;
                if (buildConfig.AutoConnectProfiler) buildOptions |= BuildOptions.ConnectWithProfiler;
                if (buildConfig.DeepProfilingSupport) buildOptions |= BuildOptions.EnableDeepProfilingSupport;
                if (buildConfig.ScriptDebugging) buildOptions |= BuildOptions.AllowDebugging;
                
                switch (buildConfig.CompressionMethod)
                {
                    case COMPRESSION_METHOD_DEFAULT:
                        break;
                    case COMPRESSION_METHOD_LZ4:
                        buildOptions |= BuildOptions.CompressWithLz4;
                        break;
                    case COMPRESSION_METHOD_LZ4HC:
                        buildOptions |= BuildOptions.CompressWithLz4HC;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                BuildTarget buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), buildConfig.BuildTarget);

                BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray(),
                    locationPathName = Path.GetFullPath(buildConfig.OutputPath),
                    options = buildOptions,
                    target = buildTarget,
                    targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget),
                    extraScriptingDefines = buildConfig.ScriptingDefines
                };

                BuildPipeline.BuildPlayer(buildPlayerOptions);
                EditorApplication.Exit(SUCCESS_CODE);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                EditorApplication.Exit(FAILURE_CODE);
            }
        }

        [PostProcessBuild(9999)]
        public static void OnPostProcessBuild(BuildTarget target, string outputPath)
        {
            // Remove the auto-generated debug folders which shouldn't be shipped with the build.
            try
            {
                string executableName = Path.GetFileNameWithoutExtension(outputPath);
                string outputFolder = Path.GetFullPath(Path.GetDirectoryName(outputPath)!);

                deleteLocalFolderIfExists($"{executableName}_BurstDebugInformation_DoNotShip"); // Burst Debug
                deleteLocalFolderIfExists($"{executableName}_BackUpThisFolder_ButDontShipItWithYourGame"); // IL2CPP

                void deleteLocalFolderIfExists(string localFolderName)
                {
                    string fullPath = Path.Combine(outputFolder, localFolderName);
                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private static T ParseJsonFromCommandLine<T>(string argument)
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