using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{

    private int tutorialProgression = 0;
    [SerializeField]
    private Canvas tutorialCanvas;
    [SerializeField]
    private GameObject progressionCube;
    [SerializeField]
    private Text tutorialText;
    [SerializeField]
    private GameObject tutorialPart2;
    public static int targetsRemaining;
    [SerializeField]
    public GameObject targets;

    private ProgressionCubeScirpt progressionCubeScript;



    void Start()
    {
        progressionCubeScript = progressionCube.GetComponent<ProgressionCubeScirpt>();
    }

    // Update is called once per frame
    void Update()
    {
        Tutorial();
    }

    private void Tutorial()
    {
        if (tutorialProgression == 0 && progressionCubeScript.playerOverlapping)
        {
            progressionCube.transform.position = new Vector3(-8, 3.25f, -4);
            tutorialPart2.SetActive(true);
            tutorialProgression++;
            tutorialText.text = "You can also use 'Space' to jump!";
        }
        else if (tutorialProgression == 1 && progressionCubeScript.playerOverlapping)
        {
            tutorialText.text = "You can throw your weapon with 'Left Mouse'";
            tutorialPart2.SetActive(false);
            progressionCube.SetActive(false);
            targets.SetActive(true);
            tutorialProgression++;
        }
        else if (tutorialProgression == 2 && TutorialManager.targetsRemaining == 0)
        {
            tutorialProgression++;
            tutorialText.text = "Lastly, hold down the mouse to charge your shot";
            StartCoroutine(WaitTime());
        }

        IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(3);
            tutorialText.text = "It'll increase the shots distance!";
            yield return new WaitForSeconds(2);
            tutorialText.text = "You can also pierce through enemies, shots and barriers!";
            yield return new WaitForSeconds(5);
            tutorialText.text = "Now go out there and get 'em!";
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(1);
        }

    }
}
