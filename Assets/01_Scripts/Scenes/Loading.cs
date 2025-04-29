using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    public static Loading Instance;

    [SerializeField] private Slider loadSlider;
    [SerializeField] private GameObject decorationObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadingDecoration();
    }

    public void LoadingDecoration()
    {
        decorationObject.transform.DOComplete();
        decorationObject.transform.DORotate(decorationObject.transform.rotation.eulerAngles + new Vector3(0, 0, -90), 0.625f).SetEase(Ease.OutBack);
        Invoke("LoadingDecoration", 1.25f);
    }

    public void StartLoad(string sceneName)
    {
        StartCoroutine(Load(sceneName));
    }

    public IEnumerator Load(string nextSceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            // 변화 주기
            loadSlider.value = async.progress;

            if (async.progress >= 0.95f)
            {
                // 마무리
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
