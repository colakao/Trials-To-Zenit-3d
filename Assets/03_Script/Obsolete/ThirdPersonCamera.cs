using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    Camera mainCam;

    public Transform target;
    [Range(300f, 1000f)]
    public float RotationSpeed = 400f;
    public float rotationMin = -40f;
    public float rotationMax = 70f;

    //Position and rotation
    public float cameraHeight = 1.35f;
    public float cameraDistance = 5f;
    float mouseX, mouseY;

    //CamRay Collision
    public bool camRayDebbug;
    public float camRayCushion = 0.35f;
    public float adjustedDistance;
    public LayerMask collisionMask;
    Ray camRay;
    RaycastHit camRayHit;

    public Vector3 newOffset;

    PlayerInput playerInput;

    private void Awake()
    {
        mainCam = Camera.main;
        //transform.localPosition = new Vector3(0, cameraHeight, -adjustedDistance);
        playerInput = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        newOffset = Vector3.up * cameraHeight + Vector3.forward * -adjustedDistance;
        CamControl();
        CamCollisions();
    }

    void CamControl()
    {
        var rotateInput = playerInput.Camera.CameraRotation.ReadValue<Vector2>();

        mouseX += rotateInput.x * RotationSpeed / Screen.width;  
        mouseY -= rotateInput.y * RotationSpeed / Screen.height;

        mouseY = Mathf.Clamp(mouseY,rotationMin, rotationMax);

        target.rotation = Quaternion.Euler(mouseY, mouseX, transform.rotation.z);
        transform.localPosition = newOffset;
    }

    void CamCollisions()
    {
        var cameraCushionedDistance = cameraDistance + camRayCushion;
        camRay.origin = transform.position;
        camRay.direction = transform.forward;

        if (Physics.Raycast(camRay, out camRayHit, cameraCushionedDistance, collisionMask))
        {
            adjustedDistance = Vector3.Distance(camRay.origin, camRayHit.point) - camRayCushion;
        }
        else
        {
            adjustedDistance = cameraCushionedDistance - camRayCushion;
        }

        if (camRayDebbug)
            Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction * cameraCushionedDistance, Color.cyan);
    }

    void CamOcclushion()
    {

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
