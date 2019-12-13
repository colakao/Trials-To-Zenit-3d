using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "New Ability";
    public Sprite abiltiySprite;
    public AudioClip abilitySound;
    public float abilityBaseCoolDown;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
