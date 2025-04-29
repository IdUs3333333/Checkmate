using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public Enemy enemy;

    public void EnemySpawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
