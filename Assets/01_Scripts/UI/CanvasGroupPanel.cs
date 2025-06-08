using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGroupPanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button returnButton;

    private CanvasGroup canvas;

    public bool isOpened = false;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();

        resumeButton?.onClick.AddListener(Resume);
        replayButton?.onClick.AddListener(Replay);
        returnButton?.onClick.AddListener(Return);

        Active(false);
    }

    public void Open()
    {
        Active(true);
    }

    public void Close()
    {
        Active(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Active(false);
    }

    public void Replay()
    {
        Time.timeScale = 1;
        StartCoroutine(ReplayCoroutine(SE.ingame));
    }

    public void Return()
    {
        Time.timeScale = 1;
        StartCoroutine(ReplayCoroutine(SE.lobby));
    }

    private IEnumerator ReplayCoroutine(string sceneName)
    {
        Color c = GameManager.Instance.fadePanel.color;
        GameManager.Instance.fadePanel.color = new Color(c.r, c.g, c.b, 0f);
        yield return GameManager.Instance.fadePanel.DOFade(1f, GameManager.Instance.fadeDuration).SetEase(Ease.OutSine).WaitForCompletion();

        yield return new WaitForSeconds(GameManager.Instance.fadeDuration);

        SE.nextScene = sceneName;
        SceneManager.LoadScene(SE.loading);
    }

    private void Active(bool value = true)
    {
        if (canvas == null) canvas = GetComponent<CanvasGroup>();

        isOpened = value;

        canvas.alpha = value ? 1 : 0;
        canvas.interactable = value;
        canvas.blocksRaycasts = value;
    }
}
