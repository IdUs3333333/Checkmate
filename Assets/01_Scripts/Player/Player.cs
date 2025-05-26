using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerStats stats;
    public PlayerInput input;
    public PlayerMovement movement;
    public PlayerVisual visual;
    public PlayerHP hp;
    public PlayerAttack attack;

    private Rigidbody2D rb;

    public ChessType type;

    [SerializeField] private float knockBackPower = 600f;
    [SerializeField] private float knockBackTime = 0.125f;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        visual = GetComponentInChildren<PlayerVisual>();
        hp = FindFirstObjectByType<PlayerHP>();
        attack = GetComponentInChildren<PlayerAttack>();

        rb = GetComponent<Rigidbody2D>();

        input.OnAttack += Attack;
        input.OnMainSkill += MainSkill;
        input.OnSubSkill += SubSkill;
    }

    private void Attack()
    {
        attack.Attack();
    }

    private void SubSkill()
    {
        attack.SubSkill();
    }

    private void MainSkill()
    {
        attack.MainSkill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 hitDir = (gameObject.transform.position - collision.transform.position).normalized;
            Hit(hitDir);
        }
    }

    private void Hit(Vector2 dir)
    {
        if (hp.isInvincible) return;
        hp.Damage();
        StartCoroutine(KnockBack(dir));
    }

    private IEnumerator KnockBack(Vector2 dir)
    {
        movement.canMove = false;
        visual.Blink();
        rb.AddForce(dir * knockBackPower);
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        movement.canMove = true;
    }
}
