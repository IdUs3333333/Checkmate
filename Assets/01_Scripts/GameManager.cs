using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroup GameOverPanel;

    public int gameScore = 0;
    public int maxGameScore = 0;

    public int clearedRoomCount = 0;

    public Player player;

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

        player = FindFirstObjectByType<Player>();
        GameOverPanel.Close();
        Time.timeScale = 1;
    }

    public void GetScore(int score)
    {
        gameScore += score;
    }

    public void GameOver()
    {
        GameOverPanel.Open();
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
