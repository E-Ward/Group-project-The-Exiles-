using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemyDetection : MonoBehaviour
{

    private EnemyMaster enemyMaster;
    private WanderingAI myWanderingAI;
    private PatrollingAI patrollingAI;
    private Transform myTransform;
    public Transform head;
    public LayerMask playerLayer;
    public LayerMask sightLayer;
    private float checkRate;
    private float nextCheck;
    public float detectRadius = 80;
    private RaycastHit hit;


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
        CarryOutDetection();
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * detectRadius, Color.green);
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        myWanderingAI = GetComponent<WanderingAI>();
        patrollingAI = GetComponent<PatrollingAI>();
        myTransform = transform;

        if(head == null)
        {
            head = myTransform;
        }

        checkRate = Random.Range(0.8f, 1.2f);

    }

    void CarryOutDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);

            if (colliders.Length>0)
            {
                foreach(Collider potentialTargetCollider in colliders)
                {
                    if (potentialTargetCollider.CompareTag(GameManager_References._playerTag))
                    {
                        if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            Debug.Log("Can see player.");
                            break;
                        }
                    }
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                Debug.Log("Lost player.");
            }
            
        }
    }

    bool CanPotentialTargetBeSeen (Transform potentialTarget)
    {
        if (Physics.Linecast(head.position,potentialTarget.position, out hit, sightLayer))
        {
            
            if (hit.transform == potentialTarget)
            {
                Debug.Log("Calling set nav target.");
                enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                return true;
            }
            else
            {
                Debug.Log("Calling event enemy lost target and setting patrolling to true.");
                enemyMaster.CallEventEnemyLostTarget();
                enemyMaster.isOnRoute = false;
                // myWanderingAI.isWandering = true;
                patrollingAI.isPatrolling = true;
                return false;
            }
        }
        else
        {
            Debug.Log("Calling event enemy lost target.");
            enemyMaster.CallEventEnemyLostTarget();
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
