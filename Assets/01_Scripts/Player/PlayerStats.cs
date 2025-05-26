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

    [HideInInspector] public float[] attackRange = { 2f, 4.5f, 2.5f, 5f, 7.5f, 2f };

    public void AddReinforces(PlayerReinforces reinforcement)
    {
        // reinforcements 리스트에 reinforce 추가하기
        // 조건문 해서 강화 / 추가 나누기
    }
}
