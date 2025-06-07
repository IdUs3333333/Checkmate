using System.Collections.Generic;
using UnityEngine;

public enum ChessType
{
    Pawn, Bishop, Knight, Rook, Queen, King
}

public class PlayerStats : MonoBehaviour
{
    public ChessType chessType;

    public int baseHP;
    public float baseAttackDamage;
    public float baseAttackSpeed;
    public float baseMoveSpeed;

    public float baseCritChance;
    public float baseCritMultiplier;
    public float baseEvasionChance;

    public int hp => baseHP + (int)GetBonus(StatType.HP);
    public float attackDamage => baseAttackDamage + GetBonus(StatType.AttackDamage);
    public float attackSpeed => baseAttackSpeed + GetBonus(StatType.AttackSpeed);
    public float moveSpeed => baseMoveSpeed + GetBonus(StatType.MoveSpeed);
    public float critChance => baseCritChance + GetBonus(StatType.CritChance);
    public float critMultiplier => baseCritMultiplier + GetBonus(StatType.CritMultiplier);
    public float evasionChance => baseEvasionChance + GetBonus(StatType.EvasionChance);

    public List<ReinforcementStats> reinforcements;

    [HideInInspector] public float[] baseAttackRange = { 2f, 4.5f, 2.5f, 5f, 7.5f, 2f };

    public void ResetStats()
    {
        reinforcements.Clear();
    }

    public void AddReinforces(PlayerReinforcementSO reinforcement)
    {
        var found = reinforcements.Find(r => r.data == reinforcement);
        if(found != null)
        {
            UpgradeStat(reinforcement);
        }
        else
        {
            reinforcements.Add(new ReinforcementStats
            {
                data = reinforcement,
                level = 1,
                unlocked = true
            });
        }
    }

    public float GetBonus(StatType type)
    {
        float bonus = 0f;
        foreach (var r in reinforcements)
        {
            if (r.unlocked && r.level > 0 && r.data.statType == type)
            {
                int index = Mathf.Clamp(r.level - 1, 0, r.data.values.Length - 1);
                bonus += r.data.values[index];
            }
        }
        return bonus;
    }

    public void UnlockStat(PlayerReinforcementSO so)
    {
        var found = reinforcements.Find(r => r.data == so);
        if (found != null) found.unlocked = true;
        else reinforcements.Add(new ReinforcementStats
        {
            data = so,
            unlocked = true,
            level = 0
        });
    }

    public void UpgradeStat(PlayerReinforcementSO so)
    {
        var found = reinforcements.Find(r => r.data == so);
        if (found != null && found.level < 3)
        {
            found.level++;
            Debug.Log($"{so.displayName} °­È­ ({found.level - 1} ¡æ{found.level})");
        }
    }
}
