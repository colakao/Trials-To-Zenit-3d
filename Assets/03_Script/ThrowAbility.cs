using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ThrowAbility")]
public class ThrowAbility : Ability
{
    [Tooltip("La distancia maxima es infinita con 0.")]
    public float maxDistance = 0;

    public float throwForce = 100f;
    public Rigidbody2D projectile;

    public bool reactivate = false;

    private ThrowTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ThrowTriggerable>();
        launcher.throwForce = throwForce;
        launcher.projectileRB = projectile;
    }

    public override void TriggerAbility()
    {
        launcher.Throw();
    }
}
