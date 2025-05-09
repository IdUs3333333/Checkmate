using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("�⺻ ��ġ")]
    public float HP;
    public float moveSpeed;

    [Header("���� ���� ��ġ")]
    public float attackDelay;
    public float skillDelay;
    public int attackDamage;
    public int skillDamage;

    [Header("FSM ���� ��ġ")]
    public float detectRange;
    public float attackRange;
}
