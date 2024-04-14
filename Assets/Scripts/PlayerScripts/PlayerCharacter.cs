using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthUI))]
public class PlayerCharacter : Singleton<PlayerCharacter>
{
    private Health healthComponent;
    private PlayerMovement playerMovement;
    private SpellManager spellManager;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private RuntimeAnimatorController deathAnimation;

    [SerializeField]
    private Sprite deathSprite;

    private Animator animator;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spellManager = GetComponentInChildren<SpellManager>();
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        healthComponent.OnHealthChange += UpdateHealth;
    }

    public void UpdateHealth(float healthRemaining)
    {
        if (healthRemaining <= 0)
        {
            playerMovement.Die();
            spellManager.Die();
            animator.enabled = true;
            animator.runtimeAnimatorController = deathAnimation;
            StartCoroutine(DeathAnim());
        }
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(deathAnimation.animationClips[0].length);
        spriteRenderer.sprite = deathSprite;
    }
}
