using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float projectleSpeed = 20;
    [SerializeField]
    private float expireLimit = 10;
    [SerializeField]
    private float expireTimer;
    [SerializeField]
    private LayerMask projIgnoreMask;

    void Update()
    {
        projectileMovement();
        projectileExpire();
    }


    void projectileMovement()
    {
        transform.Translate((Vector3.forward * projectleSpeed) * Time.deltaTime);
    }
    void projectileExpire()
    {
        expireTimer += 1 * Time.deltaTime;

        if (expireTimer >= expireLimit)
        {
            Destroy(transform.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        LayerMask _enemyProjLayer = other.gameObject.layer;
        if (other.tag != "Enemy")
        {
            Destroy(transform.gameObject);
        }
    }
}
