using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Implements the animal 'wait' behaviour.
/// 
/// Author: Ethan Ward
/// </summary>
public class Wait : MonoBehaviour
{
    public GameObject Animal1;
    public bool isAnimalWaiting;

    public GameObject sittingUI;
    public GameObject standingUI;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        isAnimalWaiting = false;
        sittingUI.SetActive(false);
        standingUI.SetActive(true);
        
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Q) && isAnimalWaiting == false)
        {
            //Animal1.GetComponent<NavMeshAgent>().enabled = false;
            Animal1.GetComponent<NavMeshAgent>().isStopped = true;
            isAnimalWaiting = true;
            sittingUI.SetActive(true);
            standingUI.SetActive(false);
            //Animal1.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isAnimalWaiting == true)
        {
            //Animal1.GetComponent<NavMeshAgent>().enabled = true;
            Animal1.GetComponent<NavMeshAgent>().isStopped = false;
            isAnimalWaiting = false;
            sittingUI.SetActive(false);
            standingUI.SetActive(true);
            //Animal1.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
