using System.Collections;
using UnityEngine;

public class EliteSlime : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStatsSO stats;
    [SerializeField] private GameObject smashEffect;

    private EnemyState currentState;
    private FSM fsm;
    private Player player;
    private EliteSlime self;
    private SlimeAttack attack;
    private SlimeSkill skill;

    private Vector2 moveDir = Vector2.zero;

    private bool skillAvailable = true;
    private float skillCooltime = 0f;

    private void Start()
    {
        currentState = EnemyState.Idle;
        player = GameManager.Instance.player;
        self = gameObject.GetComponent<EliteSlime>();
        attack = GetComponent<SlimeAttack>();
        skill = GetComponent<SlimeSkill>();
        skillCooltime = stats.skillDelay;

        fsm = new FSM(new IdleState(this));
        StartCoroutine(PatrolCoroutine());
    }

    protected void Update()
    {
        base.Update();

        if(!skillAvailable)
        {
            skillCooltime -= Time.deltaTime;
            if(skillCooltime <= 0f)
            {
                skillAvailable = true;
            }
        }
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
            case EnemyState.Skill:
                fsm.ChangeState(new AttackState(this));
                StartCoroutine(SkillCoroutine());
                base.state = EnemyState.Skill;
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
        if (player == null) player = GameManager.Instance.player;

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
        if (player == null) player = GameManager.Instance.player;

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
                if(skillAvailable) ChangeState(EnemyState.Skill);
                else ChangeState(EnemyState.Attack);

                animator.SetBool("isMoving", false);
                yield break;
            }

            moveDir = (player.transform.position - self.transform.position).normalized;
            self.transform.position += (Vector3)(moveDir * stats.moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        float elapsed = 0f;
        attack.Active(true, 3f);
        moveDir = (player.transform.position - self.transform.position).normalized;

        while (elapsed < stats.attackDelay)
        {
            attack.SetGauge(elapsed / stats.attackDelay);
            elapsed += Time.deltaTime;
            yield return null;
        }

        float dashTime = 0.5f;
        float dashSpeed = stats.moveSpeed * 7;
        attack.Active(false);
        animator.SetTrigger("isDash");

        while (dashTime > 0f)
        {
            transform.position += (Vector3)(moveDir * dashSpeed * Time.deltaTime);
            dashTime -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        ChangeState(EnemyState.Chase);
    }

    private IEnumerator SkillCoroutine()
    {
        float elapsed = 0f;

        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.45f);
        transform.Find("Visual").GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().isTrigger = true;

        elapsed = 0f;
        skill.Active(true);
        while (elapsed < stats.skillDelay / 3f) 
        {
            Vector2 dir = (player.transform.position - self.transform.position).normalized;
            self.transform.position += (Vector3)(dir * stats.moveSpeed * Time.deltaTime);
            skill.SetGauge(elapsed / (stats.skillDelay / 3f));
            elapsed += Time.deltaTime;
            yield return null;
        }

        skill.Active(false);
        transform.Find("Visual").GetComponent<SpriteRenderer>().enabled = true;
        animator.SetTrigger("Landing");

        yield return new WaitForSeconds(0.3f);
        GetComponent<Collider2D>().isTrigger = false;
        if (smashEffect != null) Instantiate(smashEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        skillAvailable = false;
        skillCooltime = stats.skillDelay;

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
