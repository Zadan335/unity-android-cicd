using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
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

        // Set Android keystore from environment variables
        string keystoreName = "android.keystore";
        string keystorePass = System.Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PASS");
        string keyAlias = System.Environment.GetEnvironmentVariable("ANDROID_KEY_ALIAS");
        string keyAliasPass = System.Environment.GetEnvironmentVariable("ANDROID_KEY_ALIAS_PASS");

        if (!string.IsNullOrEmpty(keystorePass) && !string.IsNullOrEmpty(keyAliasPass))
        {
            PlayerSettings.Android.keystoreName = keystoreName;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyAliasPass;
        }
        else
        {
            // Skip signing if no secrets provided
            PlayerSettings.Android.keystoreName = "";
            PlayerSettings.Android.keyaliasName = "";
            PlayerSettings.Android.keyaliasPass = "";
        }

        // Build APK
        EditorUserBuildSettings.buildAppBundle = false;
        BuildPipeline.BuildPlayer(
            scenes,
            Path.Combine(buildPath, "Demo.apk"),
            BuildTarget.Android,
            BuildOptions.None
        );

        // Build AAB
        EditorUserBuildSettings.buildAppBundle = true;
        BuildPipeline.BuildPlayer(
            scenes,
            Path.Combine(buildPath, "Demo.aab"),
            BuildTarget.Android,
            BuildOptions.None
        );

        Debug.Log("✅ Android build completed!");
    }
}
