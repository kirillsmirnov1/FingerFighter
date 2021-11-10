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
            // TODO set mono
            // TODO disable keystore 
            // TODO disable arm64 
            // TODO build apk 
            BuildPipeline.BuildPlayer(options);
        }
    }
}