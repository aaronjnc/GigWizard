using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpell : Spell
{
    public override void Cast()
    {
        base.Cast();
        Instantiate(spellObjectPrefab, transform.position, Quaternion.Euler(90, 0, 0));
    }
}
