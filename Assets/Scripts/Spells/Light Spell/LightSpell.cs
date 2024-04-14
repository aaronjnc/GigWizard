using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSpell : Spell
{
    public override void Cast()
    {
        base.Cast();
        Instantiate(spellObjectPrefab, transform.position, Quaternion.Euler(90, 0, 0));
    }
}