using FSM.Agent;
using FSM.State;
using UnityEngine;

[CreateAssetMenu(fileName = "TestState1", menuName = "FSM/TestState1")]
public class TestState1 : State<TestState>
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering TestState1");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Updating TestState1");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(TestState.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting TestState1");
    }
}