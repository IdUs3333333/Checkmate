using System.Collections;
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
        Debug.Log("Loading Scene...");
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadScene(int sceneIdx)
    {
        Debug.Log("Loading Scene...");
        SceneManager.LoadScene(sceneIdx);
    }

    public static void ReloadScene(MonoBehaviour caller, bool withAnimation = true)
    {
        Debug.Log("Reloading Scene...");
        string name = SceneManager.GetActiveScene().name;
        if(withAnimation)
        {
            caller.StartCoroutine(LoadWithAnimationCoroutine(name));
        }
        else SceneManager.LoadScene(name);
    }

    private static IEnumerator LoadWithAnimationCoroutine(string name)
    {
        SceneManager.LoadScene(loading);

        yield return new WaitUntil(() => Loading.Instance != null);

        Loading.Instance.StartLoad(name);
    }

    public static void LoadSceneWithAnimation(string sceneName)
    {
        Debug.Log("Loading Scene with Animations...");
        Loading.Instance.StartLoad(sceneName);
    }
}
