using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    private static MapBase[] combatMaps;
    private static MapBase[] bossMaps;
    private static MapBase[] rewardMaps;
    private Dictionary<MapType, MapBase[]> maps = new Dictionary<MapType, MapBase[]>()
    {
        { MapType.NormalCombat, combatMaps },
        { MapType.EliteCombat, combatMaps },
        { MapType.BossCombat, bossMaps },
        { MapType.Reward, rewardMaps }
    };

    private void Awake()
    {
        GenerateMap(MapType.NormalCombat);
    }

    public void GenerateMap(MapType type)
    {
        Debug.Log("Generated Map");
        int n = Random.Range(0, maps[type].Length);
        MapBase map = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity);

        GameManager.Instance.player.transform.SetPositionAndRotation(map.playerSpawnpoint.position, Quaternion.identity);
        map.SpawnEnemies(0.5f);
    }
}
