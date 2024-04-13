using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    protected GameObject spellObjectPrefab;

    [SerializeField]
    protected float manaCost;

    protected Mana manaComponent;

    private void Start()
    {
        manaComponent = GetComponentInParent<Mana>();
    }

    public virtual void Cast() { }

    public float GetManaCost()
    {
        return manaCost;
    }

}
