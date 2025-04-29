using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    public MapBase[] combatMapsInput;
    public MapBase[] bossMapsInput;
    public MapBase[] rewardMapsInput;

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
        combatMaps = combatMapsInput;
        bossMaps = bossMapsInput;
        rewardMaps = rewardMapsInput;
    }

    public void GenerateMap(MapType type)
    {
        Debug.Log("Generating Map");
        int n = Random.Range(0, maps[type].Length);
        MapBase map = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity);

        GameManager.Instance.player.transform.SetPositionAndRotation(map.playerSpawnpoint.position, Quaternion.identity);
        map.SpawnEnemies(0.5f);
    }
}
