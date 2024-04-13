using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteFlash))]
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

    private SpriteFlash flashScript;

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
        flashScript = GetComponent<SpriteFlash>();
    }

    public void DealDamage(float damage)
    {
        if (!bIsOnCooldown)
        {
            CurrentHealth -= damage;
            flashScript.StartFlashCoroutine(new UnityEngine.InputSystem.InputAction.CallbackContext());
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
