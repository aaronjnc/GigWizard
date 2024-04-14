using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[Serializable]
public struct SpellOption
{
    public string spellName;
    public Spell spellScript;
    public Sprite spellSprite;
    public Sprite spellCooldown;
    public float manaCost;
    public float cooldownTime;
}

[RequireComponent(typeof(Mana))]
public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private Image[] spellCooldownImages;

    private PlayerControls controls;
    private Mana manaComponent;

    [SerializeField]
    private List<SpellOption> spellOptions = new List<SpellOption>();
    private List<int> visibleSpells = new List<int>(); //1 is currently selected spell
    private List<int> spellCooldowns = new List<int>();

    private void Awake()
    {
        manaComponent = GetComponent<Mana>();
        controls = new PlayerControls();
        controls.Combat.CastSpell.started += CastSpell;
        controls.Combat.CastSpell.Enable();
        controls.Combat.CycleSpells.started += CycleSpells;
        controls.Combat.CycleSpells.Enable();
        for (int i = 0; i < spellCooldownImages.Length; i++)
        {
            visibleSpells.Add(i);
            spellCooldownImages[i].sprite = spellOptions[i].spellSprite;
        }
    }

    void CastSpell(CallbackContext ctx)
    {
        if (!spellCooldowns.Contains(visibleSpells[1]) && manaComponent.HasEnoughMana(spellOptions[visibleSpells[1]].manaCost))
        {
            manaComponent.SpendMana(spellOptions[visibleSpells[1]].manaCost);
            spellOptions[visibleSpells[1]].spellScript.Cast();
            spellCooldownImages[1].sprite = spellOptions[visibleSpells[1]].spellCooldown;
            StartCoroutine(Cooldown(visibleSpells[1]));
        }
    }

    void CycleSpells(CallbackContext ctx)
    {
        for (int i = 0; i < visibleSpells.Count; i++)
        {
            visibleSpells[i] += (int)ctx.ReadValue<float>();
            if (visibleSpells[i] < 0)
            {
                visibleSpells[i] = spellOptions.Count - 1;
            }
            else if (visibleSpells[i] == spellOptions.Count)
            {
                visibleSpells[i] = 0;
            }
            if (spellCooldowns.Contains(visibleSpells[i]))
            {
                spellCooldownImages[i].sprite = spellOptions[visibleSpells[i]].spellCooldown;
            }
            else
            {
                spellCooldownImages[i].sprite = spellOptions[visibleSpells[i]].spellSprite;
            }
        }
    }

    IEnumerator Cooldown(int spellNum)
    {
        spellCooldowns.Add(spellNum);
        yield return new WaitForSeconds(spellOptions[spellNum].cooldownTime);
        spellCooldowns.Remove(spellNum);
        if (visibleSpells.Contains(spellNum))
        {
            int idx = visibleSpells.IndexOf(spellNum);
            spellCooldownImages[idx].sprite = spellOptions[spellNum].spellSprite;
        }
    }
}
