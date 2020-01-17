using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public abstract class StateData : ScriptableObject
    {
        public float Duration;

        public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
    }
}

