using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    public float HP;
    public float moveSpeed;

    public float attackDelay;
    public float skillDelay;

    public int attackDamage;
    public int skillDamage;

    public float detectRange;
    public float attackRange;
}
