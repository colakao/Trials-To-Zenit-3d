using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerScripts
{
    public class AddForceExample : MonoBehaviour
    {
        Rigidbody rB;
        public float jumpForce = 300f;
        private void Awake()
        {
            rB = this.gameObject.GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
                rB.AddForce(Vector3.up * jumpForce);
        }
    }
}

