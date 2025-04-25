using System;
using AYellowpaper.SerializedCollections;
using FSM.Agent;
using UnityEngine;
using UnityEngine.Events;

namespace FSM.State
{
    [DefaultExecutionOrder(-100)]
    public class StateMachine<T> : MonoBehaviour where T : Enum
    {
        [Header("States")]
        public StateBaseSO<T> _stateBaseSO;
        [Header("Events"), Space]
        public UnityEvent<T> onStateChanged;

        private State<T> _currentState;
        public State<T> CurrentState => _currentState;
        private bool _isInitialized;

        public void Initialize(AgentAI<T> agent)
        {
            if (_isInitialized) return;
            foreach (var entry in _stateBaseSO.StateEntries)
            {
                if (entry.Value == null) continue;
                entry.Value.Initialize(agent, this, entry.Key.ToString());
            }

            ChangeState(_stateBaseSO.InitialState, true);
            _isInitialized = true;
        }

        public void ChangeState(T key, bool force = false)
        {
            if (!_stateBaseSO.StateEntries.TryGetValue(key, out var nextState))
            {
                Debug.LogWarning($"State {key} not found in StateMachine.");
                return;
            }

            if (!force && nextState == _currentState) return;

            _currentState?.ExitState();

            _currentState = nextState;
            _currentState.EnterState();
            onStateChanged?.Invoke(key);
        }

        public void UpdateCurrentState()
        {
            _currentState?.UpdateState();
        }
    }
}