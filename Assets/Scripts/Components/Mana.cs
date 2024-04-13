using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private float maxMana;

    public event OnManaChangeDelegate OnManaChange;
    public delegate void OnManaChangeDelegate(float newMana);

    private float currentMana;

    private float CurrentMana {
        get
        {
            return currentMana;
        }
        set
        {
            if (currentMana == value) return;
            currentMana = value;
            if (OnManaChange != null)
                OnManaChange(currentMana);
        }
    }

    protected void Awake()
    {
        CurrentMana = maxMana;
    }

    public void DealDamage(float damage)
    {
        CurrentMana -= damage;
    }

    public void Heal(float amount)
    {
        CurrentMana = Mathf.Max(currentMana + amount, maxMana);
    }
}
