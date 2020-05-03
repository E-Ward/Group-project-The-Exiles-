using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepWalk : MonoBehaviour
{
    Animator anim;
    public AnimalWanderingAI wanderingAI;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wanderingAI.isWandering == true)
        {
            //anim.SetInteger("walkCondition", 1);
        }
        else if (wanderingAI.isWandering == false)
        {
           // anim.SetInteger("walkCondition", 0);
        }
    }
}
