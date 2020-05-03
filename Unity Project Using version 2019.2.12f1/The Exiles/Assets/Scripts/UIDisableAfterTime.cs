using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisableAfterTime : MonoBehaviour
{
    public float time;
    public GameObject openingDialog;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForSec");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(time);
        openingDialog.SetActive(false);
    }
}
