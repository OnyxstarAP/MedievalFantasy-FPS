using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance = 2.5f;
    private GameObject interactSpr;
    public Transform player;

    private GameObject mainObject;
    void Start()
    {
        interactSpr = transform.GetChild(0).transform.GetChild(0).gameObject;
        mainObject = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        float _playerDistance = Vector3.Distance(transform.position, player.position);

        if (_playerDistance <= interactionDistance)
        {
            interactSpr.SetActive(true);
        }
        else
        {
            interactSpr.SetActive(false);
        }
    }
}
