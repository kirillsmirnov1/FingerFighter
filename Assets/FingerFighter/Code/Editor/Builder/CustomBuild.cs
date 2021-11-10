using System.Linq;
using UnityEditor;

namespace FingerFighter.Builder
{
    public static class CustomBuild
    {
        private enum BuildType
        {
            TestApk,
            ReleaseAab,
        }
        
        [MenuItem("Build/Dev apk Build and Run")]
        public static void DevBuildAndRun()
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            PlayerSettings.Android.useCustomKeystore = false;
            EditorUserBuildSettings.buildAppBundle = false;
            BuildPipeline.BuildPlayer(PrepareOptions(BuildType.TestApk));
        }

        [MenuItem("Build/Release aab Build")]
        public static void ReleaseBuild()
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            EditorUserBuildSettings.buildAppBundle = true;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            PlayerSettings.Android.useCustomKeystore = true;

            PlayerSettings.Android.keystoreName = "C:/Users/Kiril/Dropbox/archive/android keys/fingerFighters.keystore";
            PlayerSettings.Android.keyaliasName = "fingerfighters";
            PlayerSettings.Android.keyaliasPass = PlayerSettings.Android.keystorePass = "PASS"; // IMPR show window 

            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30; // FIXME doesn't work for some reason 
            
            BuildPipeline.BuildPlayer(PrepareOptions(BuildType.ReleaseAab));
        }

        private static BuildPlayerOptions PrepareOptions(BuildType buildType) =>
            new BuildPlayerOptions
            {
                locationPathName = $"Build/ff.{(buildType == BuildType.TestApk ? "apk" : "aab")}",
                scenes = EditorBuildSettings.scenes
                    .Where(x => x.enabled)
                    .Select(x => x.path).ToArray(),
                options = buildType == BuildType.TestApk ? BuildOptions.AutoRunPlayer : BuildOptions.ShowBuiltPlayer,
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android
            };
    }
}