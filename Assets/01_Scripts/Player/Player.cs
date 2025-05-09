using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerInput input;
    public PlayerMovement movement;
    public PlayerHP hp;

    private Rigidbody2D rb;

    [SerializeField] private float knockBackPower = 600f;
    [SerializeField] private float knockBackTime = 0.25f;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        hp = FindFirstObjectByType<PlayerHP>();

        rb = GetComponent<Rigidbody2D>();

        input.OnMainSkill += MainSkill;
        input.OnSubSkill += SubSkill;
    }

    private void Update()
    {
        
    }

    private void MainSkill()
    {

    }

    private void SubSkill()
    {

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
        hp.Damage();
        StartCoroutine(KnockBack(dir));
    }

    private IEnumerator KnockBack(Vector2 dir)
    {
        movement.canMove = false;
        rb.AddForce(dir * knockBackPower);
        yield return new WaitForSeconds(0.25f);
        rb.linearVelocity = Vector2.zero;
        movement.canMove = true;
    }
}
