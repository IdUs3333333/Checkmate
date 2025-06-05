using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroupPanel gameOverPanel;
    public CanvasGroupPanel gameClearPanel;
    public CanvasGroupPanel gamePausePanel;
    public CurrentInfo info;

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

        gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<CanvasGroupPanel>();
        gamePausePanel = GameObject.Find("GamePausePanel").GetComponent<CanvasGroupPanel>();
        gameClearPanel = GameObject.Find("GameClearPanel").GetComponent<CanvasGroupPanel>();
        info = GameObject.Find("CurrentInfo").GetComponent<CurrentInfo>();

        player = FindFirstObjectByType<Player>();
        gameOverPanel.Close();

        gameScore = 0;
        clearedRoomCount = 0;
        currentFloor = 1;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePausePanel.isOpened);
        }
    }

    public void MapReset(MapBase currentMap)
    {
        if (info == null) info = GameObject.Find("CurrentInfo").GetComponent<CurrentInfo>();
        info.SetInfo();

        if (player == null) player = FindFirstObjectByType<Player>();
        player.transform.SetPositionAndRotation(currentMap.playerSpawnpoint.position, Quaternion.identity);
    }

    public void GetScore(int score)
    {
        gameScore += score;
    }

    public void PauseGame(bool value = true)
    {
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

    public void GenerateEvent()
    {

    }

    public void GenerateReward()
    {

    }

    public void GameOver()
    {
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
        gameClearPanel.Open();
        Time.timeScale = 0;
    }
}
