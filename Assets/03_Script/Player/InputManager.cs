using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerAttack playerAttack;
    ThirdPersonMovement tpm;

    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAttack.LightAttack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerAttack.HeavyAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAttack.FirstAbility();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerAttack.SecondAbility();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAttack.Dash();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerAttack.UltimateAbility();
        }


    }
}
