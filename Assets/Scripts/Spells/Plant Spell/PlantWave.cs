using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWave : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private Collider ballCollider;

    [SerializeField]
    RuntimeAnimatorController loopingController;

    [SerializeField]
    RuntimeAnimatorController explosionController;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float blastRadius;

    [SerializeField]
    private LayerMask hitLayer;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
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
        ballCollider.enabled = false;
        animator.runtimeAnimatorController = explosionController;
        StartCoroutine(DestroyObject(other.gameObject));
    }

    IEnumerator DestroyObject(GameObject hitObj)
    {
        Explode(hitObj);
        yield return new WaitForSeconds(explosionController.animationClips[0].length);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    {
        yield return new WaitForSeconds(loopingController.animationClips[0].length);
        rb.velocity = Vector3.zero;
        ballCollider.enabled = false;
        animator.runtimeAnimatorController = explosionController;
        StartCoroutine(DestroyObject(null));
    }

    private void Explode(GameObject hitObj)
    {
        Health healthComponent;
        if (hitObj != null)
        {
            healthComponent = hitObj.GetComponent<Health>();
            if (healthComponent != null) {
                healthComponent.DealDamage(1.0f);
            }
        }
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, blastRadius, hitLayer);
        foreach (Collider col in hitObjects)
        {
            if (col.gameObject.Equals(hitObj))
                continue;
            healthComponent = col.gameObject.GetComponent<Health>();
            if (healthComponent != null) {
                healthComponent.DealDamage(0.5f);
            }
        }
    }
}
