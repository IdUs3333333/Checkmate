using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CanvasGroup GameOverPanel;

    public int gameScore;
    public int maxGameScore;

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
}
