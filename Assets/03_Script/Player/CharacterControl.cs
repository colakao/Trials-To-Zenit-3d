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
        attack,
    }

    public class CharacterControl : MonoBehaviour
    {
        [System.Serializable]
        public class RotateSettings
        {
            [HideInInspector]
            public float angle;
            [HideInInspector]
            public Quaternion targetRotation;
            [Tooltip("Valores mas pequenos causan un offset en el movimiento del jugador.")]
            public float rotateVelocity = 12f;    
        }
        [System.Serializable]
        public class DebugSettings
        {
            public bool debugRay;
            public bool debugRaySlope;
            public bool debugForwardRay;
            public LayerMask ground;
            public float distToGrounded = 0.1f;
            public float distToSloped = 1.5f;
        }
        [System.Serializable]
        public class PhysSettings
        {
            public float gravity = -9.81f;

            //public float fullJumpMult = 2.5f;
            //public float lowJumpMult = 2f;

            [HideInInspector]
            public float groundAngle;
            [HideInInspector]
            public Vector3 forward;
            public float maxSlopeAngle;
        }

        public RotateSettings rotateSettings = new RotateSettings();
        public PhysSettings physSettings = new PhysSettings();
        public DebugSettings debugSettings = new DebugSettings();

        [Header("Debug bools")]
        public bool jump;
        public bool attack;

        public bool isGrounded;
        public bool isSloped;

        Animator playerAnim;
        //float forwardInput, rightInput;
        //Vector3 camF, camR;

        [HideInInspector]
        public Vector2 input;
        Transform cam;

        [HideInInspector]
        public Vector3 velocity = Vector3.zero;

        Rigidbody rB;

        private void Awake()
        {
            playerAnim = transform.GetChild(0).GetComponent<Animator>();
            cam = Camera.main.transform;
        }

        private void Start()
        {
            Cursor.visible = false;
            if (GetComponent<Rigidbody>())
            {
                rB = GetComponent<Rigidbody>();
                rB.velocity = velocity;
            }
            else
                Debug.LogError("The player needs a rigidbody");
        }

        private void LateUpdate()
        {
            if (debugSettings.debugRay)
            {
                Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - debugSettings.distToGrounded, transform.position.z), Color.cyan);
            }
            if (debugSettings.debugRaySlope)
            {
                Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - debugSettings.distToSloped, transform.position.z), Color.yellow);
            }
            if (debugSettings.debugForwardRay)
            {
                Debug.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(0,0.5f,0) + physSettings.forward * 3f, Color.yellow);
            }

            if (Grounded())
            {
                isGrounded = true;
                playerAnim.SetBool(TransitionParameter.isGrounded.ToString(), true);
            }
            if (!Grounded())
            {
                physSettings.forward = transform.forward; // 1
                physSettings.groundAngle = 90f; // 2
                jump = false;
                isGrounded = false;
                playerAnim.SetBool(TransitionParameter.isGrounded.ToString(), false);
            }
            if (Slope())
                isSloped = true;
            if (!Slope())
            {
                physSettings.forward = transform.forward;
                physSettings.groundAngle = 90f; // 
                isSloped = false;
            }
        }

        private void FixedUpdate()
        {
            //if (rB != null)
            //    rB.velocity = transform.TransformDirection(velocity);
            //else
            //    Debug.LogError("The player needs a rigidbody");

            Gravity();
            CalculateAngle();
            PlayerRotation();
        }

        public bool Grounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, debugSettings.distToGrounded, debugSettings.ground);
        }

        public bool Slope()
        {
            if (jump)
                return false;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, debugSettings.distToSloped))
                if (hit.normal != Vector3.up)
                {
                    physSettings.forward = Vector3.Cross(transform.right, hit.normal); // 1
                    physSettings.groundAngle = Vector3.Angle(hit.normal, -transform.forward); // 2
                    return true;
                }
            return false;
        }

        private void Gravity()
        {
            //Gravity
            if (!Grounded())
            {
                rB.velocity -= Vector3.down * physSettings.gravity * Time.deltaTime;
            }
            if (Grounded() && Mathf.Approximately(input.magnitude, 0))
            {
                //var v = rB.velocity;
                //v.y = 0;
                rB.velocity = Vector3.zero;
            }
            if (Grounded() && input.magnitude > 0)
            {
                var v = rB.velocity;
                v.y = 0;
                rB.velocity = v;
            }
        }

        private void CalculateAngle()
        {
            //input = VirtualInputManager.Instance.movement;
            input = GetComponent<ManualInput>().input;
            rotateSettings.angle = Mathf.Atan2(input.x, input.y);
            rotateSettings.angle = Mathf.Rad2Deg * rotateSettings.angle;
            rotateSettings.angle += cam.eulerAngles.y;
        }

        private void PlayerRotation()
        {
            if (input.magnitude > 0)
            {
                rotateSettings.targetRotation = Quaternion.Euler(0, rotateSettings.angle, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotateSettings.targetRotation, rotateSettings.rotateVelocity * Time.deltaTime);
            }
        }
    }

}
