using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class LightBall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody rb;
    private Collider collider;

    [SerializeField]
    RuntimeAnimatorController loopingController;

    [SerializeField]
    RuntimeAnimatorController explosionController;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float timeAlive;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        rb.useGravity = false;
        rb.angularDrag = 0;
        rb.drag = 0;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.nearClipPlane + 1;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.y = transform.position.y;
        Vector3 dir = mouseWorldPos - transform.position;
        SetDirection(dir.normalized);
        StartCoroutine(TimeAlive());
    }

    public void SetDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.Euler(Quaternion.LookRotation(direction).eulerAngles + new Vector3(90, -90, 0));
        rb.velocity = direction * speed;
        animator.runtimeAnimatorController = loopingController;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        collider.enabled = false;
        animator.runtimeAnimatorController = explosionController;
        Health healthComponent = other.gameObject.GetComponent<Health>();
        if (healthComponent != null) {
            healthComponent.DealDamage(damage);
        }
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(explosionController.animationClips[0].length);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    {
        yield return new WaitForSeconds(timeAlive);
        rb.velocity = Vector3.zero;
        collider.enabled = false;
        animator.runtimeAnimatorController = explosionController;
        StartCoroutine(DestroyObject());
    }
}
