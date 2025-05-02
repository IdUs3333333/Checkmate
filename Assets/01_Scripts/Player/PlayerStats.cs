using System.Collections.Generic;
using UnityEngine;

public enum ChessType
{
    Pawn, Bishop, Knight, Rook, Queen, King
}

public enum PlayerReinforces
{

}

public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public ChessType chessType;

    public List<GameObject> reinforcements;

    [HideInInspector] public float[] attackRange = { 0.5f, 2.25f, 1.25f, 2.5f, 3.75f, 1f };

    public void AddReinforces(PlayerReinforces reinforcement)
    {
        // reinforcements ����Ʈ�� reinforce �߰��ϱ�
        // ���ǹ� �ؼ� ��ȭ / �߰� ������
    }
}
