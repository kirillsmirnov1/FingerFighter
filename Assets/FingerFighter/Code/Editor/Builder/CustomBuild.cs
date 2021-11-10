using System.Linq;
using UnityEditor;

namespace FingerFighter.Builder
{
    public static class CustomBuild
    {
        [MenuItem("Build/Dev apk Build and Run")]
        public static void DevBuildAndRun()
        {
            var options = new BuildPlayerOptions
            {
                locationPathName = "Build/ff.apk",
                scenes = EditorBuildSettings.scenes
                    .Where(x => x.enabled)
                    .Select(x => x.path).ToArray(),
                options = BuildOptions.AutoRunPlayer,
                target = BuildTarget.Android
            };
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            PlayerSettings.Android.useCustomKeystore = false;
            EditorUserBuildSettings.buildAppBundle = false;
            BuildPipeline.BuildPlayer(options);
        }
    }
}