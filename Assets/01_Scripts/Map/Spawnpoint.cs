using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public List<GameObject> entity;

    public int EnemySpawn(int turn)
    {
        if (entity.Count < turn)
        {
            return 0;
        }
        if(entity[Mathf.Clamp(turn - 1, 0, turn - 1)] != null)
        {
            Instantiate(entity[Mathf.Clamp(turn - 1, 0, turn - 1)], transform.position, Quaternion.identity);
            return 1;
        }
        return 0;
    }
}
