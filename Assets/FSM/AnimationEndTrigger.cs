using System;
using UnityEngine;
using UnityEngine.Events;

namespace FSM.Animation
{
    public class AnimationEndTrigger : MonoBehaviour
    {
        public UnityEvent OnAnimationEnd;

        public void AnimationEnd()
        {
            OnAnimationEnd.Invoke();
        }
    }
}
