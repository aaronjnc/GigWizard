using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private float healthReturn;

    private void OnTriggerEnter(Collider other)
    {
        Health healthComponent = other.GetComponent<Health>();
        if (healthComponent != null )
        {
            healthComponent.Heal(healthReturn);
            Destroy(gameObject);
        }
    }
}
