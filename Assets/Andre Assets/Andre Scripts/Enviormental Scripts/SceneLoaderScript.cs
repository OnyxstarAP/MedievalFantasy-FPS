using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaderScript : MonoBehaviour
{
    [SerializeField]
    private int sceneID;
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
        

        if (sceneID == 2)
        {
            sceneCompleted = GameManager.lvl1Complete;
        }
        else if (sceneID == 3)
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
        StartLevel(sceneID);
    }

     void StartLevel(int sceneNumber)
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            print("Load New layout");
            SceneManager.LoadScene(sceneNumber);
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
