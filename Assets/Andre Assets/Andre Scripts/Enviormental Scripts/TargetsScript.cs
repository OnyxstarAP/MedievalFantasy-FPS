using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsScript : MonoBehaviour
{  
    void Start()
    {
        TutorialManager.targetsRemaining++;
    }

    private void OnTriggerEnter(Collider other)
    {
        TutorialManager.targetsRemaining--;
        Destroy(gameObject);
    }
}
