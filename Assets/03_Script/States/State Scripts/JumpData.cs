using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Jump")]
    public class JumpData : StateData
    {
        [Range(2000,15000)]
        public float jumpForce = 5000f;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.GetCharacterControl(animator).jump = false;
            //characterState.GetCharacterControl(animator).velocity = Vector3.up * jumpVelocity;
            characterState.GetCharacterControl(animator).GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.GetCharacterControl(animator).jump = false;
        }
    }
}

