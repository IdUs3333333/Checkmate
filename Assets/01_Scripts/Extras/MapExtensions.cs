using UnityEngine;

public enum Difficulty
{
    Easy, Hard
}

public enum MapType
{
    BasicCombat, ExtendedCombat, Reward, Mystery, EliteCombat, BossCombat, StartRoom
}

public enum StatType
{
    HP, AttackDamage, MoveSpeed, AttackSpeed, CritChance, CritMultiplier, EvasionChance
}

public static class ME
{
    public static int[] easyPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };
    public static int[] normalPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };
    public static int[] hardPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };

    public static int[] easyEliteCombatNumber = { 5, 10 };
    public static int[] normalEliteCombatNumber = { 5, 10, 15 };
    public static int[] hardEliteCombatNumber = { 5, 10, 20, 25 };

    public static bool IsNear(this Vector3 vector, Vector3 target, float distance)
    {
        return Vector3.Distance(vector, target) <= distance;
    }
}
