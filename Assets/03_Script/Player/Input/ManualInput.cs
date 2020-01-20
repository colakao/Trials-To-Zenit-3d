using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class ManualInput : MonoBehaviour
    {
        CharacterControl cc;
        public Vector2 input;

        private void Awake()
        {
            cc = this.gameObject.GetComponent<CharacterControl>();
        }

        void Update()
        {
            input = VirtualInputManager.Instance.movement;

            if (cc.Grounded())
            {
                if (VirtualInputManager.Instance.jump)
                {
                    cc.jump = true;
                }
            }
            if (VirtualInputManager.Instance.attack)
            {
                VirtualInputManager.Instance.attack = false;
                cc.attack = true;
            }

        }
    }
}

