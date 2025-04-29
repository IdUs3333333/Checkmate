using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int gameScore;
    public int maxGameScore;

    public Player player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    public void GetScore(int score)
    {
        gameScore += score;
    }

    public void GameOver()
    {

    }
}
