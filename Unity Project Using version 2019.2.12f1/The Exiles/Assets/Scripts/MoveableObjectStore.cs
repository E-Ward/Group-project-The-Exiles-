using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectStore : MonoBehaviour
{
    public GameObject[] moveableGameObjects;

    public Vector3 CurrentLocation;

    public Vector3 StartPosition;

    //public List<Vector3> LocationList = new List<Vector3>();

   //public GameObject ;

    // Start is called before the first frame update
    void Start()
    {
        //StartPosition = GameObject.FindGameObjectsWithTag("Moveable");
        moveableGameObjects = GameObject.FindGameObjectsWithTag("Moveable");
        StartPosition = GameObject.FindGameObjectWithTag("Moveable").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLocation = GameObject.FindGameObjectWithTag("Moveable").transform.position;
    }
}
