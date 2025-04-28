using UnityEngine;

public enum EnemyType
{
    Normal, Elite, Boss
}

public enum EnemyTribe
{
    Slime
}

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public EnemyTribe tribe;
}
