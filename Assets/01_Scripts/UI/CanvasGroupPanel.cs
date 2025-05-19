using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupPanel : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button returnButton;

    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();

        replayButton.onClick.AddListener(Replay);
        returnButton.onClick.AddListener(Return);
    }

    public void Open()
    {
        Active(true);
    }

    public void Close()
    {
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
        canvas.alpha = value ? 1 : 0;
        canvas.interactable = value;
        canvas.blocksRaycasts = value;
    }
}
