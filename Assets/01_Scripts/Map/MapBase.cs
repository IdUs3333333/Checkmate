using UnityEngine;

public enum MapType
{
    NormalCombat, EliteCombat, BossCombat, Reward
}

public class MapBase : MonoBehaviour
{
    public EnemySpawnpoint[] enemySpawnpoints;
    public Transform playerSpawnpoint;

    public int SpawnNormalEnemyCount;
    public int SpawnEliteEnemyCount;
    public int SpawnBossEnemyCount;
    public int TurnCount = 1;

    public void SpawnEnemies(float delaySecond)
    {
        Invoke("InvokedSpawnEnemies", delaySecond);
    }

    private void InvokedSpawnEnemies()
    {
        foreach (EnemySpawnpoint spawnpoint in enemySpawnpoints)
        {
            spawnpoint.EnemySpawn();
        }
    }
}
