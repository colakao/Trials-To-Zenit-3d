using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class KeyboardInput : Singleton<KeyboardInput>
    {
        [HideInInspector]
        public Vector2 input; // Movement

        void FixedUpdate()
        {
            Movement();
            Attack();
        }

        #region Movement
        void Movement()
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            input.Normalize();

            VirtualInputManager.Instance.movement.x = input.x;
            VirtualInputManager.Instance.movement.y = input.y;
        }
        #endregion

        #region Attack
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Q)) // Ability One
            {
                Debug.Log("Q!");
            }
            if (Input.GetKeyDown(KeyCode.E)) // Ability Two
            {
                Debug.Log("E!");
            }
            if (Input.GetKeyDown(KeyCode.R)) // Ulti
            {
                Debug.Log("Ulti!");
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) // Dash
            {
                Debug.Log("Dash!");
            }
        }
        #endregion
    }
}

