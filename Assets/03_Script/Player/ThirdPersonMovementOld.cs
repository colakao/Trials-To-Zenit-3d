using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementOld : MonoBehaviour
{
    [Range(1f,15f)]
    public float speed;
    [Range(10f, 100f)]
    public float turnSpeed;
    private float gravity = 9.81f;

    Camera mainCamera;
    CharacterController cc;

    Vector2 input;

    Vector3 camF;
    Vector3 camR;

    bool isMoving;
    bool isGrounded;

    private void Awake()
    {
        mainCamera = Camera.main;
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
        IsGrounded();
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

    private bool IsGrounded()
    {
        
        if (Physics.Raycast(cc.bounds.center, Vector3.down, out RaycastHit hit, (cc.bounds.extents.y + 0.1f)))
        {
            Debug.DrawRay(cc.bounds.center, Vector3.down * (cc.bounds.extents.y + 0.1f), Color.green);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(cc.bounds.center, Vector3.down * (cc.bounds.extents.y + 0.1f), Color.red);
            isGrounded = false;
        }
  
        return hit.collider != null;
    }
}
