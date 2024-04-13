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

    [SerializeField]
    private float manaRecoverySpeed;

    bool bRecovering = false;

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

    public bool HasEnoughMana(float manaCost)
    {
        return currentMana >= manaCost;
    }

    public void SpendMana(float manaCost)
    {
        CurrentMana -= manaCost;
        if (!bRecovering)
        {
            bRecovering = true;
            StartCoroutine(RecoverMana());
        }
    }

    public void RegainMana(float amount)
    {
        CurrentMana = Mathf.Max(currentMana + amount, maxMana);
    }

    IEnumerator RecoverMana()
    {
        yield return new WaitForSeconds(manaRecoverySpeed);
        RegainMana(0.5f);
        bRecovering = false;
        if (CurrentMana < maxMana)
        {
            bRecovering = true;
            StartCoroutine(RecoverMana());
        }
    }
}
