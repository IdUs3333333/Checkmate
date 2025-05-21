using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player player;

    private Camera mainCam;

    private bool isAttacking = false;
    private float attackRange = 0f;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        mainCam = Camera.main;
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
                Debug.Log("Pawn Attack!");
                Vector2 dir = (mainCam.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
                Vector2 pos = (Vector2)player.transform.position + dir * attackRange;
                transform.position = pos;
                break;

            case ChessType.Bishop:
                Debug.Log("Bishop Attack!");
                break;

            case ChessType.Knight:
                Debug.Log("Knight Attack!");
                break;

            case ChessType.Rook:
                Debug.Log("Rook Attack!");
                break;

            case ChessType.Queen:
                Debug.Log("Queen Attack!");
                break;

            case ChessType.King:
                Debug.Log("King Attack!");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
