using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Models a wandering AI behaviour.
/// 
/// Author: Ethan Ward
/// </summary>
public class WanderingAI : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public bool isWandering;

    private NavMeshAgent agent;
    private EnemyNavDestinationReached enemyNavDestinationReached;
    private float timer;

    /// <summary>
    /// Called when this component is enabled.
    /// </summary>
    void OnEnable()
    {
        SetInitialReferences();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        isWandering = true;
    }

    void SetInitialReferences()
    {
        enemyNavDestinationReached = GetComponent<EnemyNavDestinationReached>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            if (enemyNavDestinationReached.isTouching == false && isWandering == true)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
