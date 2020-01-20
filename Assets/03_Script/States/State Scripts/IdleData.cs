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
            CharacterControl cc = characterState.GetCharacterControl(animator);

            //if (VirtualInputManager.Instance.movement.magnitude > 0)
            //{
            //    animator.SetBool(TransitionParameter.isMove.ToString(), true);
            //}
            if (cc.GetComponent<ManualInput>().input.magnitude > 0)
            {
                animator.SetBool(TransitionParameter.isMove.ToString(), true);
            }
            if (cc.jump)
            {
                animator.SetBool(TransitionParameter.jump.ToString(), true);
            }
            if (cc.attack)
            {
                animator.SetBool(TransitionParameter.attack.ToString(), true);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

