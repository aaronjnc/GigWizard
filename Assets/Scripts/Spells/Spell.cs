using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    protected GameObject spellObjectPrefab;

    public virtual void Cast() { }

}
