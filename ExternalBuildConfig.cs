// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertToAutoProperty

using System.Text;
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

        public override string ToString()
        {
            StringBuilder sb = new();
            
            sb.AppendLine(nameof(unity_build_executable_output_path) + ": " + unity_build_executable_output_path);
            sb.AppendLine(nameof(unity_build_target) + ": " + unity_build_target);
            sb.AppendLine(nameof(unity_development_build) + ": " + unity_development_build);
            sb.AppendLine(nameof(unity_auto_connect_profiler) + ": " + unity_auto_connect_profiler);
            sb.AppendLine(nameof(unity_deep_profiling_support) + ": " + unity_deep_profiling_support);
            sb.AppendLine(nameof(unity_script_debugging) + ": " + unity_script_debugging);
            sb.AppendLine(nameof(unity_compression_method) + ": " + unity_compression_method);

            sb.AppendLine(nameof(unity_scripting_defines) + ": ");
            foreach (string define in unity_scripting_defines)
            {
                sb.AppendLine(define);
            }
            
            return sb.ToString();
        }
    }
}