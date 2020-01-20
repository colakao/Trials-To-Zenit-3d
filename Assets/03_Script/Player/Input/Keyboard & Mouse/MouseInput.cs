using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class MouseInput : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                VirtualInputManager.Instance.attack = true;
            }
        }
    }
}

