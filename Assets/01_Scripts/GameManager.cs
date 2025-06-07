using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroupPanel gameOverPanel;
    public CanvasGroupPanel gameClearPanel;
    public CanvasGroupPanel gamePausePanel;
    public CanvasGroupPanel reinforcementPanel;
    public ReinforcementPanel reinforcePanel;
    public CurrentInfo info;

    public Image fadePanel;
    public float fadeDuration = 1f;

    public Difficulty difficulty;

    public int gameScore = 0;
    public int maxGameScore = 0;

    public int clearedRoomCount = 0;
    public int currentFloor = 1;

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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePausePanel.isOpened);
        }
    }

    public void MapReset(bool resetScore = false)
    {
        player = FindFirstObjectByType<Player>();

        gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<CanvasGroupPanel>();
        gameOverPanel.Close();

        gamePausePanel = GameObject.Find("GamePausePanel").GetComponent<CanvasGroupPanel>();
        gamePausePanel.Close();

        gameClearPanel = GameObject.Find("GameClearPanel").GetComponent<CanvasGroupPanel>();
        gameClearPanel.Close();

        reinforcementPanel = GameObject.Find("ReinforcementPanel").GetComponent<CanvasGroupPanel>();
        reinforcementPanel.Close();
        reinforcePanel = reinforcementPanel.GetComponent<ReinforcementPanel>();

        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();

        info = GameObject.Find("CurrentInfo").GetComponent<CurrentInfo>();
        info.SetInfo();

        if (resetScore)
        {
            gameScore = 0;
            clearedRoomCount = 0;
            currentFloor = 1;
        }
    }

    public void GetScore(int score)
    {
        gameScore += score;
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
        reinforcePanel.Init();
        reinforcementPanel.Open();
        Time.timeScale = 0f;
    }

    public void CloseReinforceUI()
    {
        reinforcementPanel.Close();
        Time.timeScale = 1f;
    }

    public void GenerateEvent()
    {

    }

    public void GenerateReward()
    {

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
        if(type == MapType.StartRoom)
        {
            clearedRoomCount++;
            currentFloor = clearedRoomCount + 1;
            MapGenerator.Instance.OnRoomClear(true);
        }
        else
        {
            clearedRoomCount++;
            currentFloor = clearedRoomCount + 1;
            MapGenerator.Instance.OnRoomClear();
        }
    }

    public void GameClear()
    {
        if (gameClearPanel == null)
        {
            gameClearPanel = GameObject.Find("GameClearPanel").GetComponent<CanvasGroupPanel>();
        }

        gameClearPanel.Open();
        Time.timeScale = 0;
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
}
