using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Move")]
    public class MoveData : StateData
    {
        [Range(1f,10f)]
        public float runVelocity;
        public AnimationCurve speedGraph;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl cc = characterState.GetCharacterControl(animator);
            if (VirtualInputManager.Instance.movement.magnitude == 0)
            {
                animator.SetBool(TransitionParameter.isMove.ToString(), false);
            }
            if (VirtualInputManager.Instance.movement.magnitude > 0)
            {
                var input = VirtualInputManager.Instance.movement;

                var camF = Camera.main.transform.forward;
                var camR = Camera.main.transform.right;

                camF.y = 0f;
                camR.y = 0f;
                camF.Normalize();
                camR.Normalize();

                cc.transform.position += (camF * input.y + camR * input.x) * runVelocity * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime;
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

