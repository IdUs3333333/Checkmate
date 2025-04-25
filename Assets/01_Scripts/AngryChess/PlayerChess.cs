using UnityEngine;

public class PlayerChess : MonoBehaviour
{
    public float shootSpeed;
    public ChessType chessType;

    public enum ChessType
    {
        Pawn, Bishop, Knight, Rook, Queen, King
    }
}
