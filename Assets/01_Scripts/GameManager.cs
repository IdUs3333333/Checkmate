using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int gameScore;
    public int maxGameScore;

    public void GetScore(int score)
    {
        gameScore += score;
    }
}
