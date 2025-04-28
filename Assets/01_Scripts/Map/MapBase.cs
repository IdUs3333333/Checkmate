using UnityEngine;

public enum MapType
{
    NormalCombat, EliteCombat, BossCombat, Reward
}

public class MapBase : MonoBehaviour
{
    public Transform playerSpawnpoint;

    public int SpawnNormalEnemyCount;
    public int SpawnEliteEnemyCount;
    public int SpawnBossEnemyCount;
    public int TurnCount = 1;
}
