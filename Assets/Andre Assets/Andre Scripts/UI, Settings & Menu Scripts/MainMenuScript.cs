using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject levelFader;
    private SceneFader scenefader;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private Slider mouseSesne;
    [SerializeField]
    private Slider SFXVolume;
    [SerializeField]
    private Slider musicVolume;

    private void Start()
    {
        scenefader = levelFader.GetComponent<SceneFader>();
    }
    public void StartGame()
    {
        scenefader.FadetoLevel("Hub_Scene");
    }

    public void MenuSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void menuMain()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void saveChanges()
    {
        GameWatcher.mouseSensitivity = mouseSesne.value;
        GameWatcher.sfxVolume = SFXVolume.value;
        GameWatcher.musicVolume = musicVolume.value;
        Debug.Log("Main Menu - Mouse Sensetivity set to: " + GameWatcher.mouseSensitivity);
        Debug.Log("Main Menu - Sound Effects volume set to: " + GameWatcher.sfxVolume);
        Debug.Log("Main Menu - Music Volume set to: " + GameWatcher.musicVolume);
        menuMain();
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
