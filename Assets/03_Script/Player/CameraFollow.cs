using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    ThirdPersonCamera cameraMain;

    [Range(0.01f, 0.3f)]
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    void Start()
    {
        cameraMain = GameObject.FindObjectOfType<ThirdPersonCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }


    private void FixedUpdate()
    {
        var updatedOffset = offset + cameraMain.newOffset;
        Vector3 targetCamPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref velocity, smoothTime);
    }
}
