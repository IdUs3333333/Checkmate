using UnityEngine;
using System.Linq;

public class MapBase : MonoBehaviour
{
    public EnemySpawnpoint[] enemySpawnpoints;
    public Transform playerSpawnpoint;
    public Transform portalSpawnpoint;

    public MapType type = MapType.StartRoom;

    public int maxTurnCount = 1;
    public int currentTurnCount = 0;

    private int maxEnemyCount;
    public int currentEnemyCount;

    private bool spawnEnemyTrigger = true;

    public void Init(MapType _type)
    {
        type = _type;
        OnGenerate();
    }

    private void OnGenerate()
    {
        Debug.Log($"<color=#5283CC>Current Floor : {GameManager.Instance.currentFloor}F</color>");

        maxEnemyCount = enemySpawnpoints.Length;
        maxTurnCount = 1;

        switch(type)
        {
            case MapType.BasicCombat:
                foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
                {
                    enemySpawnpoints.Append(point);
                    maxTurnCount = point.enemy.Count;
                }
                break;

            case MapType.BossCombat:
                foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
                {
                    enemySpawnpoints.Append(point);
                    maxTurnCount = point.enemy.Count;
                }
                break;

            case MapType.EliteCombat:
                foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
                {
                    enemySpawnpoints.Append(point);
                    maxTurnCount = point.enemy.Count;
                }
                break;

            case MapType.ExtendedCombat:
                foreach (EnemySpawnpoint point in transform.GetComponentsInChildren<EnemySpawnpoint>())
                {
                    enemySpawnpoints.Append(point);
                    maxTurnCount = point.enemy.Count;
                }
                break;

            case MapType.Mystery:
                GameManager.Instance.GenerateEvent();
                GameManager.Instance.RoomCleared(MapType.Mystery);
                break;

            case MapType.Reward:
                GameManager.Instance.GenerateReward();
                GameManager.Instance.RoomCleared(MapType.Reward);
                break;

            case MapType.StartRoom:
                GameManager.Instance.RoomCleared(MapType.StartRoom);
                break;
        }
    }

    public void SpawnEnemies(float delaySecond)
    {

        if (currentTurnCount < maxTurnCount && spawnEnemyTrigger)
        {
            Invoke("InvokedSpawnEnemies", delaySecond);

            Debug.Log($"<color=#7777FF>currentTurnCount</color> : <color=#85B6FF>{currentTurnCount}</color>");
            Debug.Log($"<color=#7777FF>maxTurnCount</color> : <color=#85B6FF>{maxTurnCount}</color>");
        }
        else if(currentTurnCount >= maxTurnCount)
        {
            Debug.Log($"<color=#85B6FF>Room Cleared!</color>" +
                $"<color=#5283CC> - Next Floor : {GameManager.Instance.currentFloor + 1}F</color>");

            spawnEnemyTrigger = false;
            currentTurnCount = 0;
            GameManager.Instance.RoomCleared(type);
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
