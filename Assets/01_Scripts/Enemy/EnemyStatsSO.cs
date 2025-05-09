using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("기본 수치")]
    public float HP;
    public float moveSpeed;

    [Header("공격 관련 수치")]
    public float attackDelay;
    public float skillDelay;
    public int attackDamage;
    public int skillDamage;

    [Header("FSM 관련 수치")]
    public float detectRange;
    public float attackRange;
}
