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
    private GameObject tutorialPhase2;
    [SerializeField]
    public GameObject tutorialPhase3;
    [SerializeField]
    public GameObject tutorialPhase4;

    public static int targetsRemaining;
    private ProgressionCubeScirpt progressionCubeScript;
    public SceneFader scenefader;

    private bool isSpeaking;


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
            tutorialPhase2.SetActive(true);
            tutorialText.text = "You can also use 'Space' to jump!";
            tutorialProgression++;
        }
        else if (tutorialProgression == 1 && progressionCubeScript.playerOverlapping)
        {
            tutorialPhase2.SetActive(false);
            progressionCube.SetActive(false);
            tutorialPhase3.SetActive(true);
            tutorialText.text = "Now, try throwing your weapon at those Targets with 'Left Mouse'!";
            tutorialProgression++;
        }
        else if (tutorialProgression == 2 && targetsRemaining == 0)
        {
            tutorialProgression++;
            tutorialPhase4.SetActive(true);
            StartCoroutine(Phase3());
        }
        else if (tutorialProgression == 3 && targetsRemaining == 0 && isSpeaking == false)
        {
            tutorialProgression++;
            StartCoroutine(Phase4());
        }

        IEnumerator Phase3()

        {
            isSpeaking = true;
            tutorialText.text = "Nice shot! Just get that last one and we're done here!";
            yield return new WaitForSeconds(10);
            tutorialText.text = "Oh yeah, my bad! You'll have charge your weapon first!";
            yield return new WaitForSeconds(6);
            tutorialText.text = "Hold 'Left Mouse' down until the blades golden, then let fly!";
            yield return new WaitForSeconds(3);
            isSpeaking = false;
        }

        IEnumerator Phase4()
        {
            tutorialText.text = "There you go!";
            yield return new WaitForSeconds(6);
            tutorialText.text = "Charging shots can help cover longer distances without needing to moving";
            yield return new WaitForSeconds(8);
            tutorialText.text = "At Max Charge, as you just saw, it can even pierce through enemies and their barriers!";
            yield return new WaitForSeconds(10);
            tutorialText.text = "Be sure to use it whenever you can!";
            yield return new WaitForSeconds(8);
            tutorialText.text = "Now, go out there and get 'em!";
            yield return new WaitForSeconds(6);
            scenefader.FadetoLevel(1);
        }

    }
}
