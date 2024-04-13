using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Health))]
public class EnemyCharacter : MonoBehaviour
{
    private Health healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthChange += TakeDamage;
    }

    private void TakeDamage(float newHealth)
    {
        if (newHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RemoveEnemy(gameObject);
        }
    }
}
