using UnityEngine;
using FSM.State;

public enum EnemyStates
{
    Idle,
    Chase,
    Attack,
    Die
}

public class EnemyStateMachine : StateMachine<EnemyStates>
{
}
