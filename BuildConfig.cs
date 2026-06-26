// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertToAutoProperty

using System;
using System.Text;

namespace NPTP.GamedevAutomationsUnity
{
    internal struct BuildConfig
    {
        // These private field names have to match the argument names passed in by the Python build tool.
        private bool unity_development_build;
        private bool unity_auto_connect_profiler;
        private bool unity_deep_profiling_support;
        private bool unity_script_debugging;
        private string unity_build_executable_output_path;
        private string unity_build_target;
        private string unity_compression_method;
        private string[] unity_scripting_defines;

        public bool DevelopmentBuild => unity_development_build;
        public bool AutoConnectProfiler => unity_auto_connect_profiler;
        public bool DeepProfilingSupport => unity_deep_profiling_support;
        public bool ScriptDebugging => unity_script_debugging;
        public string BuildExecutableOutputPath => unity_build_executable_output_path;
        public string BuildTarget => unity_build_target;
        public string CompressionMethod => unity_compression_method;
        public string[] ScriptingDefines => unity_scripting_defines;

        public static BuildConfig FromCommandLine()
        {
            BuildConfig config = new BuildConfig();
            string value;
            
            config.unity_auto_connect_profiler = Utilities.HasCLIArg(nameof(unity_auto_connect_profiler));
            config.unity_deep_profiling_support = Utilities.HasCLIArg(nameof(unity_deep_profiling_support));
            config.unity_script_debugging = Utilities.HasCLIArg(nameof(unity_script_debugging));
            config.unity_development_build = Utilities.HasCLIArg(nameof(unity_development_build));
            config.unity_build_executable_output_path = Utilities.TryGetCLIArgValue(nameof(unity_build_executable_output_path), out value) ? value : string.Empty;
            config.unity_build_target = Utilities.TryGetCLIArgValue(nameof(unity_build_target), out value) ? value : string.Empty;
            config.unity_compression_method = Utilities.TryGetCLIArgValue(nameof(unity_compression_method), out value) ? value : string.Empty;
            config.unity_scripting_defines = Utilities.TryGetCLIArgValue(nameof(unity_scripting_defines), out value) ? value.Split(';') : Array.Empty<string>();
            
            return config;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine(nameof(unity_build_executable_output_path) + ": " + unity_build_executable_output_path);
            sb.AppendLine(nameof(unity_build_target) + ": " + unity_build_target);
            sb.AppendLine(nameof(unity_development_build) + ": " + unity_development_build);
            sb.AppendLine(nameof(unity_auto_connect_profiler) + ": " + unity_auto_connect_profiler);
            sb.AppendLine(nameof(unity_deep_profiling_support) + ": " + unity_deep_profiling_support);
            sb.AppendLine(nameof(unity_script_debugging) + ": " + unity_script_debugging);
            sb.AppendLine(nameof(unity_compression_method) + ": " + unity_compression_method);
            sb.AppendLine(nameof(unity_scripting_defines) + ": " + Utilities.CollectionToString(unity_scripting_defines));
            
            return sb.ToString();
        }
    }
}