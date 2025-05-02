using UnityEngine;

public class FSM : MonoBehaviour
{
    private BaseState _currentState;

    public FSM(BaseState initState)
    {
        _currentState = initState;
        ChangeState(_currentState);
    }

    public void ChangeState(BaseState nextState)
    {
        if (nextState == _currentState) return;

        if (_currentState != null) _currentState.OnStateExit();

        _currentState = nextState;
        _currentState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (_currentState != null) _currentState.OnStateUpdate();
    }
}
