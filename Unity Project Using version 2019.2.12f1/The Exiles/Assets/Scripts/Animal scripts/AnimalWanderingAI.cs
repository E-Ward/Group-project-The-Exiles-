using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderingAI : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public bool isWandering;

    private NavMeshAgent agent;
    private AnimalNavDestinationReached animalNavDestinationReached;
    private float timer;

    public Animator anim;

    //Start is called before the first frame update
    void OnEnable()
    {
        SetInitialReferences();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        isWandering = true;
    }

    void SetInitialReferences()
    {
        animalNavDestinationReached = GetComponent<AnimalNavDestinationReached>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            if (animalNavDestinationReached.isTouching == false && isWandering == true)
            {
                //anim.SetInteger("walkCondition", 1);
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
