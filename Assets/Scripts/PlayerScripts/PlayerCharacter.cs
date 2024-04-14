using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthUI))]
public class PlayerCharacter : Singleton<PlayerCharacter>
{
    private PlayerControls controls;
    private Health healthComponent;
    private PlayerMovement playerMovement;
    private SpellManager spellManager;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private RuntimeAnimatorController deathAnimation;

    [SerializeField]
    private Sprite deathSprite;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
        spellManager = GetComponentInChildren<SpellManager>();
        playerMovement.SetupControls(controls);
        if (spellManager != null)
            spellManager.SetupControls(controls);
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        healthComponent.OnHealthChange += UpdateHealth;
    }

    public void UpdateHealth(float healthRemaining)
    {
        if (healthRemaining <= 0)
        {
            DisableControls();
            animator.enabled = true;
            animator.runtimeAnimatorController = deathAnimation;
            StartCoroutine(DeathAnim());
        }
    }

    public void DisableControls()
    {
        controls.Disable();
    }

    public void EnableControls()
    {
        controls.Enable();
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(deathAnimation.animationClips[0].length);
        spriteRenderer.sprite = deathSprite;
        GameManager.Instance.LoseGame();
    }

    private void OnDestroy()
    {
        DisableControls();
    }

    public void RegainMana()
    {
        spellManager.RegainMana();
    }
}
