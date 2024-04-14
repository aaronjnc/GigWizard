using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class WateringCan : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    RuntimeAnimatorController animatorController;

    [SerializeField]
    private float healthRestoration;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = animatorController;
        StartCoroutine(WaterFlower());
    }

    IEnumerator WaterFlower()
    {
        yield return new WaitForSeconds(animatorController.animationClips[0].length);
        Flower.Instance.gameObject.GetComponent<Health>().Heal(healthRestoration);
        Destroy(gameObject);
    }
}
