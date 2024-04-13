using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(LightSpell))]
[RequireComponent(typeof(Mana))]
public class SpellManager : MonoBehaviour
{
    private PlayerControls controls;
    private LightSpell lightSpell;
    private Mana manaComponent;

    private void Awake()
    {
        lightSpell = GetComponent<LightSpell>();
        manaComponent = GetComponent<Mana>();
        controls = new PlayerControls();
        controls.Combat.LightSpell.started += LightSpell;
        controls.Combat.LightSpell.Enable();
    }

    void LightSpell(CallbackContext ctx)
    {
        if (manaComponent.HasEnoughMana(lightSpell.GetManaCost()))
        {
            lightSpell.Cast();
        }
    }
}
