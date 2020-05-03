using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Handles enemy chasing the player.
/// 
/// Author: Ethan Ward
/// </summary>
public class EnemyNavPursue : MonoBehaviour
{
    private EnemyMaster enemyMaster;
    private WanderingAI myWanderAI;
    private PatrollingAI patrollingAI;
    private NavMeshAgent myNavMeshAgent;
    private float checkRate;
    private float nextCheck;
    

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            TryToChaseTarget();
        }
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        myWanderAI = GetComponent<WanderingAI>();
        patrollingAI = GetComponent<PatrollingAI>();
        if (GetComponent<NavMeshAgent>() !=null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.1f, 0.2f);
    }

    public void TryToChaseTarget()
    {
        if(enemyMaster.myTarget !=null && myNavMeshAgent !=null && !enemyMaster.isNavPaused)
        {
            myNavMeshAgent.SetDestination(enemyMaster.myTarget.position);

            if(myNavMeshAgent.remainingDistance>myNavMeshAgent.stoppingDistance)
            {
                enemyMaster.CallEventEnemyWalking();
                enemyMaster.CallEventFollowingPlayer();
                enemyMaster.isOnRoute = true;

                Debug.Log("Disabling patrol.");
                myWanderAI.isWandering = false;
                patrollingAI.isPatrolling = false;
            }
        }
    }

    void DisableThis()
    {
        if (myNavMeshAgent !=null)
        {
            myNavMeshAgent.enabled = false;
        }

        this.enabled = false;
    }
}
