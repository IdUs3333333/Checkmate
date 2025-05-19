using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private SerializedDictionary<MapType, List<MapBase>> maps = new SerializedDictionary<MapType, List<MapBase>>();

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
        GenerateMap(MapType.BasicCombat);
    }

    public void GenerateMap(MapType type)
    {
        Debug.Log("Generating Map");
        int n = Random.Range(0, maps[type].Count);
        currentMap = Instantiate(maps[type][n], Vector3.zero, Quaternion.identity);

        GameManager.Instance.player.transform.SetPositionAndRotation(currentMap.playerSpawnpoint.position, Quaternion.identity);
        currentMap.SpawnEnemies(0.5f);
    }

    public void GeneratePortal()
    {
        Transform portalPoint = currentMap.portalSpawnpoint;
        
        if(GameManager.Instance.difficulty == Difficulty.Easy)
        {

        }
        else if(GameManager.Instance.difficulty == Difficulty.Normal)
        {

        }
        else if(GameManager.Instance.difficulty == Difficulty.Hard)
        {

        }

        Portal portal1 = Instantiate(mapPortal, portalPoint.position - new Vector3(1.5f, 0, 0), Quaternion.identity);
        Portal portal2 = Instantiate(mapPortal, portalPoint.position + new Vector3(1.5f, 0, 0), Quaternion.identity);

        portal1.Init();
        portal2.Init();
    }
}
