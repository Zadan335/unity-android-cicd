using UnityEditor;
using System.IO;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string buildPath = "Builds/Android";
        Directory.CreateDirectory(buildPath);

        string[] scenes = { }; // Empty for demo

        // Build APK
        EditorUserBuildSettings.buildAppBundle = false;
        BuildPipeline.BuildPlayer(
            scenes,
            buildPath + "/Demo.apk",
            BuildTarget.Android,
            BuildOptions.None
        );

        // Build AAB
        EditorUserBuildSettings.buildAppBundle = true;
        BuildPipeline.BuildPlayer(
            scenes,
            buildPath + "/Demo.aab",
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
