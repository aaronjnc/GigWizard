using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectSpell : Spell
{
    public override void Cast()
    {
        base.Cast();
        Instantiate(spellObjectPrefab, Flower.Instance.transform.position + new Vector3(0.026f, -0.061f, -0.182f), Quaternion.Euler(90, 0, 0));
    }
}
