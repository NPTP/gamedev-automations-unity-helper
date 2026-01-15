using System;
using System.IO;
using System.Linq;
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
                ExternalBuildConfig buildConfig = Utilities.ParseJsonFromCommandLine<ExternalBuildConfig>(BUILD_CONFIG_CLI_ARGUMENT);
                
                Debug.Log($"{nameof(ExternalBuildConfig)} parsed from command line JSON : {buildConfig.ToString()}");
                
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
                    locationPathName = Path.GetFullPath(buildConfig.BuildExecutableOutputPath),
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

                Utilities.DeleteLocalFolderIfExists($"{executableName}_BurstDebugInformation_DoNotShip", outputFolder); // Burst Debug
                Utilities.DeleteLocalFolderIfExists($"{executableName}_BackUpThisFolder_ButDontShipItWithYourGame", outputFolder); // IL2CPP
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}