using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWatcher : MonoBehaviour
{
    public static int playerHealth = 3;
    public static bool playerAlive = true;
    private Transform playerObject;

    public static int enemyCount;
    public static bool gameOver = false;

    public static bool objectiveFailed = false;
    public static int objectiveCount;

    public static bool gamePaused = false;

    public static float sfxVolume = 0.75f;
    public static float musicVolume = 0.75f;

    private string sceneName;

    public Slider mouseSenseSlider;
    public static float mouseSensitivity = 200;

    public Text playerHealthHUD;
    void Start()
    {
        gameOver = false;
        playerAlive = true;
        sceneName = SceneManager.GetActiveScene().name;

        if (playerObject == null)
        {
            playerObject = GameObject.Find("Player").transform;
        }
    }

    public void MouseSensitivityAdjuster()
    {
        //mouseSensitivity = mouseSenseSlider.value;
        //Debug.Log("Your current Mouse sensitivity is: " + mouseSensitivity);
    }

    public void HUDUpdater()
    {
        playerHealthHUD.text = "Health: " + playerHealth;
    }
    public static void PlayerDamage()
    {
        playerHealth--;
        Debug.Log("The Players current Health Points: " + playerHealth);
    }

    public void OutOfBounds()
    {
        Vector3 _outOfBounds = new Vector3(0, -5, 0);
        if (playerObject.position.y < _outOfBounds.y && playerObject != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
   
    private void gameoverState()
    {
        if (playerAlive != true || objectiveFailed)
        {
            gameOver = true;
        }
    }

    void Update()
    {
        OutOfBounds();
        MouseSensitivityAdjuster();
        HUDUpdater();
        Debug.Log("Enemies Remaining: " + enemyCount);
    }


}
