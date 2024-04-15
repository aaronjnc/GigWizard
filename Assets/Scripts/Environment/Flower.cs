using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class Flower : Singleton<Flower>
{
    private SpriteRenderer spriteRenderer;
    private Health healthComponent;
    [SerializeField]
    private GameObject familiar;

    [SerializeField]
    private Quests currentQuest;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthChange += UpdateHealth;
        familiar.SetActive(false);
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

    public GameObject SpawnFamiliar()
    {
        GameManager.Instance.CompleteQuest(currentQuest);
        familiar.SetActive(true);
        return familiar;
    }
}
