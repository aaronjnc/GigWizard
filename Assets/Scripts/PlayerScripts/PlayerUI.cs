using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct UICapsule
{
    public Sprite EmptyCapsule;
    public Sprite HalfCapsule;
    public Sprite FullCapsule;
}

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image[] healthImages;

    [SerializeField]
    private Image[] manaImages;

    [SerializeField]
    private UICapsule healthCapsules;

    [SerializeField]
    private UICapsule manaCapsules;

    private void Start()
    {
        Health healthScript = GetComponent<Health>();
        Mana manaScript = GetComponent<Mana>();

        healthScript.OnHealthChange += UpdateHealth;
        manaScript.OnManaChange += UpdateMana;
        Debug.Log("Link change");
    }

    private void UpdateHealth(float newHealth)
    {
        int i = 0;
        for (i = 0; i < (int)newHealth; i++)
        {
            healthImages[i].sprite = healthCapsules.FullCapsule;
        }
        if (newHealth - i == 0.5)
        {
            healthImages[i++].sprite = healthCapsules.HalfCapsule;
        }
        for (int j = i; j < healthImages.Length; j++)
        {
            healthImages[j].sprite = healthCapsules.EmptyCapsule;
        }
    }

    private void UpdateMana(float newMana)
    {
        int i = 0;
        for (i = 0; i < (int)newMana; i++)
        {
            manaImages[i].sprite = manaCapsules.FullCapsule;
        }
        if (newMana - i == 0.5)
        {
            manaImages[i++].sprite = manaCapsules.HalfCapsule;
        }
        for (int j = i; j < manaImages.Length; j++)
        {
            manaImages[j].sprite = manaCapsules.EmptyCapsule;
        }
    }
}
