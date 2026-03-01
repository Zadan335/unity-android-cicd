using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string buildPath = "Builds/Android";
        Directory.CreateDirectory(buildPath);

        // Ensure at least one scene exists
        string scenePath = "Assets/Scenes/DemoScene.unity";
        if (!File.Exists(scenePath))
        {
            Directory.CreateDirectory("Assets/Scenes");
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
            EditorSceneManager.SaveScene(scene, scenePath);
        }

        string[] scenes = { scenePath };

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
