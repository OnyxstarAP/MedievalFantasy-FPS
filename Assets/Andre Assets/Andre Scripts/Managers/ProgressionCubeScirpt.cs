using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionCubeScirpt : MonoBehaviour
{
    public bool playerOverlapping;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlapping = false;
        }
    }
}
