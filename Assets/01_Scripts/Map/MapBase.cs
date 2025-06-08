using UnityEngine;
using System.Linq;

public class MapBase : MonoBehaviour
{
    public Spawnpoint[] entitySpawnpoints;
    public Transform playerSpawnpoint;
    public Transform portalSpawnpoint;

    public MapType type = MapType.StartRoom;

    public int maxTurnCount = 1;
    public int currentTurnCount = 0;

    private int maxEntityCount;
    public int currentEntityCount;

    private bool spawnEnemyTrigger = true;

    public void Init(MapType _type)
    {
        type = _type;
        OnGenerate();
    }

    private void OnGenerate()
    {
        Debug.Log($"<color=#5283CC>Current Floor : {GameManager.Instance.currentFloor}F</color>");

        maxEntityCount = entitySpawnpoints.Length;
        maxTurnCount = 1;

        switch(type)
        {
            case MapType.BasicCombat:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 1;
                break;

            case MapType.BossCombat:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 1;
                break;

            case MapType.EliteCombat:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 2;
                break;

            case MapType.ExtendedCombat:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 3;
                break;

            case MapType.Mystery:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 1;
                break;

            case MapType.Reward:
                entitySpawnpoints = transform.GetComponentsInChildren<Spawnpoint>();
                maxTurnCount = 1;
                break;

            case MapType.StartRoom:
                maxTurnCount = 0;
                GameManager.Instance.RoomCleared(MapType.StartRoom);
                break;
        }
    }

    public void SpawnEntities(float delaySecond)
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
        int currentMaxEntityCount = 0;

        foreach (Spawnpoint point in entitySpawnpoints)
        {
            if (point.entity.Count == 0)
            {
                GameManager.Instance.RoomCleared(type);
                return;
            }
            currentMaxEntityCount += point.EnemySpawn(currentTurnCount);
        }
        maxEntityCount = currentMaxEntityCount;
        currentEntityCount = maxEntityCount;
    }

    public void OnEntityDestroy()
    {
        currentEntityCount--;

        if(currentEntityCount <= 0)
        {
            SpawnEntities(0.5f);
        }
    }
}
