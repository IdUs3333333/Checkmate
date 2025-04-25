using UnityEngine;
using FSM.State;
using FSM.Agent;

[CreateAssetMenu(fileName = "EnemyDie", menuName = "FSM/Enemy/EnemyDie")]
public class EnemyDie : State<EnemyStates>
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
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

