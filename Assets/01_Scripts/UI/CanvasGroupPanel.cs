using UnityEngine;
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
        SE.LoadSceneWithAnimation(SE.ingame);
    }

    public void Return()
    {
        SE.LoadSceneWithAnimation(SE.lobby);
    }

    private void Active(bool value = true)
    {
        isOpened = value;

        canvas.alpha = value ? 1 : 0;
        canvas.interactable = value;
        canvas.blocksRaycasts = value;
    }
}
