using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStatsSO stats;

    private EnemyState currentState;
    private FSM fsm;
    private Player player;

    private void Start()
    {
        currentState = EnemyState.Idle;
        fsm = new FSM(new IdleState(this));
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        switch(currentState)
        {
            case EnemyState.Idle:
                if(CanDetectPlayer())
                {
                    if(CanAttackPlayer())
                    {
                        ChangeState(EnemyState.Attack);
                    }
                    else
                    {
                        ChangeState(EnemyState.Chase);
                    }
                }
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Skill:
                break;
            case EnemyState.Die:
                break;
        }
    }

    private void ChangeState(EnemyState nextState)
    {
        currentState = nextState;
        switch(currentState)
        {
            case EnemyState.Idle:
                fsm.ChangeState(new IdleState(this));
                break;
            case EnemyState.Chase:
                fsm.ChangeState(new ChaseState(this));
                break;
            case EnemyState.Attack:
                fsm.ChangeState(new AttackState(this));
                break;
            case EnemyState.Skill:
                fsm.ChangeState(new SkillState(this));
                break;
            case EnemyState.Die:
                fsm.ChangeState(new DieState(this));
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
}
