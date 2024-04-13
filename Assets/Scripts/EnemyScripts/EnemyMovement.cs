using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private MovementAnimator animator;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private float enemyMinDistance;
    private GameObject enemyTarget;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<MovementAnimator>();
        agent.stoppingDistance = enemyMinDistance;
        agent.speed = enemySpeed;
        agent.updateRotation = false;
        MoveTo(PlayerMovement.Instance.gameObject);
    }

    public void MoveTo(GameObject target)
    {
        enemyTarget = target;
        agent.SetDestination(enemyTarget.transform.position);
    }

    private void FixedUpdate()
    {
        if (enemyTarget && enemyTarget.transform.position != agent.destination)
        {
            agent.SetDestination(enemyTarget.transform.position);
        }
        if (agent.velocity.x < 0)
        {
            animator.Move(DirectionEnum.Left);
        }
        else
        {
            animator.Move(DirectionEnum.Right);
        }
    }
}
