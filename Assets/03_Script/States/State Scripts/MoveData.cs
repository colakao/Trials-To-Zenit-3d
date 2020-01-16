using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Move")]
    public class MoveData : StateData
    {
        public float runVelocity;

        public override void UpdateAbility(CharacterState characterState, Animator animator)
        {
            CharacterController cc = characterState.GetCharacterController(animator);
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

                cc.transform.position += (camF * input.y + camR * input.x) * runVelocity * Time.deltaTime;
            }
        }
    }
}

