using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatBattleManager : MonoBehaviour
{

    private bool inProgress = true;
    public GameObject gameOver;
    public Text victory;
    public Text Failure;
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
        if (GameManager.Instance.EnemyCount <= 0 && inProgress)
        {
            print("YOU WIN");
            inProgress = false;
            gameOver.SetActive(true);
            victory.gameObject.SetActive(true);
            GameManager.lvl2Complete = true;
            GameManager.Instance.GameOver = true;
        }
    }

    private void failCondition()
    {
        if (GameManager.Instance.PlayerAlive != true)
        {
            print("YOU LOSE");
            inProgress = false;
            gameOver.SetActive(true);
            Failure.gameObject.SetActive(true);
            GameManager.Instance.GameOver = true;
        }
    }
}
