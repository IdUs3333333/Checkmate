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
        "ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��" +
        "\nǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��" +
        "\nǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��ǥ��",
        "�� ������ ���� �����ڰ� ���� ���� �л��� �ƴմϴ�!",
        "���� ������ ������ ���߽��ϴ�... �����ΰ���?",
        "�� FSM �ڵ�� �� ���� �����ڰ� ������ �ʾҽ��ϴ� :)",
        "�����е� ���Ϲ��ָ� �÷����غ�����!",
        "��� �� ������ �α׶���\"ũ\" �����Դϴ�!",
        "�������� STA+C ��ȸ�� �ƽó���?",
        "ü���� ��ս��ϴ�!",
        "�� ������ ����б� 3�г� 4���̼� ��������ϴ�!",
        "�������δ� ����(QA) 1��, ��Ʈ 2��, ��ȹ 1���� �ֽ��ϴ�.",
        "����Ʈ ������ ��ս��ϴ�!",
        "���� �����ڴ� ��� �ȼ� ��Ƽ��Ʈ�Դϴ�. ¥��!"
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
            // ��ȭ �ֱ�
            loadSlider.value = async.progress;

            if (async.progress >= 0.95f)
            {
                // ������
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
