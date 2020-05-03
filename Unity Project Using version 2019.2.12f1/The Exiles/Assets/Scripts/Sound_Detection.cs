using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sound_Detection : MonoBehaviour
{
    private NavMeshAgent myNavMeshAgent;
    public bool enter = false;
    public bool exit = true;

    public Collider boxCollider;

    public GateOpener gateOpen;

    //public Transform mySoundTarget;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();

        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        //mySoundTarget = gateOpen.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "sound" && gateOpen.isSoundPlaying == true)
        {
            Debug.Log("Bois were in!!!");
            enter = true;
            exit = false;

            myNavMeshAgent.SetDestination(gateOpen.transform.position);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "sound")
        {
            enter = false;
        }
    }



}
