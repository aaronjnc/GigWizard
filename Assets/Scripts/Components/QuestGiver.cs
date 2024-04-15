using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class QuestGiver : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite preQuestSprite;

    [SerializeField]
    private Sprite postQuestSprite;

    [SerializeField]
    private GameObject familiar;

    [SerializeField]
    private Quests questEnum;

    Collider triggerCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        triggerCollider = GetComponent<Collider>();
        bool bQuestCompleted = GameManager.Instance.GetQuestCompleted(questEnum);
        if (bQuestCompleted)
        {
            triggerCollider.enabled = false;
            spriteRenderer.sprite = postQuestSprite;
            familiar.SetActive(true);
        }
        else
        {
            triggerCollider.enabled = true;
            spriteRenderer.sprite = preQuestSprite;
            familiar.SetActive(false);
        }
    }
}
