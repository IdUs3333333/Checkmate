using FSM.Agent;
using FSM.State;
using UnityEngine;

[CreateAssetMenu(fileName = "TestState1", menuName = "FSM/TestState2")]
public class TestState2 : State<TestState>
{

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering TestState2");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Updating TestState2");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(TestState.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting TestState2");
    }
}