using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroupPanel gameOverPanel;
    public CanvasGroupPanel gameClearPanel;
    public CanvasGroupPanel gamePausePanel;

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

        player = FindFirstObjectByType<Player>();
        gameOverPanel.Close();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePausePanel.isOpened);
        }
    }

    public void GetScore(int score)
    {
        gameScore += score;
    }

    public void PauseGame(bool value = true)
    {
        if (value) gamePausePanel.Open();
        else gamePausePanel.Close();
    }

    public void GameOver()
    {
        gameOverPanel.Open();
        Time.timeScale = 0;
    }

    public void RoomCleared()
    {
        clearedRoomCount++;
        MapGenerator.Instance.OnRoomClear();
    }

    public void GameClear()
    {
        gameClearPanel.Open();
        Time.timeScale = 0;
    }

    public void PortalInteract(MapType type)
    {
        Debug.Log($"<color=#FFFF77>mapType</color> : <color=#FFFF77>{type}</color>");
        MapGenerator.Instance.GenerateMap(type);
    }
}
