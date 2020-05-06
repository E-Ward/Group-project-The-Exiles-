using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{

    public GameObject InvisWall;

    public Sound_Detection soundDetect;


    public Transform gate;
    public Vector3 openedPosition = new Vector3(-0.57f, 0.83f, -3.9f);
    public Vector3 closedPosition = new Vector3(0.28f, 0.83f, 0.05f);
    // public float animationSpeed = 2.0f;

    private Vector3 openedRotation = new Vector3(-90f, 0, 0);
    // private Vector3 closedRotation = new Vector3(0, 0, 0);
    private Quaternion originalRotation;
    private bool isOpen = false;
    private bool playerInTrigger = false;

    public bool isSoundPlaying;

    public Vector3 myPos;

    void Start()
    {
        originalRotation = transform.rotation;

        myPos = transform.position;
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown("e"))
        {
            InvisWall.SetActive(false);
            if (!isOpen)
            {
                /*
                gate.position = Vector3.Lerp(gate.position, openedPosition, Time.deltaTime * animationSpeed);
                Quaternion target = Quaternion.Euler(-90, 0, 0);
                gate.rotation = Quaternion.Slerp(gate.rotation, target, Time.deltaTime * animationSpeed);
                */
                gate.position = openedPosition;
                gate.rotation = Quaternion.Euler(openedRotation);
                isOpen = true;
                isSoundPlaying = true;
            }
            else
            {
                /*
                gate.position = Vector3.Lerp(gate.position, closedPosition, Time.deltaTime * openSpeed);
                Quaternion target = Quaternion.Euler(closedRotation);
                gate.rotation = Quaternion.Slerp(gate.rotation, target, Time.deltaTime * smooth);
                */
                gate.position = closedPosition;
                gate.rotation = originalRotation;
                isOpen = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
