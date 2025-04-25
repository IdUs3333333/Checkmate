using UnityEngine;
using FSM.State;
using FSM.Agent;
using System.Collections;

[CreateAssetMenu(fileName = "EnemyIdle", menuName = "FSM/Enemy/EnemyIdle")]
public class EnemyIdle : State<EnemyStates>
{
    public override void Initialize(AgentAI<EnemyStates> agent, StateMachine<EnemyStates> machine, string stateName)
    {
        base.Initialize(agent, machine, stateName);
        Coroutine = Co();
    }

    private IEnumerator Co()
    {
        yield return null;
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
            StateMachine.ChangeState(EnemyStates.Chase);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

