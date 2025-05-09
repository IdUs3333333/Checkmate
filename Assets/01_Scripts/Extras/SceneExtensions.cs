using UnityEngine;
using UnityEngine.SceneManagement;

public static class SE // Scene Extensions
{
    public static string intro = "IntroScene";
    public static string lobby = "LobbyScene";
    public static string ingame = "IngameScene";
    public static string loading = "LoadingScene";

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadScene(int sceneIdx)
    {
        SceneManager.LoadScene(sceneIdx);
    }

    public static void ReloadScene()
    {
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    public static void LoadSceneWithAnimation(string sceneName)
    {
        Loading.Instance.StartLoad(sceneName);
    }
}
