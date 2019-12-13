using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target, player;
    public float RotationSpeed = 1f;
    float mouseX, mouseY;


    void Start()
    {
        //offset = transform.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxisRaw("Mouse Y") * RotationSpeed;

        target.rotation = Quaternion.Euler(mouseY, mouseX, transform.rotation.z);
    }
}
