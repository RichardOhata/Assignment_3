using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public LayerMask Ground;


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    private float stuckThreshold = 2f; // seconds
    private float stuckTimer = 0f;

    private GameManager gameManager;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Patrolling();

        if (agent.velocity.magnitude < 0.1f) // Threshold can be adjusted
        {
            stuckTimer += Time.deltaTime;
        }
        else
        {
            stuckTimer = 0f; // Reset timer if the agent moves
        }
        if (stuckTimer > stuckThreshold)
        {
            Debug.Log("Agent is stuck.");
            gameManager.UnStuckEnemy();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }
    }
}
