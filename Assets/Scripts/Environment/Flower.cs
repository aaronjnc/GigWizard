using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class Flower : Singleton<Flower>
{
    private SpriteRenderer spriteRenderer;
    private Health healthComponent;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthChange += UpdateHealth;
    }

    public void UpdateSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    public void UpdateHealth(float newHealth)
    {
        if (newHealth <= 0)
        {
            GameManager.Instance.LoseGame();
        }
    }
}
