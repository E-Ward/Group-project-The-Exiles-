using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A trigger that interacts with invisible barriers in the game.
/// 
/// Author: Alpeche Pancha
/// </summary>
public class BarrierTrigger : MonoBehaviour
{
    /// <summary>
    /// The GameObject of the invisible barrier.
    /// </summary>
    public GameObject tutorialBarrier;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BoxCollider boxCollider = tutorialBarrier.GetComponent<BoxCollider>();
            // Disable invisible barrier when player enters area with this trigger.
            boxCollider.enabled = false;
        }
    }
}
