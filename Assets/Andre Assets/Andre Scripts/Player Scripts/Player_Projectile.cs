﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileForce = 1000;
    public float projectileKnockback;
    public float projectileCharge;
    [SerializeField]
     GameObject player;
    [SerializeField]
    private float chargeLimit = 2.5f;
    [SerializeField]
    private float expireLimit = 10;
    [SerializeField]
    private LayerMask playerProjMask;
    [SerializeField]
    private bool isMaxCharged;
    [SerializeField]
    private bool isThrown;

    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Material neutralChargeMat;
    [SerializeField]
    private Material midChargeMat;
    [SerializeField]
    private Material maxChargeMat;

    private Transform firePoint;
    private Transform mainCamera;
    [SerializeField]
    private float accuracyMod;


    private Rigidbody rb;

    private void Awake()
    {
        player = GameObject.Find("Player");
        firePoint = player.GetComponent<PlayerControllerScript>().firePoint;
        mainCamera = player.GetComponent<PlayerControllerScript>().mainCamera;
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        neutralChargeMat = meshRenderer.material;
    }
    void Update()
    {
        chargeThrow();
    }
    
    private void chargeThrow()
    {
        bool isCharing = Input.GetKey(KeyCode.Mouse0);

        if (isCharing && isThrown != true)
        { 
            if(projectileCharge < chargeLimit)
            {
                projectileCharge += 1 * Time.deltaTime;
                if (projectileCharge > 1 && meshRenderer.material == neutralChargeMat)
                {
                    meshRenderer.material = midChargeMat;
                }

            }
            else if (projectileCharge > chargeLimit)
            {
                projectileCharge = chargeLimit;
                meshRenderer.material = maxChargeMat;
                isMaxCharged = true;
            }
            projectileLock();
        }

        if (isCharing == false && isThrown == false)
        {
            if (projectileCharge > 1)
            {
                rb.AddForce(transform.forward * (projectileForce * projectileCharge));
            }
            else
            {
                rb.AddForce(transform.forward * projectileForce);
            }
            StartCoroutine(lifetimeCounter());
            rb.useGravity = true;
            isThrown = true;
        }
    }

    private void projectileLock()
    {

        Vector3 lockPoint = firePoint.transform.position;
        Quaternion lockRotation = mainCamera.rotation;
        Quaternion rotationMod = new Quaternion(lockRotation.x, lockRotation.y - accuracyMod, lockRotation.z, lockRotation.w);
        transform.position = lockPoint;
        transform.rotation = rotationMod;
    } 
    IEnumerator lifetimeCounter()
    {
        yield return new WaitForSeconds(expireLimit);
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        LayerMask otherLayer = other.gameObject.layer;
        if (isMaxCharged == false && other.tag != "Player")
        {
            Destroy(transform.gameObject);
        }
        else if (isMaxCharged && other.tag != "Player" && other.tag != "Enemy")
        {
            Destroy(transform.gameObject);
        }
    }
}
