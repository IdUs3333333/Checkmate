using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace FSM.State
{
    public class StateBaseSO<T> : ScriptableObject where T : Enum
    {
        [Header("States")]
        public SerializedDictionary<T, State<T>> StateEntries = new();

        [Header("Initial State")]
        public T InitialState;
    }
}