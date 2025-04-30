using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Loading : MonoBehaviour
{
    public static Loading Instance;

    [SerializeField] private Slider loadSlider;
    [SerializeField] private GameObject decorationObject;
    [SerializeField] private TextMeshProUGUI loadingTipText;

    private string[] loadingTips = 
    {
        "표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절" +
        "\n표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절" +
        "\n표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절표절",
        "이 게임은 메인 개발자가 개발 전공 학생이 아닙니다!",
        "게임 콘텐츠 제작은 망했습니다... 정말인가요?",
        "적 FSM 코드는 이 팀의 개발자가 만들지 않았습니다 :)",
        "여러분도 명일방주를 플레이해보세요!",
        "사실 이 게임은 로그라이\"크\" 게임입니다!",
        "여러분은 STA+C 대회를 아시나요?",
        "체스는 재밌습니다!",
        "이 게임은 고등학교 3학년 4명이서 만들었습니다!",
        "팀원으로는 개발(QA) 1명, 아트 2명, 기획 1명이 있습니다.",
        "이펙트 만들기는 재밌습니다!",
        "메인 개발자는 사실 픽셀 아티스트입니다. 짜잔!"
    };

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
        LoadingTips();
    }

    public void LoadingDecoration()
    {
        decorationObject.transform.DOComplete();
        decorationObject.transform.DORotate(decorationObject.transform.rotation.eulerAngles + new Vector3(0, 0, -90), 0.625f).SetEase(Ease.OutBack);
        Invoke("LoadingDecoration", 1.25f);
    }

    private void LoadingTips()
    {
        int idx = Random.Range(0, loadingTips.Length);
        loadingTipText.text = (idx == 0 ? "" : "Tip! ") + loadingTips[idx];
        Invoke("LoadingTips", 3.75f);
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
