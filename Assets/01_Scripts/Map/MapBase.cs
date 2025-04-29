using UnityEngine;

public enum MapType
{
    NormalCombat, EliteCombat, BossCombat, Reward
}

public class MapBase : MonoBehaviour
{
    public EnemySpawnpoint[] enemySpawnpoints;
    public Transform playerSpawnpoint;

    public int maxTurnCount = 1;
    public int currentTurnCount = 0;

    private int maxEnemyCount;
    public int currentEnemyCount;

    private void Awake()
    {
        maxEnemyCount = enemySpawnpoints.Length;
    }

    public void SpawnEnemies(float delaySecond)
    {
        if(currentTurnCount < maxTurnCount)
        {
            Invoke("InvokedSpawnEnemies", delaySecond);
        }
    }

    private void InvokedSpawnEnemies()
    {
        currentTurnCount++;
        currentEnemyCount = maxEnemyCount;

        foreach (EnemySpawnpoint spawnpoint in enemySpawnpoints)
        {
            spawnpoint.EnemySpawn();
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
