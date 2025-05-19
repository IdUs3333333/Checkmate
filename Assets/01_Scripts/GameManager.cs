using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroupPanel gameOverPanel;

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

    public void GetScore(int score)
    {
        gameScore += score;
    }

    public void GameOver()
    {
        gameOverPanel.Open();
        Time.timeScale = 0;
    }

    public void RoomCleared()
    {
        clearedRoomCount++;
        MapGenerator.Instance.GeneratePortal();
    }

    public void PortalInteract(MapType type)
    {
        MapGenerator.Instance.GenerateMap(type);
    }
}
