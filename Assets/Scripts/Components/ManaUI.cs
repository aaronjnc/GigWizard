using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [SerializeField]
    private Image[] manaImages;
    
    [SerializeField]
    private UICapsule manaCapsules;

    private void Start()
    {
        Mana manaScript = GetComponent<Mana>();

        manaScript.OnManaChange += UpdateMana;
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
