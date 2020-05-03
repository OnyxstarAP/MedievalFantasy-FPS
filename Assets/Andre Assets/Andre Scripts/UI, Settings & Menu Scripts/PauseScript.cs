using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseCanvasScreen;
    public Text pauseCanvasText;
    public GameObject pauseMainMenu;
    public GameObject pauseSettingsMenu;

    public Slider mouseSenseSlider;
    public Slider SFXVolumeSlider;
    public Slider musicVolumeSlider;

    public GameObject sceneFader;
    private SceneFader sceneFaderScript;

    void Start()
    {
        sceneFaderScript = sceneFader.GetComponent<SceneFader>();
        if (sceneFader == null)
        {
            sceneFader = GameObject.Find("FadeCanvas");
        }
    }

    void Update()
    {
        PauseSystem();
    }

    private void PauseSystem()
    {
        Debug.Log("Pause Script is Running");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameWatcher.gamePaused)
            {
                Debug.Log("Menu Closed");
                ResumeGame();
            }
            else
            {
                Debug.Log("Menu Opened");
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseCanvasScreen.SetActive(false);
        Time.timeScale = 1f;
        GameWatcher.gamePaused = false;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseCanvasScreen.SetActive(true);
        Time.timeScale = 0f;
        GameWatcher.gamePaused = true;
    }

    public void PauseMenuResume()
    {
        Debug.Log("Pause Menu - Resuming Game");
        ResumeGame();
    }

    public void PauseMenuSettings()
    {
        Debug.Log("Pause Menu - Settings");
        pauseMainMenu.SetActive(false);
        pauseSettingsMenu.SetActive(true);
        pauseCanvasText.text = "Settings";

        mouseSenseSlider.value = GameWatcher.mouseSensitivity;
        SFXVolumeSlider.value = GameWatcher.sfxVolume;
        musicVolumeSlider.value = GameWatcher.musicVolume;
    }

    public void PauseSettingsSave()
    {
        Debug.Log("Pause Settings - Saved Changes");

        GameWatcher.mouseSensitivity = mouseSenseSlider.value;
        GameWatcher.sfxVolume = SFXVolumeSlider.value;
        GameWatcher.musicVolume = musicVolumeSlider.value;

        Debug.Log("Pause Settings - Mouse Sensetivity set to: " + GameWatcher.mouseSensitivity);
        Debug.Log("Pause Settings - Sound Effects volume set to: " + GameWatcher.sfxVolume);
        Debug.Log("Pause Settings - Music Volume set to: " + GameWatcher.musicVolume);

        PauseSettingsExit();
    }
    public void PauseSettingsExit()
    {
        Debug.Log("Pause Settings - Back to Pause Menu");
        pauseMainMenu.SetActive(true);
        pauseSettingsMenu.SetActive(false);
        pauseCanvasText.text = "Paused";
    }


    public void PauseMenuLVLSelect()
    {
        sceneFaderScript.FadetoLevel("Hub_Scene");
        Debug.Log("Pause Menu - Level Select");
        ResumeGame();
    }

    public void PauseMenuCloseGame()
    {
        Debug.Log("Pause Menu - Closing Game");
        Application.Quit();
    }
}
