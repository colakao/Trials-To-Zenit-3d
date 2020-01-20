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

            #region MovementLogic
            if (VirtualInputManager.Instance.movement.magnitude == 0)
            {
                animator.SetBool(TransitionParameter.isMove.ToString(), false);
            }
            if (VirtualInputManager.Instance.movement.magnitude > 0 && cc.physSettings.groundAngle > cc.physSettings.maxSlopeAngle)
            {
                cc.transform.position += cc.physSettings.forward * runVelocity * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime;
            }
            #endregion

            #region Transitions
            if (cc.jump)
            {
                animator.SetBool(TransitionParameter.jump.ToString(), true);
            }
            if (cc.attack)
            {
                animator.SetBool(TransitionParameter.attack.ToString(), true);
            }
            #endregion
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

