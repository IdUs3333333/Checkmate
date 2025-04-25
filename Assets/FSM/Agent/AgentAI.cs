using System;
using FSM.State;
using UnityEngine;

namespace FSM.Agent
{
    public abstract class AgentAI<T> : MonoBehaviour where T : Enum
    {
        [field: SerializeField] public StateMachine<T> StateMachineCompo { get; private set; }
        [field: SerializeField] public Animator AnimatorCompo { get; private set; }

        protected virtual void Awake()
        {
            if (StateMachineCompo == null)
            {
                Debug.LogError("StateMachine reference is missing in AgentAI.");
                return;
            }
            StateMachineCompo.Initialize(this);
        }

        protected virtual void Update()
        {
            StateMachineCompo?.UpdateCurrentState();
        }
    }
}