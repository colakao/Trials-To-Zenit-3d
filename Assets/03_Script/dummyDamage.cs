using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ouch!");
    }
}
