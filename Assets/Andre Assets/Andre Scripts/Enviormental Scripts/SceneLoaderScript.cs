using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaderScript : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    public Material confirm;
    public Material standby;
    public Material complete;
    public bool sceneCompleted = false;
    [SerializeField]
    private bool inTrigger = false;
    [SerializeField]
    private Transform enterLevel;
    private MeshRenderer meshRenderer;


    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        

        if (sceneName == "LevelTown_Scene")
        {
            sceneCompleted = GameManager.lvl1Complete;
        }
        else if (sceneName == "LevelBoat_Scene")
        {
            sceneCompleted = GameManager.lvl2Complete;
        }

        if (sceneCompleted)
        {
            meshRenderer.material = complete;
        }
    }

    private void Update()
    {
        StartLevel(sceneName);
    }

     void StartLevel(string sceneName)
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            Debug.Log("Loaded Scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            inTrigger = true;
            enterLevel.gameObject.SetActive(true);
            meshRenderer.material = confirm;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
            if (sceneCompleted)
            {
                meshRenderer.material = complete;
            }
            else
            {
                meshRenderer.material = standby;
            }
            enterLevel.gameObject.SetActive(false);
        }
    }

}
