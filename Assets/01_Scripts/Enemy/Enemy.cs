using System;
using UnityEngine;

public enum EnemyType
{
    Normal, Elite, Boss
}

public enum EnemyTribe
{
    Slime, Skelleton, Robot
}

public enum EnemyState
{
    Idle, Chase, Attack, Skill, Die
}

public class Enemy : MonoBehaviour
{
    private MapBase currentMap;

    public EnemyType type;
    public EnemyTribe tribe;
    public EnemyState state;
    
    public float maxHP;
    public float currentHP;

    private bool dieTrigger = false;

    private void Awake()
    {
        currentHP = maxHP;
        currentMap = FindFirstObjectByType<MapBase>();
        state = EnemyState.Idle;
    }

    public void Damage(float value)
    {
        currentHP = Mathf.Clamp(currentHP - value, 0f, maxHP);
    }

    public void Heal(float value)
    {
        currentHP = Mathf.Clamp(currentHP + value, 0f, maxHP);
    }

    protected void Update()
    {
        if(currentHP <= 0 && !dieTrigger)
        {
            if(TryGetComponent(out EliteSlime entity))
            {
                Debug.Log($"Died - {entity.name}");
            }

            if (currentMap == null) currentMap = FindFirstObjectByType<MapBase>();
            currentMap.OnEnemyDeath();
            dieTrigger = !dieTrigger;
            Destroy(gameObject);
            return;
        }

        switch(state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Skill:
                break;
            case EnemyState.Die:
                break;
        }
    }
}
