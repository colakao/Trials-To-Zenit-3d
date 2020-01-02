using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float timeBetweenInput = 0.3f;

    private ComboKey lightCombo = new ComboKey(new string[] { "lightA", "lightA", "lightA" });
    private ComboKey heavyCombo = new ComboKey(new string[] { "heavyA", "heavyA", "heavyA" });

    private void Update()
    {
        if (lightCombo.Check(timeBetweenInput))
        {
            Debug.Log("Ligh Combo!");
        }

        if (heavyCombo.Check(timeBetweenInput))
        {
            Debug.Log("Heavy Combo!");
        }
    }
}
