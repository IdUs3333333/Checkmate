using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SE // Scene Extensions
{
    public static string intro = "IntroScene";
    public static string lobby = "LobbyScene";
    public static string ingame = "IngameScene";
    public static string loading = "LoadingScene";
    public static string nextScene = "";

    public static void LoadScene(string sceneName)
    {
        if (sceneName == loading) return;

        nextScene = sceneName;
        Debug.Log($"Loading Scene... {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int sceneIdx)
    {
        if (SceneManager.GetSceneAt(sceneIdx).name == loading) return;

        nextScene = SceneManager.GetSceneAt(sceneIdx).name;
        Debug.Log("Loading Scene...");
        SceneManager.LoadScene(sceneIdx);
    }

    public static void ReloadScene(bool withAnimation = true)
    {
        Debug.Log("Reloading Scene...");
        string name = SceneManager.GetActiveScene().name;
        nextScene = name;

        if(withAnimation)
        {
            LoadSceneWithAnimation(name);
        }
        else SceneManager.LoadScene(name);
    }

    public static void LoadSceneWithAnimation(string sceneName)
    {
        nextScene = sceneName;
        Debug.Log($"Loading Scene With Animation... {sceneName}");
        SceneManager.LoadScene(loading);
    }
}
