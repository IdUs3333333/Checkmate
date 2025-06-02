using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStatsSO stats;
    [SerializeField] private GameObject bone;

    private EnemyState currentState;
    private FSM fsm;
    private Player player;
    private Skeleton self;

    private Vector2 moveDir = Vector2.zero;

    private void Start()
    {
        currentState = EnemyState.Idle;
        player = GameManager.Instance.player;
        self = gameObject.GetComponent<Skeleton>();

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
                base.state = EnemyState.Idle;
                break;
            case EnemyState.Chase:
                fsm.ChangeState(new ChaseState(this));
                StartCoroutine(ChaseCoroutine());
                base.state = EnemyState.Chase;
                break;
            case EnemyState.Attack:
                fsm.ChangeState(new AttackState(this));
                StartCoroutine(AttackCoroutine());
                base.state = EnemyState.Attack;
                break;
            case EnemyState.Die:
                fsm.ChangeState(new DieState(this));
                Die();
                base.state = EnemyState.Die;
                break;
        }
    }

    private bool CanDetectPlayer()
    {
        Vector2 dir = ((Vector2)player.transform.position - (Vector2)self.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(self.transform.position, dir, stats.detectRange, 1 << LayerMask.NameToLayer("EnemyDetectLayer"));

        Debug.DrawRay(self.transform.position, dir * stats.detectRange, Color.cyan);

        if (hit.collider == null) return false;

        if (hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    private bool CanAttackPlayer()
    {
        Vector2 dir = ((Vector2)player.transform.position - (Vector2)self.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(self.transform.position, dir, stats.attackRange, 1 << LayerMask.NameToLayer("EnemyDetectLayer"));

        if (hit.collider == null) return false;

        if (hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
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

            moveDir = (player.transform.position - self.transform.position).normalized;
            transform.position += (Vector3)(moveDir * stats.moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        animator.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.25f);

        Vector2 attackDir = (player.transform.position - self.transform.position).normalized;
        SkeletonBone iBone = Instantiate(bone, transform.position, Quaternion.identity).GetComponent<SkeletonBone>();
        iBone.Shoot(attackDir, 2.5f, stats.attackRange);

        yield return new WaitForSeconds(2f);
        ChangeState(EnemyState.Chase);
    }

    private void Die()
    {

    }

    private void OnDrawGizmos()
    {
        Color baseColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        Gizmos.color = baseColor;
    }
}
