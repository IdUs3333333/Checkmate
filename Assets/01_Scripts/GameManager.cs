using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SaveSystem.Manager;
using SaveSystem.Data;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroupPanel gameOverPanel;
    public CanvasGroupPanel gameClearPanel;
    public TextMeshProUGUI gameClearScoreText;
    public CanvasGroupPanel gamePausePanel;
    public CanvasGroupPanel reinforcementPanel;
    public ReinforcementPanel reinforcePanel;
    public CanvasGroupPanel mysteryEventPanel;
    public MysteryPanel mysteryPanel;
    public CurrentInfo info;

    public Image fadePanel;
    public float fadeDuration = 1f;

    public Difficulty difficulty;

    public int gameScore = 0;
    public int maxGameScore = 0;

    public float playTime;

    public int clearedRoomCount = 0;
    public int currentFloor = 0;

    public Player player;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MapReset(true);

        Time.timeScale = 1;
        Fade();
    }

    private void Update()
    {
        playTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePausePanel.isOpened);
        }
    }

    public void GetScore(int score)
    {
        gameScore += score;
        gameScore = Mathf.Clamp(gameScore, 0, gameScore);
    }

    public void MapReset(bool resetScore = false)
    {
        DataManager.Instance.LoadData();

        player = FindFirstObjectByType<Player>();

        gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<CanvasGroupPanel>();
        gameOverPanel.Close();

        gamePausePanel = GameObject.Find("GamePausePanel").GetComponent<CanvasGroupPanel>();
        gamePausePanel.Close();

        gameClearPanel = GameObject.Find("GameClearPanel").GetComponent<CanvasGroupPanel>();
        gameClearPanel.Close();

        gameClearScoreText = GameObject.Find("RecordText").GetComponent<TextMeshProUGUI>();

        reinforcementPanel = GameObject.Find("ReinforcementPanel").GetComponent<CanvasGroupPanel>();
        reinforcePanel = reinforcementPanel.GetComponent<ReinforcementPanel>();
        reinforcementPanel.Close();

        mysteryEventPanel = GameObject.Find("MysteryPanel").GetComponent<CanvasGroupPanel>();
        mysteryPanel = mysteryEventPanel.GetComponent<MysteryPanel>();
        mysteryEventPanel.Close();

        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0f);

        info = GameObject.Find("CurrentInfo").GetComponent<CurrentInfo>();
        info.SetInfo();

        if (resetScore)
        {
            gameScore = 0;
            clearedRoomCount = -1;
            currentFloor = 0;
        }
    }

    public void PauseGame(bool value = true)
    {
        if(gamePausePanel == null)
        {
            gamePausePanel = GameObject.Find("GamePausePanel").GetComponent<CanvasGroupPanel>();
        }

        if (value)
        {
            Time.timeScale = 0;
            gamePausePanel.Open();
        }
        else
        {
            Time.timeScale = 1;
            gamePausePanel.Close();
        }
    }

    public void OpenReinforceUI()
    {
        GetScore(3);

        if (reinforcePanel.available.Count == 0)
        {
            MapGenerator.Instance.currentMap.OnEntityDestroy();
            Destroy(player.currentChest.gameObject);
            return;
        }

        reinforcePanel.Init();
        reinforcementPanel.Open();
    }

    public void CloseReinforceUI()
    {
        MapGenerator.Instance.currentMap.OnEntityDestroy();
        Destroy(player.currentChest.gameObject);
        reinforcementPanel.Close();
    }

    public void OpenEventUI()
    {
        GetScore(3);
        mysteryPanel.Init();
        mysteryEventPanel.Open();
    }

    public void CloseEventUI()
    {
        MapGenerator.Instance.currentMap.OnEntityDestroy();
        Destroy(player.currentStatue.gameObject);
        mysteryEventPanel.Close();
    }

    public void GameOver()
    {
        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<CanvasGroupPanel>();
        }

        gameOverPanel.Open();
        Time.timeScale = 0;
    }

    public void RoomCleared(MapType type)
    {
        MapGenerator.Instance.OnRoomClear((type == MapType.StartRoom) ? true : false);
    }

    public void GameClear()
    {
        if (gameClearPanel == null)
        {
            gameClearPanel = GameObject.Find("GameClearPanel").GetComponent<CanvasGroupPanel>();
        }

        GetScore(10);
        gameClearPanel.Open();
        Time.timeScale = 0;

        int highScore = DataManager.Instance.GetHighScore(difficulty);
        bool isNewHighScore = highScore < gameScore;
        if (isNewHighScore) DataManager.Instance.TrySetHighScore(difficulty, gameScore);

        float highTimeRecord = DataManager.Instance.GetPlayTime(difficulty);
        bool isNewPlayTime = highTimeRecord > playTime;
        if (isNewPlayTime) DataManager.Instance.TrySetPlayTime(difficulty, playTime);

        float targetTime = isNewPlayTime ? playTime : highTimeRecord;
        string timeRecordText = string.Format("{0:00}:{1:00}:{2:00}.{3:0}",
            (int)(targetTime / 3600), (int)((targetTime % 3600) / 60),
            (int)(targetTime % 60), (int)((targetTime * 10) % 10));

        gameClearScoreText.text = "BEST SCORE" + (isNewHighScore ? " (NEW!)" : "")
            + $"\n{(isNewHighScore ? gameScore : highScore)}"
            + "\n\nBEST TIME RECORD" + (isNewPlayTime ? " (NEW!)" : "")
            + $"\n{(timeRecordText)}";

        DataManager.Instance.SaveData();
    }

    public void Fade(bool isIn = true)
    {
        if (isIn)
        {
            StartCoroutine(FadeIn());
        }
        else StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        Color c = fadePanel.color;
        fadePanel.color = new Color(c.r, c.g, c.b, 1f);
        yield return fadePanel.DOFade(0f, fadeDuration).SetEase(Ease.InSine).WaitForCompletion();
    }

    private IEnumerator FadeOut()
    {
        Color c = fadePanel.color;
        fadePanel.color = new Color(c.r, c.g, c.b, 0f);
        yield return fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.OutSine).WaitForCompletion();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData();
    }
}
