using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteFlash))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    public event OnHealthChangeDelegate OnHealthChange;
    public event OnHealthChangeCallbackDelegate OnHealthChangeCallback;
    public delegate void OnHealthChangeDelegate(float newHealth);
    public delegate void OnHealthChangeCallbackDelegate();

    private float currentHealth;

    [SerializeField]
    private float damageCooldown;

    bool bIsOnCooldown = false;

    private SpriteFlash flashScript;
    private CharacterAnimator characterAnimator;

    private bool bIsAlive = true;

    private float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (currentHealth == value) return;
            currentHealth = value;
            if (OnHealthChange != null)
            {
                OnHealthChange(currentHealth);
                if (OnHealthChangeCallback != null) 
                    OnHealthChangeCallback();
            }
        }
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        flashScript = GetComponent<SpriteFlash>();
    }

    public void DealDamage(float damage)
    {
        if (!bIsOnCooldown)
        {
            CurrentHealth = Mathf.Max(currentHealth - damage, 0);
            if (currentHealth == 0)
            {
                bIsAlive = false;
                return;
            }
            if (characterAnimator != null)
                characterAnimator.Damage();
            flashScript.StartFlashCoroutine(new UnityEngine.InputSystem.InputAction.CallbackContext());
            StartCoroutine(Cooldown(damageCooldown));
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
    }

    public bool GetIsAlive()
    {
        return bIsAlive;
    }

    public IEnumerator Cooldown(float cooldownTime)
    {
        bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        bIsOnCooldown = false;
    }
}
