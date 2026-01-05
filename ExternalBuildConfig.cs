// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertToAutoProperty
namespace NPTP.GamedevAutomationsUnityHelper
{
    public class ExternalBuildConfig
    {
        // These private field names have to match the build tool JSON naming for JsonUtility.FromJson calls to work cleanly.
        private string output_path;
        private string build_target;
        private bool development_build;
        private bool auto_connect_profiler;
        private bool deep_profiling_support;
        private bool script_debugging;
        private string compression_method;
        private string[] scripting_defines;
        
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