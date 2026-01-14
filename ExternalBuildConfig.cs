// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertToAutoProperty

using UnityEngine;

namespace NPTP.GamedevAutomationsUnityHelper
{
    internal struct ExternalBuildConfig
    {
        // These private field names have to match the build tool JSON naming for JsonUtility.FromJson calls to work cleanly.
        [SerializeField] private string unity_build_executable_output_path;
        [SerializeField] private string unity_build_target;
        [SerializeField] private bool unity_development_build;
        [SerializeField] private bool unity_auto_connect_profiler;
        [SerializeField] private bool unity_deep_profiling_support;
        [SerializeField] private bool unity_script_debugging;
        [SerializeField] private string unity_compression_method;
        [SerializeField] private string[] unity_scripting_defines;

        public string BuildExecutableOutputPath => unity_build_executable_output_path;
        public string BuildTarget => unity_build_target;
        public bool DevelopmentBuild => unity_development_build;
        public bool AutoConnectProfiler => unity_auto_connect_profiler;
        public bool DeepProfilingSupport => unity_deep_profiling_support;
        public bool ScriptDebugging => unity_script_debugging;
        public string CompressionMethod => unity_compression_method;
        public string[] ScriptingDefines => unity_scripting_defines;
    }
}