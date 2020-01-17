using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playerScripts
{
    public enum TransitionParameter
    {
        isMove,
        jump,
        forceTransition,
        isGrounded,
    }

    public class CharacterControl : MonoBehaviour
    {
        [System.Serializable]
        public class RotateSettings
        {
            public float rotateVelocity = 12f;
        }
        [System.Serializable]
        public class DebugSettings
        {
            public bool debugRay;
            public LayerMask ground;
            public float distToGrounded = 0.1f;
        }
        [System.Serializable]
        public class PhysSettings
        {
            public float gravity = -9.81f;

            public float fullJumpMult = 2.5f;
            public float lowJumpMult = 2f;
        }

        public RotateSettings rotateSettings = new RotateSettings();
        public PhysSettings physSettings = new PhysSettings();
        public DebugSettings debugSettings = new DebugSettings();

        public bool jump;
        public bool isGrounded;

        Animator playerAnim;
        float forwardInput, rightInput;
        Vector3 camF, camR;

        Vector2 input;

        [HideInInspector]
        public Vector3 velocity = Vector3.zero;

        Rigidbody rB;

        public bool Grounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, debugSettings.distToGrounded, debugSettings.ground);
        }

        private void Awake()
        {
            playerAnim = transform.GetChild(0).GetComponent<Animator>();
        }

        private void Start()
        {
            Cursor.visible = false;
            if (GetComponent<Rigidbody>())
                rB = GetComponent<Rigidbody>();
            else
               Debug.LogError("The player needs a rigidbody");
        }

        private void Update()
        {
            if (debugSettings.debugRay)
            {
                Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - debugSettings.distToGrounded, transform.position.z), Color.cyan);
            }
            if (Grounded())
            {
                isGrounded = true;
                playerAnim.SetBool(TransitionParameter.isGrounded.ToString(), true);
            }
            else
            {
                isGrounded = false;
                playerAnim.SetBool(TransitionParameter.isGrounded.ToString(), false);
            }
        }

        private void FixedUpdate()
        {
            if (rB != null)
            rB.velocity = transform.TransformDirection(velocity);
            else
                Debug.LogError("The player needs a rigidbody");

            PlayerRotation();
            Gravity();
        }

        private void Gravity()
        {
            //Gravity
            if (!Grounded())
            {
                velocity -= Vector3.down * physSettings.gravity * (physSettings.fullJumpMult - 1) * Time.deltaTime;
            }
            if (Grounded() && !VirtualInputManager.Instance.jump)
            {
                velocity.y = 0;
            }
            if (Grounded() && VirtualInputManager.Instance.jump)
            {
                //Jump
                jump = true;
            }
        }

        private void PlayerRotation()
        {
            input = VirtualInputManager.Instance.movement;
            camF = Camera.main.transform.forward;
            camR = Camera.main.transform.right;
            camF.y = 0;
            camR.y = 0;

            //Look at cam direction when you move
            var intent = camF * input.y + camR * input.x;

            if (input.magnitude > 0)
            {
                playerAnim.SetBool(TransitionParameter.isMove.ToString(), true);
                Quaternion rot = Quaternion.LookRotation(intent);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSettings.rotateVelocity * Time.deltaTime);
            }
            else if (input.magnitude == 0)
            {
                playerAnim.SetBool(TransitionParameter.isMove.ToString(), false);
            }
        }
    }

}
