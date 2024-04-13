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

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image[] healthImages;

    [SerializeField]
    private UICapsule healthCapsules;

    private void Start()
    {
        Health healthScript = GetComponent<Health>();

        healthScript.OnHealthChange += UpdateHealth;
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
}
