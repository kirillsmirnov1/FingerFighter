﻿using System.Linq;
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
            TweakSettings(BuildType.TestApk);
            BuildPipeline.BuildPlayer(PrepareOptions(BuildType.TestApk));
        }

        [MenuItem("Build/Release aab Build")]
        public static void ReleaseBuild()
        {
            TweakSettings(BuildType.ReleaseAab);
            BuildPipeline.BuildPlayer(PrepareOptions(BuildType.ReleaseAab));
        }

        private static void TweakSettings(BuildType buildType)
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, buildType == BuildType.TestApk ? ScriptingImplementation.Mono2x : ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | (buildType == BuildType.TestApk ? AndroidArchitecture.None : AndroidArchitecture.ARM64);
            PlayerSettings.Android.useCustomKeystore = buildType == BuildType.ReleaseAab;
            EditorUserBuildSettings.buildAppBundle = buildType == BuildType.ReleaseAab;

            if (buildType == BuildType.ReleaseAab)
            {
                PlayerSettings.Android.keystoreName = "C:/Users/Kiril/Dropbox/archive/android keys/fingerFighters.keystore";
                PlayerSettings.Android.keyaliasName = "fingerfighters";
                PlayerSettings.Android.keyaliasPass = PlayerSettings.Android.keystorePass = "PASS"; // IMPR show window 

                PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30; 
            }
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