using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;
    private bool playerInTrigger;

    public GameObject InvisWall;
    public Sound_Detection soundDetect;
    public bool isSoundPlaying;

    public GameObject openUI;

    void Start()
    {
        openUI.SetActive(false);
        animator = transform.parent.gameObject.GetComponent<Animator>();
        isOpen = animator.GetBool("open");
        playerInTrigger = false;
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown("e"))
        {
            InvisWall.SetActive(false);

            isOpen = !isOpen;
            isSoundPlaying = true;
            // isSoundPlaying = isOpen;
            animator.SetBool("open", isOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            openUI.SetActive(true);
            playerInTrigger = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            openUI.SetActive(false);
            playerInTrigger = false;
        }
    }
}
