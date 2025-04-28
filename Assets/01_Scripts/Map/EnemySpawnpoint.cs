using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public GameObject enemy;

    public void EnemySpawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
