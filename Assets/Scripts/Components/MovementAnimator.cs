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
public class MovementAnimator : MonoBehaviour
{
    private DirectionEnum currentDirection = DirectionEnum.Down;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    private List<SpriteStruct> sprites;

    Dictionary<DirectionEnum, SpriteStruct> directionalSprites = new Dictionary<DirectionEnum, SpriteStruct>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        foreach (SpriteStruct spriteStruct in sprites)
        {
            directionalSprites.Add(spriteStruct.direction, spriteStruct);
        }
        spriteRenderer.sprite = directionalSprites[currentDirection].baseDirectionSprite;
        animator.runtimeAnimatorController = directionalSprites[currentDirection].controller;
        animator.enabled = false;
    }

    public void Move(DirectionEnum direction)
    {
        animator.enabled = true;
        if (direction == currentDirection)
        {
            return;
        }
        currentDirection = direction;
        spriteRenderer.sprite = directionalSprites[currentDirection].baseDirectionSprite;
        animator.runtimeAnimatorController = directionalSprites[currentDirection].controller;
    }

    public void StopMove()
    {
        animator.enabled = false;
        spriteRenderer.sprite = directionalSprites[currentDirection].baseDirectionSprite;
    }
}
