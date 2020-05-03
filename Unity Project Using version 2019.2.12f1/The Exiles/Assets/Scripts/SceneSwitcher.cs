using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public GameObject levelSelectCanvas;
    public GameObject mainMenuCanvas;

    public void GotoDemoScene()
    {
        SceneManager.LoadScene("DEMO LEVEL");
        Time.timeScale = 1f;
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1f;
    }

    public void EnableLevelSelectCanvas()
    {
        mainMenuCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

    public void DisableLevelSelectCanvas()
    {
        levelSelectCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void GotoTutorialScene()
    {
        SceneManager.LoadScene("TUTORIAL LEVEL");
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void OpenURL()
    {
        Application.OpenURL("http://Youtube.com/");
        Debug.Log("is this working?");
    }
}
