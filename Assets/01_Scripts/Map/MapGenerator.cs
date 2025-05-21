using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private SerializedDictionary<MapType, List<MapBase>> maps = new SerializedDictionary<MapType, List<MapBase>>();

    [SerializeField] private SerializedDictionary<int, List<MapType>> easyMapList = new SerializedDictionary<int, List<MapType>>();
    [SerializeField] private SerializedDictionary<int, List<MapType>> hardMapList = new SerializedDictionary<int, List<MapType>>();

    private Dictionary<int, List<MapType>> currentMapList;

    public MapBase currentMap;
    public Portal mapPortal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateMap(MapType.StartRoom);
    }

    public void GenerateMap(MapType type)
    {
        Debug.Log("Generating Map");
        int n = Random.Range(0, maps[type].Count);
        currentMap = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity);

        GameManager.Instance.player.transform.SetPositionAndRotation(currentMap.playerSpawnpoint.position, Quaternion.identity);
        currentMap.SpawnEnemies(0.5f);
    }

    public void OnRoomClear()
    {
        int floor = GameManager.Instance.currentFloor;
        GeneratePortal(floor);

    }

    public void GeneratePortal(int floor)
    {
        switch(GameManager.Instance.difficulty)
        {
            case Difficulty.Easy:
                if (floor == 10)
                {
                    GameManager.Instance.GameClear();
                    return;
                }
                
                List<MapType> easyNextMaps = easyMapList[floor];
                if(easyNextMaps.Count == 1)
                {
                    MapType nextMap = easyNextMaps[0];
                    SpawnPortal(nextMap);
                }
                else
                {
                    MapType nextMap1 = easyNextMaps[Random.Range(0, easyNextMaps.Count)];
                    MapType nextMap2 = easyNextMaps[Random.Range(0, easyNextMaps.Count)];

                    SpawnPortal(nextMap1, -1.5f);
                    SpawnPortal(nextMap2, 1.5f);
                }

                break;
            case Difficulty.Hard:
                if (floor == 20)
                {
                    GameManager.Instance.GameClear();
                    return;
                }

                List<MapType> hardNextMaps = hardMapList[floor];
                if (hardNextMaps.Count == 1)
                {
                    MapType nextMap = hardNextMaps[0];
                    SpawnPortal(nextMap);
                }
                else
                {
                    MapType nextMap1 = hardNextMaps[Random.Range(0, hardNextMaps.Count)];
                    MapType nextMap2 = hardNextMaps[Random.Range(0, hardNextMaps.Count)];

                    SpawnPortal(nextMap1, -1.5f);
                    SpawnPortal(nextMap2, 1.5f);
                }

                break;
        }
    }

    public void SpawnPortal(MapType type, float offset = 0f)
    {
        Portal portal = Instantiate(mapPortal, currentMap.portalSpawnpoint.position
            + new Vector3(offset, 0, 0), Quaternion.identity);
        portal.Init();
    }
}
