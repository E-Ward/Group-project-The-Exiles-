using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{

    public GameObject Player;
    public GameObject ThisAnimal;
    public GameObject ThisAnimalHead;
    public float Distance_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance_ = Vector3.Distance(Player.transform.position, ThisAnimalHead.transform.position);
        if (Distance_ <= 2)
        {
            //Debug.Log("It worked");
            //ThisAnimal.GetComponent<AnimalWanderingAI>().enabled = false;
            //ThisAnimal.GetComponent<AnimalNavPursue>().enabled = false;
            ThisAnimal.GetComponent<AnimalMaster>().isNavPaused = true;
        }
        else if (Distance_ > 2)
        {
            //ThisAnimal.GetComponent<AnimalWanderingAI>().enabled = true;
            //ThisAnimal.GetComponent<AnimalNavPursue>().enabled = true;
            ThisAnimal.GetComponent<AnimalMaster>().isNavPaused = false;
        }
    }
}
