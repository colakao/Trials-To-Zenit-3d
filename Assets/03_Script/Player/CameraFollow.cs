using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    [Range(0.01f, 0.3f)]
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref velocity, smoothTime);
    }
}
