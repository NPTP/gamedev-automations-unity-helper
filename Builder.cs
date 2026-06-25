using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NPTP.GamedevAutomationsUnity
{
    public static class Builder
    {
        private const int SUCCESS_CODE = 0;
        private const int FAILURE_CODE = 1;
        private const string COMPRESSION_METHOD_DEFAULT = "default";
        private const string COMPRESSION_METHOD_LZ4 = "lz4";
        private const string COMPRESSION_METHOD_LZ4HC = "lz4hc";

        /// <summary>
        /// Used by automation build tool, called from the CLI - not meant to be called by other C# project code.
        /// </summary>
        public static void Build()
        {
            try
            {
                BuildConfig buildConfig = BuildConfig.FromCommandLine();
                
                // TODO: Remove this log
                Debug.Log($"{nameof(BuildConfig)} parsed from command line : {buildConfig.ToString()}");
                
                BuildOptions buildOptions = 0;
                if (buildConfig.DevelopmentBuild) buildOptions |= BuildOptions.Development;
                if (buildConfig.AutoConnectProfiler) buildOptions |= BuildOptions.ConnectWithProfiler;
                if (buildConfig.DeepProfilingSupport) buildOptions |= BuildOptions.EnableDeepProfilingSupport;
                if (buildConfig.ScriptDebugging) buildOptions |= BuildOptions.AllowDebugging;
                
                switch (buildConfig.CompressionMethod.ToLower())
                {
                    case COMPRESSION_METHOD_DEFAULT:
                        break;
                    case COMPRESSION_METHOD_LZ4:
                        buildOptions |= BuildOptions.CompressWithLz4;
                        break;
                    case COMPRESSION_METHOD_LZ4HC:
                        buildOptions |= BuildOptions.CompressWithLz4HC;
                        break;
                }
                
                BuildTarget buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), buildConfig.BuildTarget);

                string locationPathName = Utilities.CreateValidFilePath(buildConfig.BuildExecutableOutputPath);
                Debug.Log($"Location path name: {locationPathName}");
                
                BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray(),
                    locationPathName = "G:/Gamedev/Builds/Testing/UnityBuild/Unity_Test.exe",
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

                // Burst Debug
                Utilities.DeleteLocalFolderIfExists($"{executableName}_BurstDebugInformation_DoNotShip", outputFolder); 
                
                // IL2CPP
                Utilities.DeleteLocalFolderIfExists($"{executableName}_BackUpThisFolder_ButDontShipItWithYourGame", outputFolder);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}