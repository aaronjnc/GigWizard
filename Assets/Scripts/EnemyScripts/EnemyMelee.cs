using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float minAttackDistance;

    [SerializeField]
    private float attackTime;

    private bool bAttackReady = true;

    private GameObject targetObject;

    private Health targetHealthComponent;

    public void SetTarget(GameObject newTarget)
    {
        targetObject = newTarget;
        targetHealthComponent = targetObject.GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (bAttackReady && (targetObject.transform.position - transform.position).magnitude <= minAttackDistance && targetHealthComponent.GetIsAlive())
        {
            targetHealthComponent.DealDamage(damage);
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        bAttackReady = false;
        yield return new WaitForSeconds(attackTime);
        bAttackReady = true;
    }
}
