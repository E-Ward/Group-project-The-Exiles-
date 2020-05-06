using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TutorialWin : MonoBehaviour
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

    public GameObject rescuedAnimal1;
    public GameObject rescuedAnimal2;

    public GameObject AnimalGoTo;
    public GameObject AnimalGoTo2;

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
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine(Reset());
        }
    }

    private void OnTriggerEnter(Collider player)
    {

        if (player.gameObject.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;

            Animal2.SetActive(false);
            rescuedAnimal2.SetActive(true);
            Animal2.GetComponent<AnimalMaster>().isfollowingplayer = false;

            Animal1.SetActive(false);
            rescuedAnimal1.SetActive(true);
            Animal1.GetComponent<AnimalMaster>().isfollowingplayer = false;
            animalsRescued += 2;
        }
    }

    void Update()
    {
        animalsRescuedText.text = $"Number of animals rescued: {animalsRescued}/{numberOfAnimalsInTheScene}";

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
