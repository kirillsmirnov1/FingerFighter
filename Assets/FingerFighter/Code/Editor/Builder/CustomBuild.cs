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
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android
            };
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            PlayerSettings.Android.useCustomKeystore = false;
            EditorUserBuildSettings.buildAppBundle = false;
            BuildPipeline.BuildPlayer(options);
        }
        
        [MenuItem("Build/Release aab Build")]
        public static void ReleaseBuild()
        {
            var options = new BuildPlayerOptions
            {
                locationPathName = "Build/ff.aab",
                scenes = EditorBuildSettings.scenes
                    .Where(x => x.enabled)
                    .Select(x => x.path).ToArray(),
                options = BuildOptions.ShowBuiltPlayer,
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
            };
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            EditorUserBuildSettings.buildAppBundle = true;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            PlayerSettings.Android.useCustomKeystore = true;

            PlayerSettings.Android.keystoreName = "C:/Users/Kiril/Dropbox/archive/android keys/fingerFighters.keystore";
            PlayerSettings.Android.keyaliasName = "fingerfighters";
            PlayerSettings.Android.keyaliasPass = PlayerSettings.Android.keystorePass = "PASS"; // IMPR show window 

            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30; // FIXME doesn't work for some reason 
            
            BuildPipeline.BuildPlayer(options);
        }
    }
}