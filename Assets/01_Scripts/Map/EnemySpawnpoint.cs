using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public List<Enemy> enemy;

    public void EnemySpawn(int turn)
    {
        if (enemy.Count < turn)
        {
            return;
        }
        if(enemy[Mathf.Clamp(turn - 1, 0, turn - 1)] != null)
        {
            Instantiate(enemy[Mathf.Clamp(turn - 1, 0, turn - 1)], transform.position, Quaternion.identity);
        }
    }
}
