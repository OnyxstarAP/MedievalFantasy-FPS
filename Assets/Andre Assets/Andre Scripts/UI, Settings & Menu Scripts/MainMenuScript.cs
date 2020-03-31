using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject levelFader;

    public void StartGame()
    {
        levelFader.GetComponent<SceneFader>().FadetoLevel(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    public void EnterTutorial()
    {
        SceneManager.LoadScene(4);
    }

}
