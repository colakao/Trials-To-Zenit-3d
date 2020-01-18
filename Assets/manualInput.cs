using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class manualInput : MonoBehaviour
    {
        CharacterControl cc;

        private void Awake()
        {
            cc = this.gameObject.GetComponent<CharacterControl>();
        }
        void Update()
        {
            if (cc.Grounded())
            {
                if (VirtualInputManager.Instance.jump)
                {
                    cc.jump = true;
                }
            }
        }
    }
}

