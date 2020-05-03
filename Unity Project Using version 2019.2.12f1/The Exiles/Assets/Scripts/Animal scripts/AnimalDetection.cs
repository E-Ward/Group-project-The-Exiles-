using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDetection : MonoBehaviour
{

    private AnimalMaster animalMaster;
    private AnimalWanderingAI myAnimalWanderingAI;
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
        animalMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        animalMaster.EventEnemyDie -= DisableThis;
    }

    // Update is called once per frame
    void Update()
    {
        CarryOutDetection();
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * detectRadius, Color.green);
    }

    void SetInitialReferences()
    {
        animalMaster = GetComponent<AnimalMaster>();
        myAnimalWanderingAI = GetComponent<AnimalWanderingAI>();
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
                            break;
                        }
                    }
                }
            }
            else
            {
                animalMaster.CallEventEnemyLostTarget();
                
            }
            
        }
    }

    bool CanPotentialTargetBeSeen (Transform potentialTarget)
    {
        if (Physics.Linecast(head.position,potentialTarget.position, out hit, sightLayer))
        {
            
            if (hit.transform == potentialTarget)
            {
                animalMaster.CallEventEnemySetNavTarget(potentialTarget);
                return true;
            }
            else
            {
                animalMaster.CallEventEnemyLostTarget();
                animalMaster.isOnRoute = false;
                myAnimalWanderingAI.isWandering = true;
                return false;
            }
        }
        else
        {
            animalMaster.CallEventEnemyLostTarget();
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
