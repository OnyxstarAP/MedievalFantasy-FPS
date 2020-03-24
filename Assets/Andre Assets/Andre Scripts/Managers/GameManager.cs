using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }


    public GameObject player;
    public int EnemyCount { get; set; }
    public int ObjectiveCount { get; set; }
    public bool GameOver { get; set; }

    public bool PlayerAlive { get; set; }

    public static bool lvl1Complete { get; set; }
    public static bool lvl2Complete { get; set; }

    private Scene currentScene;

    private PlayerControllerScript playerScript;
    

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        currentScene = SceneManager.GetActiveScene();
        playerScript = player.GetComponent<PlayerControllerScript>();
        PlayerAlive = true;
    }

    private void Update()
    {
        OutOfBounds();
        GameState();
        PlayerCheck();
    }

    private void OutOfBounds()
    {
        Vector3 _outOfBounds = new Vector3(0, -5, 0);
        string _thisScene = currentScene.name;
        if (player.transform.position.y < _outOfBounds.y && player != null)
        {
            SceneManager.LoadScene(_thisScene);
        }
    }

    private void GameState()
    {
        if (GameOver == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
            GameOver = false;
        }
    }

    private void PlayerCheck()
    {
        if (playerScript.playerHealth <= 0)
        {
            playerScript.enabled = false;
            PlayerAlive = false;
        }
    }
}
