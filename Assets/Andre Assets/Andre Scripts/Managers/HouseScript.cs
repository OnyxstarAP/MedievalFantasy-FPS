using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField]
    private int houseHealth = 30;
    [SerializeField]
    private float flickerRate = 0.1f;
    private Material curMat;
    public Material destroyed;
    [SerializeField]
    private bool isDestroyed = false;


    private MeshRenderer meshRenderer;


    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        curMat = meshRenderer.material;
        GameManager.Instance.ObjectiveCount += 1;
    }
    private void Update()
    {
        Debug.Log(GameManager.Instance.ObjectiveCount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DamageHouse();
        }
    }

    private void DamageHouse()
    {
        if (houseHealth > 0)
        { 
        houseHealth -= 1;
        StartCoroutine(DamageFlicker());
        }

        else
        {
            isDestroyed = true;
            meshRenderer.material = destroyed;
            GameManager.Instance.ObjectiveCount -= 1;
        }

        IEnumerator DamageFlicker()
        {
            meshRenderer.material = destroyed;
            yield return new WaitForSeconds(flickerRate);
            meshRenderer.material = curMat;
        }
    }
}
