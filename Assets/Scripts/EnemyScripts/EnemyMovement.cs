using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private float enemyMinDistance;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = PlayerMovement.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
