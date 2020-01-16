using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPosition : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
