using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboKey
{
    private float timeLastInput;

    public string[] inputString;
    private int inputStringIndex = 0;

    public ComboKey(string[] s)
    {
        inputString = s;
    }

    public bool Check(float timeDelay)
    {
        if (Time.time > timeLastInput + timeDelay) inputStringIndex = 0;
        {
            if (inputStringIndex < inputString.Length)
            {
                if ((inputString[inputStringIndex] == "lightA" && InputManager.LightAttack())
                    ||(inputString[inputStringIndex] == "heavyA" && InputManager.HeavyAttack())
                    ||(inputString[inputStringIndex] != "lightA" && inputString[inputStringIndex] != "heavyA"))
                {
                    timeLastInput = Time.time;
                    inputStringIndex++;
                }

                if (inputStringIndex >= inputString.Length)
                {
                    inputStringIndex = 0;
                    return true;
                }
                else return false;
            }
        }
        return false;
    }
}
