using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField]
    private int houseHealth = 30;
    [SerializeField]
    private float flickerRate = 0.1f;
    [SerializeField]
    private bool isInvincible = false;
    [SerializeField]
    private bool isDestroyed = false;

    private Material curMat;
    public Material destroyed;



    private MeshRenderer meshRenderer;
    public HealthbarScript healthbarscript;

    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        curMat = meshRenderer.material;
    }
    private void Start()
    {
        healthbarscript.SetMaxHealth(houseHealth);
        // VV To be removed with new Game Manager VV
        GameManager.Instance.ObjectiveCount += 1;
    }
    private void Update()
    {
        Debug.Log(GameManager.Instance.ObjectiveCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isInvincible == false)
        {
            DamageHouse();
        }
    }

    private void DamageHouse()
    {
        houseHealth -= 1;
        healthbarscript.SetHealth(houseHealth);
        if (houseHealth > 0 )
        { 

            StartCoroutine(InvinceFlicker());
        }
        else
        {
            isDestroyed = true;
            meshRenderer.material = destroyed;
            GameManager.Instance.ObjectiveCount -= 1;
        }

        IEnumerator InvinceFlicker()
        {
            isInvincible = true;
            meshRenderer.material = destroyed;
            yield return new WaitForSeconds(flickerRate);
            meshRenderer.material = curMat;
            yield return new WaitForSeconds(flickerRate);
            meshRenderer.material = destroyed;
            yield return new WaitForSeconds(flickerRate);
            meshRenderer.material = curMat;
            isInvincible = false;
        }
    }
}
