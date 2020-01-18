using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Idle")]
    public class IdleData : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (VirtualInputManager.Instance.movement.magnitude > 0)
            {
                animator.SetBool(TransitionParameter.isMove.ToString(), true);
            }
            if (characterState.GetCharacterControl(animator).jump)
            {
                animator.SetBool(TransitionParameter.jump.ToString(), true);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

