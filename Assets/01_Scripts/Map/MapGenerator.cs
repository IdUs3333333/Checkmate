using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private static GameObject[] combatMaps;
    [SerializeField] private static GameObject[] bossMaps;
    [SerializeField] private static GameObject[] rewardMaps;
    private Dictionary<MapType, GameObject[]> maps = new Dictionary<MapType, GameObject[]>()
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
        MapBase map = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity).GetComponent<MapBase>();
    }
}
