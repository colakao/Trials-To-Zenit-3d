using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxToggler : MonoBehaviour
{
    public GameObject sword;
    private Collider swordHitBox;

    private void Awake()
    {
        swordHitBox = sword.GetComponent<Collider>();
    }

    void Toggle()
    {
        swordHitBox.enabled = !swordHitBox.enabled;
    }

    void Activate()
    {
        swordHitBox.enabled = true;
    }

    void Deactivate()
    {
        swordHitBox.enabled = false;
    }

}
