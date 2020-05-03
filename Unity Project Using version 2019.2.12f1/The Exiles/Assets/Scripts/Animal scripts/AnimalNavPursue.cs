using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalNavPursue : MonoBehaviour
{
    private AnimalMaster animalMaster;
    private AnimalWanderingAI myAnimalWanderAI;
    private NavMeshAgent myNavMeshAgent;
    private float checkRate;
    private float nextCheck;

   
    

    void OnEnable()
    {
        SetInitialReferences();
        animalMaster.EventEnemyDie += DisableThis;
        
    }

    void OnDisable()
    {
        animalMaster.EventEnemyDie -= DisableThis;
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
        animalMaster = GetComponent<AnimalMaster>();
        myAnimalWanderAI = GetComponent<AnimalWanderingAI>();
        if(GetComponent<NavMeshAgent>() !=null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.1f, 0.2f);
    }

    void TryToChaseTarget()
    {
        if(animalMaster.myTarget !=null && myNavMeshAgent !=null && !animalMaster.isNavPaused)
        {
            myNavMeshAgent.SetDestination(animalMaster.myTarget.position);

            animalMaster.isfollowingplayer = true;

            if (myNavMeshAgent.remainingDistance>myNavMeshAgent.stoppingDistance)
            {
                animalMaster.CallEventEnemyWalking();
                animalMaster.CallEventFollowingPlayer();
                
                animalMaster.isOnRoute = true;
                
                myAnimalWanderAI.isWandering = false;
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
