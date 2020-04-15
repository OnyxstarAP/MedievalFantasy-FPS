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
        scenefader.FadetoLevel("Hub_Scene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    public void EnterTutorial()
    {
        scenefader.FadetoLevel("LevelTutorial_Scene");
    }

}
