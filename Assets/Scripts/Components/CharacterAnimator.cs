using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum DirectionEnum
{
    Left,
    Right,
    Up,
    Down,
}

[Serializable]
public struct SpriteStruct
{
    public DirectionEnum direction;
    public Sprite baseDirectionSprite;
    public RuntimeAnimatorController controller;
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    private DirectionEnum currentDirection = DirectionEnum.Down;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    private List<SpriteStruct> movementSprites;
    [SerializeField]
    private List<SpriteStruct> attackSprites;
    [SerializeField]
    private List<SpriteStruct> damageSprites;

    Dictionary<DirectionEnum, SpriteStruct> directionalMovement = new Dictionary<DirectionEnum, SpriteStruct>();
    Dictionary<DirectionEnum, SpriteStruct> directionalAttack = new Dictionary<DirectionEnum, SpriteStruct>();
    Dictionary<DirectionEnum, SpriteStruct> directionalDamage = new Dictionary<DirectionEnum, SpriteStruct>();

    private bool bPrevEnabled = false;

    private bool bOverridingAnimation = false;

    int i = 0;

    void Start()
    {
        if (animator != null)
        {
            return;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        foreach (SpriteStruct spriteStruct in movementSprites)
        {
            directionalMovement.Add(spriteStruct.direction, spriteStruct);
        }
        foreach (SpriteStruct spriteStruct in attackSprites)
        {
            directionalAttack.Add(spriteStruct.direction, spriteStruct);
        }
        foreach (SpriteStruct spriteStruct in damageSprites)
        {
            directionalDamage.Add(spriteStruct.direction, spriteStruct);
        }
        spriteRenderer.sprite = directionalMovement[currentDirection].baseDirectionSprite;
        animator.runtimeAnimatorController = directionalMovement[currentDirection].controller;
        animator.enabled = false;
    }

    public void Move(DirectionEnum direction)
    {
        if (bOverridingAnimation)
        {
            return;
        }
        if (animator == null)
        {
            Start();
        }
        animator.enabled = true;
        if (direction == currentDirection)
        {
            return;
        }
        currentDirection = direction;
        if (directionalMovement.ContainsKey(direction))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = directionalMovement[currentDirection].baseDirectionSprite;
            animator.runtimeAnimatorController = directionalMovement[currentDirection].controller;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void StopMove()
    {
        animator.enabled = false;
        spriteRenderer.sprite = directionalMovement[currentDirection].baseDirectionSprite;
    }

    public void Damage()
    {
        if (animator == null)
        {
            Start();
        }
        if (directionalDamage.Count == 0)
        {
            return;
        }
        bOverridingAnimation = true;
        bPrevEnabled = animator.enabled;
        animator.enabled = true;
        animator.runtimeAnimatorController = directionalDamage[DirectionEnum.Right].controller;
        StartCoroutine(WaitForEndOfAnimation(directionalDamage[DirectionEnum.Right].controller));
    }

    public void Attack()
    {
        if (animator == null)
        {
            Start();
        }
        if (directionalAttack.Count == 0)
        {
            return;
        }
        bOverridingAnimation = true;
        bPrevEnabled = animator.enabled;
        animator.enabled = true;
        animator.runtimeAnimatorController = directionalAttack[DirectionEnum.Right].controller;
        StartCoroutine(WaitForEndOfAnimation(directionalAttack[DirectionEnum.Right].controller));
    }

    IEnumerator WaitForEndOfAnimation(RuntimeAnimatorController controller)
    {
        yield return new WaitForSeconds(controller.animationClips[0].length);
        bOverridingAnimation = false;
        animator.enabled = bPrevEnabled;
        spriteRenderer.sprite = directionalMovement[currentDirection].baseDirectionSprite;
        animator.runtimeAnimatorController = directionalMovement[currentDirection].controller;
    }
}
