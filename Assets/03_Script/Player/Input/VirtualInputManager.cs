using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        // movement input
        public Vector2 movement;
        public bool jump;
        public bool attack;
    }
}

