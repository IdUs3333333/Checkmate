using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform impactPoint;

    private Camera mainCam;
    private Collider2D impactCollider;

    private bool isAttacking = false;
    private float attackRange = 0f;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        mainCam = Camera.main;
        impactCollider = impactPoint.GetComponent<Collider2D>();
    }

    public void Attack()
    {
        Debug.Log("Attack Check");
        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        attackRange = player.stats.attackRange[(int)player.type];

        switch(player.type)
        {
            case ChessType.Pawn:
                Vector2 dir = (mainCam.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
                Vector2 pos = (Vector2)player.transform.position + dir * attackRange;
                transform.position = pos;
                break;

            case ChessType.Bishop:
                break;

            case ChessType.Knight:
                break;

            case ChessType.Rook:
                break;

            case ChessType.Queen:
                break;

            case ChessType.King:
                break;
        }
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    public void SubSkill()
    {

    }

    public void MainSkill()
    {

    }
}
