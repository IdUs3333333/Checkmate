using UnityEngine;

public enum Difficulty
{
    Easy, Normal, Hard
}

public enum MapType
{
    BasicCombat, ExtendedCombat, Reward, Mystery, EliteCombat, BossCombat
}

public class ME
{
    public static int[] easyPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };
    public static int[] normalPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };
    public static int[] hardPortalCount = { 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 };

    public static int[] easyEliteCombatNumber = { 5, 10 };
    public static int[] normalEliteCombatNumber = { 5, 10, 15 };
    public static int[] hardEliteCombatNumber = { 5, 10, 20, 25 };
}
