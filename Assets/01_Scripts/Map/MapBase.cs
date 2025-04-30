using UnityEngine;
using System.Linq;

public class MapBase : MonoBehaviour
{
    public EnemySpawnpoint[] enemySpawnpoints;
    public Transform playerSpawnpoint;
    public Transform portalSpawnpoint;

    public int maxTurnCount = 1;
    public int currentTurnCount = 0;

    private int maxEnemyCount;
    public int currentEnemyCount;

    private void Awake()
    {
        maxEnemyCount = enemySpawnpoints.Length;

        foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
        {
            enemySpawnpoints.Append(point);
        }
    }

    public void SpawnEnemies(float delaySecond)
    {
        if(currentTurnCount < maxTurnCount)
        {
            Invoke("InvokedSpawnEnemies", delaySecond);
        }
        else
        {
            GameManager.Instance.RoomCleared();
        }
    }

    private void InvokedSpawnEnemies()
    {
        currentTurnCount++;
        currentEnemyCount = maxEnemyCount;

        foreach (EnemySpawnpoint point in enemySpawnpoints)
        {
            point.EnemySpawn();
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;

        if(currentEnemyCount <= 0)
        {
            SpawnEnemies(0.5f);
        }
    }
}
