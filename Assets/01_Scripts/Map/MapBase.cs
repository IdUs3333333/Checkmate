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

    private bool spawnEnemyTrigger = true;

    private void Awake()
    {
        maxEnemyCount = enemySpawnpoints.Length;
        maxTurnCount = 1;

        foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
        {
            enemySpawnpoints.Append(point);
            maxTurnCount = point.enemy.Count;
        }
    }

    public void SpawnEnemies(float delaySecond)
    {
        Debug.Log($"<color=#7777FF>currentTurnCount</color> : <color=#85B6FF>{currentTurnCount}</color>");
        Debug.Log($"<color=#7777FF>maxTurnCount</color> : <color=#85B6FF>{maxTurnCount}</color>");

        if (currentTurnCount < maxTurnCount && spawnEnemyTrigger)
        {
            Invoke("InvokedSpawnEnemies", delaySecond);
        }
        else if(currentTurnCount >= maxTurnCount)
        {
            spawnEnemyTrigger = false;
            currentTurnCount = 0;
            GameManager.Instance.RoomCleared();
        }
    }

    private void InvokedSpawnEnemies()
    {
        currentTurnCount++;
        currentEnemyCount = maxEnemyCount;

        foreach (EnemySpawnpoint point in enemySpawnpoints)
        {
            point.EnemySpawn(currentTurnCount);
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
