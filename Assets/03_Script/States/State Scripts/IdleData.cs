using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Idle")]
    public class IdleData : StateData
    {
        public override void UpdateAbility(CharacterState characterState, Animator animator)
        {
            if (VirtualInputManager.Instance.movement.magnitude > 0)
            {
                animator.SetBool(TransitionParameter.isMove.ToString(), true);
            }
        }
    }
}

