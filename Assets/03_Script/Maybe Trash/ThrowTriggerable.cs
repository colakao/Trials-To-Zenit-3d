using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTriggerable : MonoBehaviour
{
    [HideInInspector] public float throwForce = 30f;

    public Transform projectileOrigin;
    [HideInInspector] public GameObject projectilePrefab;
    [HideInInspector] public Rigidbody2D projectileRB;

    public void Throw()
    {
        Rigidbody2D clonedProjectile = Instantiate(projectileRB, projectileOrigin.position, transform.rotation) as Rigidbody2D;

        clonedProjectile.AddForce(projectileOrigin.up * throwForce, ForceMode2D.Impulse);
    }
}
