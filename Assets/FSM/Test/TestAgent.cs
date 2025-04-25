using FSM;
using FSM.Agent;
using FSM.State;
using UnityEngine;

public class TestAgent : AgentAI<TestState>
{
    protected override void Awake()
    {
        base.Awake();
        StateMachineCompo.ChangeState(TestState.Idle);
    }
}

public enum TestState
{
    Idle,
    Move,
    Attack,
    Die
}