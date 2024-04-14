using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMelee))]
public class EnemyCharacter : MonoBehaviour
{
    private Health healthComponent;
    private GameObject targetObject;
    private EnemyMovement enemyMovement;
    private EnemyMelee enemyMelee;

    [SerializeField]
    private GameObject healthDrop;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMelee = GetComponent<EnemyMelee>();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthChange += TakeDamage;
        healthComponent.OnHealthChangeCallback += AudioManager.Instance.PlayEnemyBattleSound;
    }

    public void SetTarget(GameObject newTarget)
    {
        if (enemyMovement == null)
        {
            Start();
        }
        targetObject = newTarget;
        enemyMovement.MoveTo(targetObject);
        enemyMelee.SetTarget(targetObject);
    }

    private void TakeDamage(float newHealth)
    {
        if (newHealth <= 0)
        {
            if (UnityEngine.Random.Range(0, 4) == 0)
            {
                Instantiate(healthDrop, transform.position, Quaternion.Euler(90, 0, 0));
            }
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
