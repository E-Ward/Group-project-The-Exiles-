using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalNavWander : MonoBehaviour
{
    private AnimalMaster animalMaster;
    private NavMeshAgent myNavMeshAgent;
    private float checkRate;
    private float nextCheck;
    private float wanderRange = 10;
    private Transform myTransform;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;

    


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
        if (Time.time > nextCheck)
        {
            //nextCheck = Time.time + checkRate;
            CheckIfIShouldWander();

        }
    }
    void SetInitialReferences()
    {
        animalMaster = GetComponent<AnimalMaster>();
        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.3f, 0.4f);
        myTransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if (animalMaster.myTarget == null && !animalMaster.isOnRoute && !animalMaster.isNavPaused)
        {

            if(RandomWanderTarget(myTransform.position, wanderRange,out wanderTarget))
            {
                myNavMeshAgent.SetDestination(wanderTarget);
                animalMaster.isOnRoute = true;
                animalMaster.CallEventEnemyWalking();
            }
            
        }
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if(NavMesh.SamplePosition(randomPoint, out navHit,1.0f, NavMesh.AllAreas))
        {
            
            result = navHit.position;
            return true;
        }
        else
        {
          
            result = centre;
            return false;
            
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }

}
