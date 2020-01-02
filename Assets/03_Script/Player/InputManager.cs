using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public static bool LightAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool HeavyAttack()
    {
        return Input.GetMouseButtonDown(1);
    }

    public static bool DashAttack()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public static bool FirstAbility()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public static bool SecondAbility()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public static bool UltimateAbility()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    public static bool Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
