using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TownDefenderManager : MonoBehaviour
{

    private bool inProgress = true;
    public GameObject gameOver;
    public Text victory;
    public Text Failure;
    public GameObject player;
    private void Start()
    {
    }
    private void Update()
    {
        clearCondition();
        failCondition();
    }

    private void clearCondition()
    {
        if (GameManager.Instance.EnemyCount <= 0 && GameManager.Instance.ObjectiveCount > 0 && inProgress)
        {
            inProgress = false;
            gameOver.SetActive(true);
            victory.gameObject.SetActive(true);
            GameManager.lvl1Complete = true;
            GameManager.Instance.GameOver = true;
        }
    }

    private void failCondition()
    {
        if (GameManager.Instance.ObjectiveCount <= 0 && inProgress || GameManager.Instance.PlayerAlive == false)
        {
            inProgress = false;
            gameOver.SetActive(true);
            Failure.gameObject.SetActive(true);
            GameManager.Instance.GameOver = true;
        }
    }

}
