using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Range(1f,15f)]
    public float speed;
    [Range(10f, 100f)]
    public float turnSpeed;

    Camera mainCamera;

    Vector2 input;

    Vector3 camF;
    Vector3 camR;

    bool isMoving;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
    }

    private void PlayerMovement()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        camF = mainCamera.transform.forward;
        camR = mainCamera.transform.right;

        camF.y = 0f;
        camR.y = 0f;
        camF.Normalize();
        camR.Normalize();

        transform.position += (camF * input.y + camR * input.x) * speed * Time.deltaTime;

    }

    private void PlayerRotation()
    {
        //Look at cam direction when you move
        var intent = camF * input.y + camR * input.x;

        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }
}
