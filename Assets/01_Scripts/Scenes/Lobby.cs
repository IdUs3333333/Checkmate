using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using SaveSystem.Manager;
using SaveSystem.Data;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button quitAcceptButton;
    [SerializeField] private Button quitDenyButton;
    [SerializeField] private Button closePanelButton;
    [SerializeField] private Button dataResetButton;

    [Header("Panels")]
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private CanvasGroup quitCheckPanel;
    [SerializeField] private Image fadePanel;

    [Header("Sliders")]
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    [Header("Extras")]
    [SerializeField] private AudioMixer audioMixer;

    private float fadeDuration = 0.5f;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(Quit);
        quitAcceptButton.onClick.AddListener(QuitAccept);
        quitDenyButton.onClick.AddListener(QuitDeny);
        closePanelButton.onClick.AddListener(ClosePanel);
        dataResetButton.onClick.AddListener(DataReset);

        Active(settingsPanel, false);
        Active(quitCheckPanel, false);

        MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        BGMVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        MasterVolumeSlider.value = 1;
        BGMVolumeSlider.value = 1;
        SFXVolumeSlider.value = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Active(settingsPanel, false);
            Active(quitCheckPanel, false);
        }
    }

    private void StartGame()
    {
        StartCoroutine(ReplayCoroutine(SE.ingame));
    }

    private IEnumerator ReplayCoroutine(string sceneName)
    {
        Color c = fadePanel.color;
        fadePanel.color = new Color(c.r, c.g, c.b, 0f);
        yield return fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.OutSine).WaitForCompletion();

        yield return new WaitForSeconds(fadeDuration);

        SE.nextScene = sceneName;
        SceneManager.LoadScene(SE.loading);
    }

    private void Settings()
    {
        if (IsActive(settingsPanel)) Active(settingsPanel, false);
        else Active(settingsPanel, true);
    }

    private void Quit()
    {
        Active(quitCheckPanel, true);
    }

    private void QuitAccept()
    {
        Application.Quit();
    }

    private void QuitDeny()
    {
        Active(quitCheckPanel, false);
    }

    private void SetMasterVolume(float value)
    {
        audioMixer?.SetFloat("Master", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void SetBGMVolume(float value)
    {
        audioMixer?.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void SetSFXVolume(float value)
    {
        audioMixer?.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void ClosePanel()
    {
        Active(settingsPanel, false);
    }

    private void DataReset()
    {
        DataManager.Instance.SetHighScore(Difficulty.Easy, 0);
        DataManager.Instance.SetHighScore(Difficulty.Hard, 0);
        DataManager.Instance.SetPlayTime(Difficulty.Easy, int.MaxValue);
        DataManager.Instance.SetPlayTime(Difficulty.Hard, int.MaxValue);
        DataManager.Instance.SaveData();
    }

    private void Active(CanvasGroup canvas, bool value = true)
    {
        canvas.alpha = value ? 1 : 0;
        canvas.interactable = value;
        canvas.blocksRaycasts = value;
    }

    private bool IsActive(CanvasGroup canvas)
    {
        return canvas.interactable;
    }
}
