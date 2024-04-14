using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpell : Spell
{
    public override void Cast()
    {
        base.Cast();
        Instantiate(spellObjectPrefab, Flower.Instance.transform.position + new Vector3(-0.15f, 0.1f, 0), Quaternion.Euler(90, 0, 0));
    }
}
