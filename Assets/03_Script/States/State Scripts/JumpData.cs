using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    [CreateAssetMenu(fileName = "New MoveData", menuName = "State Machine/Ability/Jump")]
    public class JumpData : StateData
    {
        [Range(200,1500)]
        public float jumpForce = 200;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.GetCharacterControl(animator).GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }


        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.GetCharacterControl(animator).jump = false;
        }
    }
}

