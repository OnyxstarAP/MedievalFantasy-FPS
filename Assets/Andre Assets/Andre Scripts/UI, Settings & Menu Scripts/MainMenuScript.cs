using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject levelFader;
    private SceneFader scenefader;

    private void Start()
    {
        scenefader = levelFader.GetComponent<SceneFader>();
    }
    public void StartGame()
    {
        scenefader.FadetoLevel(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    public void EnterTutorial()
    {
        scenefader.FadetoLevel(4);
    }

}
