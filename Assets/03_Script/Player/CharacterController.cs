using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playerScripts
{
    public enum TransitionParameter
    {
        isMove,
    }
    public class CharacterController : MonoBehaviour
    {

        [System.Serializable]
        public class MoveSettings
        {
            public float rotateVelocity = 12f;
            public float jumpVelocity = 25f;
            public float distToGrounded = 0.1f;
            public LayerMask ground;

            public bool debugRay;
        }

        [System.Serializable]
        public class PhysSettings
        {
            public float gravity = -9.81f;

            public float fullJumpMult = 2.5f;
            public float lowJumpMult = 2f;
        }

        [System.Serializable]
        public class InputSettings
        {
            public float inputDelay = 0.1f;
        }

        public MoveSettings moveSettings = new MoveSettings();
        public PhysSettings physSettings = new PhysSettings();
        public InputSettings inputSettings = new InputSettings();

        Vector3 velocity = Vector3.zero;
        Quaternion targetRotation;
        Rigidbody rBody;
        Animator playerAnim;
        float forwardInput, rightInput;
        bool isJump;
        Vector3 camF, camR;

        PlayerInput inputAction;
        Vector2 input;

        public Quaternion TargetRotation
        {
            get { return targetRotation; }
        }

        bool Grounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, moveSettings.distToGrounded, moveSettings.ground);
        }

        private void Awake()
        {
            inputAction = new PlayerInput();
            playerAnim = transform.GetChild(0).GetComponent<Animator>();
        }

        private void Start()
        {
            Cursor.visible = false;
            if (GetComponent<Rigidbody>())
                rBody = GetComponent<Rigidbody>();
            else
                Debug.LogError("The player needs a rigidbody");
        }

        private void Update()
        {
            if (moveSettings.debugRay)
            {
                Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y * -moveSettings.distToGrounded, transform.position.z), Color.cyan);
            }
        }

        private void FixedUpdate()
        {
            rBody.velocity = transform.TransformDirection(velocity);
            PlayerRotation();
            Gravity();
        }

        private void Gravity()
        {
            //Gravity
            if (!Grounded())
            {
                velocity -= Vector3.down * physSettings.gravity * (physSettings.fullJumpMult - 1) * Time.deltaTime;
                // eventualmente mejorar feel de salto
            }
            else if (Grounded() && !inputAction.Player.Jump.triggered)
            {
                velocity.y = 0;
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
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, moveSettings.rotateVelocity * Time.deltaTime);
            }
            else if (input.magnitude == 0)
            {
                playerAnim.SetBool(TransitionParameter.isMove.ToString(), false);
            }
        }

        private void Jump()
        {
            if (Grounded() && isJump)
            {
                //Jump
                velocity.y = moveSettings.jumpVelocity;
                isJump = false;
            }
            else if (!isJump && Grounded())
            {
                //Zero out or velocity.y
                velocity.y = 0;
            }
            else
            {
                //decrease velocity.y
                velocity.y -= physSettings.gravity;
            }
        }

        private void JumpAction(InputAction.CallbackContext context)
        {
            isJump = true;
            if (Grounded())
                velocity = Vector3.up * moveSettings.jumpVelocity;
        }

        public void OnEnable()
        {
            inputAction.Player.Jump.performed += JumpAction;
            inputAction.Player.Enable();
        }

        public void OnDisable()
        {
            inputAction.Player.Jump.performed -= JumpAction;
            inputAction.Player.Disable();
        }
    }

}
