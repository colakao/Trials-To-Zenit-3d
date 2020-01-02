using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;
    [Range(300f, 1000f)]
    public float RotationSpeed = 400f;
    public float rotationMin = -40f;
    public float rotationMax = 70f;
    float mouseX, mouseY;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        var rotateInput = playerInput.Camera.CameraRotation.ReadValue<Vector2>();

        mouseX += rotateInput.x * RotationSpeed / Screen.width;  
        mouseY -= rotateInput.y * RotationSpeed / Screen.height;

        mouseY = Mathf.Clamp(mouseY,rotationMin, rotationMax);

        target.rotation = Quaternion.Euler(mouseY, mouseX, transform.rotation.z);
    }

    private void OnEnable()
    {
        playerInput.Camera.Enable();
    }

    private void OnDisable()
    {
        playerInput.Camera.Disable();
    }
}
