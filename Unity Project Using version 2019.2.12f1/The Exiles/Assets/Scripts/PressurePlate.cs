using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isTriggered = false;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        Debug.Log("Pressure plate triggered.");
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        Debug.Log("Pressure plate no longer triggered.");
    }
}
