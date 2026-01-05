// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertToAutoProperty

using UnityEngine;

namespace NPTP.GamedevAutomationsUnityHelper
{
    public struct ExternalBuildConfig
    {
        // These private field names have to match the build tool JSON naming for JsonUtility.FromJson calls to work cleanly.
        [SerializeField] private string output_path;
        [SerializeField] private string build_target;
        [SerializeField] private bool development_build;
        [SerializeField] private bool auto_connect_profiler;
        [SerializeField] private bool deep_profiling_support;
        [SerializeField] private bool script_debugging;
        [SerializeField] private string compression_method;
        [SerializeField] private string[] scripting_defines;

        public string OutputPath => output_path;
        public string BuildTarget => build_target;
        public bool DevelopmentBuild => development_build;
        public bool AutoConnectProfiler => auto_connect_profiler;
        public bool DeepProfilingSupport => deep_profiling_support;
        public bool ScriptDebugging => script_debugging;
        public string CompressionMethod => compression_method;
        public string[] ScriptingDefines => scripting_defines;
    }
}