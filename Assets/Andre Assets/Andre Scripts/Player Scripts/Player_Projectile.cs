using System.Collections;
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
    private float maxChargeTime = 2.5f;
    [SerializeField]
    private float expireLimit = 10;
    [SerializeField]
    private LayerMask playerProjMask;
    [SerializeField]
    private bool isMaxCharged;
    [SerializeField]
    private bool isThrown;

    [SerializeField]
    private Transform childObject;
    [SerializeField]
    private Transform weaponBody;
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
    private AudioSource throwsource;
    private AudioSource chargeSource;
    private AudioSource drawSource;
    private void Awake()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        childObject = transform.GetChild(0);
        weaponBody = transform.GetChild(1);
        meshRenderer = childObject.GetComponent<MeshRenderer>();
        firePoint = player.GetComponent<PlayerControllerScript>().firePoint;
        mainCamera = player.GetComponent<PlayerControllerScript>().mainCamera;
        neutralChargeMat = meshRenderer.material;
        throwsource = transform.GetComponent<AudioSource>();
        chargeSource = childObject.GetComponent<AudioSource>();
        drawSource = weaponBody.GetComponent<AudioSource>();
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
            if(projectileCharge < maxChargeTime)
            {
                projectileCharge += 1 * Time.deltaTime;
                if (projectileCharge > (maxChargeTime / 5 * 2) && meshRenderer.material == neutralChargeMat)
                {
                    // Changes the objects material to Mid-Charge
                    chargeSource.pitch = 1;
                    chargeSource.Play();
                    meshRenderer.material = midChargeMat;
                }

            }
            else if (projectileCharge >= maxChargeTime && isMaxCharged != true)
            {
                chargeSource.pitch = 1.25f;
                chargeSource.Play();
                isMaxCharged = true;
                meshRenderer.material = maxChargeMat;
                projectileCharge = maxChargeTime;
            }
            projectileLock();
        }

        if (isCharing == false && isThrown == false)
        {
            throwsource.Play();
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
        yield return new WaitForSeconds(expireLimit / 2);
        childObject.GetComponent<MeshCollider>().enabled = true;
        yield return new WaitForSeconds(expireLimit / 2);
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        LayerMask otherLayer = other.gameObject.layer;
        if (isMaxCharged == false && other.tag != "Player")
        {
            
            //Destroy(transform.gameObject);
        }
        else if (isMaxCharged && other.tag != "Player" && other.tag != "Enemy")
        {
            //Destroy(transform.gameObject);
        }
    }
}
