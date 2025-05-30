using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStatsSO stats;

    private EnemyState currentState;
    private FSM fsm;
    private Player player;
    private SlimeAttack attack;

    private Vector2 moveDir = Vector2.zero;

    private void Start()
    {
        currentState = EnemyState.Idle;
        player = GameManager.Instance.player;
        attack = GetComponent<SlimeAttack>();

        fsm = new FSM(new IdleState(this));
        StartCoroutine(PatrolCoroutine());
    }

    private void ChangeState(EnemyState nextState)
    {
        currentState = nextState;
        switch (currentState)
        {
            case EnemyState.Idle:
                fsm.ChangeState(new IdleState(this));
                StartCoroutine(PatrolCoroutine());
                break;
            case EnemyState.Chase:
                fsm.ChangeState(new ChaseState(this));
                StartCoroutine(ChaseCoroutine());
                break;
            case EnemyState.Attack:
                fsm.ChangeState(new AttackState(this));
                StartCoroutine(AttackCoroutine());
                break;
            case EnemyState.Die:
                fsm.ChangeState(new DieState(this));
                Die();
                break;
        }
    }

    private bool CanDetectPlayer()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        return (dist <= stats.detectRange);
    }

    private bool CanAttackPlayer()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        return (dist <= stats.attackRange);
    }

    private IEnumerator PatrolCoroutine()
    {
        float timer = 0f;
        moveDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;

        while (true)
        {
            if (CanDetectPlayer())
            {
                ChangeState(EnemyState.Chase);
                animator.SetBool("isMoving", false);
                yield break;
            }

            transform.position += (Vector3)(moveDir * stats.moveSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                animator.SetBool("isMoving", true);
                moveDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
                timer = 0f;
            }

            yield return null;
        }
    }

    private IEnumerator ChaseCoroutine()
    {
        while (true)
        {
            animator.SetBool("isMoving", true);

            if (!CanDetectPlayer())
            {
                ChangeState(EnemyState.Idle);
                animator.SetBool("isMoving", false);
                yield break;
            }

            if (CanAttackPlayer())
            {
                ChangeState(EnemyState.Attack);
                animator.SetBool("isMoving", false);
                yield break;
            }

            moveDir = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)(moveDir * stats.moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        float elapsed = 0f;
        attack.Active(true);
        moveDir = (player.transform.position - transform.position).normalized;

        while (elapsed < stats.attackDelay)
        {
            attack.SetGauge(elapsed / stats.attackDelay);
            elapsed += Time.deltaTime;
            yield return null;
        }

        float dashTime = 0.5f;
        float dashSpeed = stats.moveSpeed * 3;
        attack.Active(false);
        animator.SetTrigger("isDash");

        while (dashTime > 0f)
        {
            transform.position += (Vector3)(moveDir * dashSpeed * (Time.deltaTime));
            dashTime -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        ChangeState(EnemyState.Chase);
    }

    private void Die()
    {

    }
}
