using System;
using UnityEngine;

public enum EnemyType
{
    Normal, Elite, Boss
}

public enum EnemyTribe
{
    Slime
}

public class Enemy : MonoBehaviour
{
    private MapBase currentMap;

    public EnemyType type;
    public EnemyTribe tribe;
    
    public float maxEnemyHP;
    public float currentEnemyHP;

    private void Awake()
    {
        currentEnemyHP = maxEnemyHP;
        currentMap = FindFirstObjectByType<MapBase>();
    }

    public void Damage(float value)
    {
        currentEnemyHP = Mathf.Clamp(currentEnemyHP - value, 0f, maxEnemyHP);
    }

    public void Heal(float value)
    {
        currentEnemyHP = Mathf.Clamp(currentEnemyHP + value, 0f, maxEnemyHP);
    }

    private void Update()
    {
        if(currentEnemyHP <= 0)
        {
            currentMap.OnEnemyDeath();
            Destroy(gameObject);
        }
    }
}
