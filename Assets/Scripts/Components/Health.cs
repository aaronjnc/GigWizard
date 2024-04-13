using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    public event OnHealthChangeDelegate OnHealthChange;
    public delegate void OnHealthChangeDelegate(float newHealth);

    private float currentHealth;

    [SerializeField]
    private float damageCooldown;

    bool bIsOnCooldown = false;

    private float CurrentHealth {
        get
        {
            return currentHealth;
        }
        set
        {
            if (currentHealth == value) return;
            currentHealth = value;
            if (OnHealthChange != null)
                OnHealthChange(currentHealth);
        }
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (!bIsOnCooldown)
        {
            CurrentHealth -= damage;
            StartCoroutine(Cooldown());
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth + amount, maxHealth);
    }

    IEnumerator Cooldown()
    {
        bIsOnCooldown = true;
        yield return new WaitForSeconds(damageCooldown);
        bIsOnCooldown = false;
    }
}
