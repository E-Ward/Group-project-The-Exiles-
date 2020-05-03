using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalNavDestinationReached : MonoBehaviour
{
    private AnimalMaster animalMaster;
    private NavMeshAgent myNavMeshAgent;

    private AnimalWanderingAI myAnimalWanderingAI;

    public GameObject uiObject;


    private float checkRate;
    private float nextCheck;

    public bool isTouching;


    public GameObject Player;
    public GameObject ThisAnimal;
    public float Distance_;


    void OnEnable()
    {
        SetInitialReferences();
        animalMaster.EventEnemyDie += DisableThis;   
    }

    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnDisable()
    {
        animalMaster.EventEnemyDie -= DisableThis;
    }

    // Update is called once per frame
    void Update()
    {
        Distance_ = Vector3.Distance(Player.transform.position, ThisAnimal.transform.position);
        if(Distance_ <= 2)
        {
            myAnimalWanderingAI.isWandering = false;
            animalMaster.isNavPaused = true;
            animalMaster.isOnRoute = false;
        }

        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            //CheckIfDestinationReached();
     
        }
    }

    void SetInitialReferences()
    {
        isTouching = false;
        animalMaster = GetComponent<AnimalMaster>();
        myAnimalWanderingAI = GetComponent<AnimalWanderingAI>();
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

   /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = true;
            myAnimalWanderingAI.isWandering = false;
            animalMaster.isNavPaused = true;
            animalMaster.isOnRoute = false;
            animalMaster.CallEventEnemyReachedNavTarget();
        }

        if (collision.gameObject.name == "Player")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }*/
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        //Destroy(uiObject);
        Destroy(gameObject);
    }
    /*void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = false;
            //myWanderingAI.isWandering = true;
            animalMaster.isNavPaused = false;
            //enemyMaster.isOnRoute = false;
            animalMaster.CallEventEnemyReachedNavTarget();
        }
    }*/

    void DisableThis()
    {
        this.enabled = false;
    }

}
