using UnityEngine;
using FSM.State;
using FSM.Agent;

[CreateAssetMenu(fileName = "EnemyChase", menuName = "FSM/Enemy/EnemyChase")]
public class EnemyChase : State<EnemyStates>
{
    public override void Initialize(AgentAI<EnemyStates> agent, StateMachine<EnemyStates> machine, string stateName)
    {
        base.Initialize(agent, machine, stateName);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(EnemyStates.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

