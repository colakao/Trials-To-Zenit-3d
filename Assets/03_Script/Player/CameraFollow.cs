using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject target;

    [Range(0.01f, 0.3f)]
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref velocity, smoothTime);
    }
}
