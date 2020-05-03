using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A trigger used in the tutorial level to display help tooltips when triggered.
/// </summary>
public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialManager;
    private TutorialManager tutorialManagerScript;
    public Texture textureToAssign;
    public Vector3 textureScale = new Vector3(0.2f, 0.2f, 0.2f);
    public bool animalIsFree = false;

    // Start is called before the first frame update
    void Start()
    {
        tutorialManagerScript = tutorialManager.GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialManagerScript.displayHelp = true;
            tutorialManagerScript.setTexture(textureToAssign, true, textureScale);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialManagerScript.displayHelp = false;
        }
    }
}
