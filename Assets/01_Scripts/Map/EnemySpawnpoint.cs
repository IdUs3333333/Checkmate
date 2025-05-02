using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public List<Enemy> enemy;

    public void EnemySpawn(int turn)
    {
        Instantiate(enemy[turn - 1], transform.position, Quaternion.identity);
    }
}
