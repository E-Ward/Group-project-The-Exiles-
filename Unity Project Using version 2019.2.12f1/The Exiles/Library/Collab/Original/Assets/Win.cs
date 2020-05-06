using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    private AnimalMaster animalMaster;
    private Text animalsRescuedText;

    public Text winScreenText;

    public GameObject animalsRescuedTextBox;
    public GameObject uiObject;
    public GameObject helpUiObject;
    public GameObject Enemy;

    public GameObject Animal1;
    public GameObject Animal2;
    public GameObject Animal3;
    public GameObject Animal4;

    public GameObject rescuedAnimal1;
    public GameObject rescuedAnimal2;
    public GameObject rescuedAnimal3;
    public GameObject rescuedAnimal4;

    public GameObject AnimalGoTo;
    public GameObject AnimalGoTo2;
    public GameObject AnimalGoTo3;
    public GameObject AnimalGoTo4;

    public int animalsRescued;
    public int numberOfAnimalsInTheScene = 2;

    public bool isColliding;

    void OnEnable()
    {
        animalMaster = GetComponent<AnimalMaster>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animalsRescued = 0;
        numberOfAnimalsInTheScene = 2;
        uiObject.SetActive(false);
        helpUiObject.SetActive(false);
        animalsRescuedText = animalsRescuedTextBox.GetComponent<Text>();
        animalsRescuedText.text = $"Number of animals rescued: {animalsRescued}/{numberOfAnimalsInTheScene}";
    }

    private void OnTriggerExit(Collider player)
    {
        helpUiObject.SetActive(false);
        

        if (player.gameObject.tag == "Player")
        {
            StartCoroutine(Reset());
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && animalsRescued <= 0)
        {
            helpUiObject.SetActive(true);
        }

        

        if (player.gameObject.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;

            if (Animal2.GetComponent<AnimalMaster>().isfollowingplayer == true)
            {
                Animal2.SetActive(false);
                rescuedAnimal2.SetActive(true);
                Animal2.GetComponent<AnimalMaster>().isfollowingplayer = false;
                animalsRescued += 1;    
            }

            if (Animal1.GetComponent<AnimalMaster>().isfollowingplayer == true)
            {
                Animal1.SetActive(false);
                rescuedAnimal1.SetActive(true);
                Animal1.GetComponent<AnimalMaster>().isfollowingplayer = false;
                animalsRescued += 1;
            }

            if (Animal3.GetComponent<AnimalMaster>().isfollowingplayer == true)
            {
                Animal3.SetActive(false);
                rescuedAnimal3.SetActive(true);
                Animal3.GetComponent<AnimalMaster>().isfollowingplayer = false;
                animalsRescued += 1;
            }

            if (Animal4.GetComponent<AnimalMaster>().isfollowingplayer == true)
            {
                Animal4.SetActive(false);
                rescuedAnimal4.SetActive(true);
                Animal4.GetComponent<AnimalMaster>().isfollowingplayer = false;
                animalsRescued += 1;
            }

            animalsRescuedText.text = $"Number of animals rescued: {animalsRescued}/{numberOfAnimalsInTheScene}";
            winScreenText.text = $"You saved: {animalsRescued} Animals";
            //StartCoroutine(Reset());
        }  
    }

    void Update()
    {
        if (animalsRescued >= 2)
        {
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        winScreenText.text = $"You saved: {animalsRescued} Animals";
        uiObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Enemy.SetActive(false);
        StartCoroutine("WaitForSec2");
    }

    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
