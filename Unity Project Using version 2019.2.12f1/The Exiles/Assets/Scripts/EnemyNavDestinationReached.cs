using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavDestinationReached : MonoBehaviour
{
    private EnemyMaster enemyMaster;
    private NavMeshAgent myNavMeshAgent;

    private WanderingAI myWanderingAI;
    private PatrollingAI patrollingAI;

    public GameObject uiObject;


    private float checkRate;
    private float nextCheck;

    public bool isTouching;


    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;   
    }

    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            //CheckIfDestinationReached();
     
        }
    }

    void SetInitialReferences()
    {
        isTouching = false;
        enemyMaster = GetComponent<EnemyMaster>();
        myWanderingAI = GetComponent<WanderingAI>();
        patrollingAI = GetComponent<PatrollingAI>();
        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.3f, 0.4f);
    }


    /*void CheckIfDestinationReached()
    {
        if(enemyMaster.isOnRoute)
        {
            if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
            {
                Debug.Log("ThisHappened");
                enemyMaster.isOnRoute = false;
                enemyMaster.CallEventEnemyReachedNavTarget();
            }
        }
       
    }*/

   void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = true;
            myWanderingAI.isWandering = false;
            patrollingAI.isPatrolling = false;
            enemyMaster.isNavPaused = true;
            enemyMaster.isOnRoute = false;
            enemyMaster.CallEventEnemyReachedNavTarget();
        }

        if (collision.gameObject.name == "Player")
        {
            uiObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        //Destroy(uiObject);
        Destroy(gameObject);
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = false;
            //myWanderingAI.isWandering = true;
            enemyMaster.isNavPaused = false;
            //enemyMaster.isOnRoute = false;
            enemyMaster.CallEventEnemyReachedNavTarget();
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }

}
