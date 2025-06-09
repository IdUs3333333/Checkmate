using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private SerializedDictionary<MapType, List<MapBase>> maps = new SerializedDictionary<MapType, List<MapBase>>();

    [SerializeField] private SerializedDictionary<int, List<MapType>> easyMapList = new SerializedDictionary<int, List<MapType>>();
    [SerializeField] private SerializedDictionary<int, List<MapType>> hardMapList = new SerializedDictionary<int, List<MapType>>();

    public MapBase currentMap;
    public Portal mapPortal;

    public MapType currentMapType = MapType.StartRoom;
    public Difficulty currentDifficulty = Difficulty.Easy;

    public bool isPortalSpawned = false;

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
        GameManager.Instance.MapReset(true);
        GenerateMap(MapType.StartRoom);
    }

    public void GenerateMap(MapType type)
    {
        if (currentMap != null)
        {
            Destroy(currentMap.gameObject);
        }
        isPortalSpawned = false;

        Debug.Log("<color=#777777>Generating Map...</color>");

        GameManager.Instance.clearedRoomCount++;
        GameManager.Instance.currentFloor = GameManager.Instance.clearedRoomCount + 1;
        currentMapType = type;
        int n = Random.Range(0, maps[type].Count);
        currentMap = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity);
        currentMap.Init(currentMapType);

        GameManager.Instance.MapReset();
        GameManager.Instance.player.transform.SetPositionAndRotation
            (currentMap.playerSpawnpoint.position, Quaternion.identity);

        if(currentMap.type != MapType.StartRoom)
        {
            currentMap.SpawnEntities(0.5f);
        }
    }

    public void OnRoomClear(bool isStart = false)
    {
        if (currentMapType == MapType.BasicCombat || currentMapType == MapType.ExtendedCombat
            || currentMapType == MapType.EliteCombat || currentMapType == MapType.BossCombat)
            GameManager.Instance.GetScore(5);
        GameManager.Instance.GetScore(10);
        GeneratePortal(GameManager.Instance.currentFloor, isStart);
    }

    public void GeneratePortal(int floor, bool isStart)
    {
        if (isPortalSpawned) return;

        if (isStart)
        {
            MapType nextMap1 = easyMapList[floor][Random.Range(0, easyMapList[floor].Count)];
            MapType nextMap2 = hardMapList[floor][Random.Range(0, hardMapList[floor].Count)];

            SpawnPortal(nextMap1, Difficulty.Easy, -1.5f, true);
            SpawnPortal(nextMap2, Difficulty.Hard, 1.5f, true);
        }
        else
        {
            switch (GameManager.Instance.difficulty)
            {
                case Difficulty.Easy:
                    if (floor == 10)
                    {
                        GameManager.Instance.GameClear();
                        return;
                    }

                    List<MapType> easyNextMaps = easyMapList[floor];
                    if (easyNextMaps.Count == 1)
                    {
                        MapType nextMap = easyNextMaps[0];
                        SpawnPortal(nextMap, Difficulty.Easy);
                    }
                    else
                    {
                        MapType nextMap1 = easyNextMaps[Random.Range(0, easyNextMaps.Count)];
                        MapType nextMap2;
                        do
                        {
                            nextMap2 = easyNextMaps[Random.Range(0, easyNextMaps.Count)];
                        }
                        while (nextMap1 == nextMap2);

                        SpawnPortal(nextMap1, Difficulty.Easy, -1.5f);
                        SpawnPortal(nextMap2, Difficulty.Easy, 1.5f);
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
                        SpawnPortal(nextMap, Difficulty.Hard);
                    }
                    else
                    {
                        MapType nextMap1 = hardNextMaps[Random.Range(0, hardNextMaps.Count)];
                        MapType nextMap2;
                        do
                        {
                            nextMap2 = hardNextMaps[Random.Range(0, hardNextMaps.Count)];
                        }
                        while (nextMap1 == nextMap2);

                        SpawnPortal(nextMap1, Difficulty.Hard, -1.5f);
                        SpawnPortal(nextMap2, Difficulty.Hard, 1.5f);
                    }

                    break;
            }
        }

        isPortalSpawned = true;
    }

    public void SpawnPortal(MapType type, Difficulty difficulty, float offset = 0f, bool isStart = false)
    {
        Portal portal = Instantiate(mapPortal, currentMap.portalSpawnpoint.position
            + new Vector3(offset, 0, 0), Quaternion.identity);
        portal.Init(type, difficulty, isStart);
    }
}
