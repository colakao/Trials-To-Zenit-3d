using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Range(1f,15f)]
    public float speed = 8f;
    [Range(10f, 100f)]
    public float turnSpeed = 30f;
    private float gravity = 9.81f;

    Camera mainCamera;
    Rigidbody rigidBody;

    Vector3 camF;
    Vector3 camR;

    bool isGrounded;

    Vector2 input;

    //Input actions
    PlayerInput playerInput;


    private void Awake()
    {
        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
    }

    private void PlayerMovement()
    {
        var movementInput = playerInput.Player.Movement.ReadValue<Vector2>();
        input = new Vector2()
        {
            x = movementInput.x,
            y = movementInput.y
        };

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

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}
