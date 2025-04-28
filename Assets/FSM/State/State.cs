using System;
using FSM.Agent;
using UnityEngine;
using System.Collections;

namespace FSM.State
{
    public abstract class State<T> : ScriptableObject where T : Enum
    {
        protected AgentAI<T> Agent;
        protected StateMachine<T> StateMachine;
        protected int StateHash;
        protected IEnumerator  Coroutine;

        public virtual void Initialize(AgentAI<T> agent, StateMachine<T> machine, string stateName)
        {
            this.Agent = agent;
            this.StateMachine = machine;
            StateHash = Animator.StringToHash(stateName);
        }

        public virtual void EnterState()
        {
            // Agent.AnimatorCompo.SetBool(StateHash, true);
            // Agent.StartCoroutine(Coroutine);
        }
        public virtual void UpdateState() 
        {
        }

        public virtual void ExitState()
        {
            // Agent.AnimatorCompo.SetBool(StateHash, false);
            // if (Coroutine != null)
            // {
            //     Agent.StopCoroutine(Coroutine);
            //     Coroutine = null;
            // }
        }
        public virtual void OnAnimationEvent(string evt) { }
    }
}