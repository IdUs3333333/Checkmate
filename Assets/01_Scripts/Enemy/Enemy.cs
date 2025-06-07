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
    [SerializeField] private GameObject dieEffect;

    private MapBase currentMap;

    public EnemyType type;
    public EnemyTribe tribe;
    public EnemyState state;
    
    public float maxHP;
    public float currentHP;

    private bool dieTrigger = false;

    private void Awake()
    {
        ApplyDifficultyScaling();
        currentHP = maxHP;
        currentMap = FindFirstObjectByType<MapBase>();
        state = EnemyState.Idle;
    }
    
    private void ApplyDifficultyScaling()
    {
        int floor = GameManager.Instance.currentFloor;
        float multiplier = 1f + 0.15f * (floor - 1);
        maxHP = Mathf.RoundToInt(maxHP * multiplier);
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
            if (currentMap == null) currentMap = FindFirstObjectByType<MapBase>();
            currentMap.OnEnemyDeath();
            dieTrigger = !dieTrigger;
            Instantiate(dieEffect, transform.position, Quaternion.identity);
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
