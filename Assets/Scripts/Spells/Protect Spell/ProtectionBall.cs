using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ProtectionBall : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    RuntimeAnimatorController animatorController;

    [SerializeField]
    private float protectionTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
        StartCoroutine(WaterFlower());
    }

    IEnumerator WaterFlower()
    {
        yield return new WaitForSeconds(animatorController.animationClips[0].length);
        StartCoroutine(Flower.Instance.gameObject.GetComponent<Health>().Cooldown(protectionTime));
        StartCoroutine(DestroyProtection());
    }

    IEnumerator DestroyProtection()
    {
        yield return new WaitForSeconds(protectionTime);
        Destroy(gameObject);
    }
}
