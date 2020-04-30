using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtboxDisable : MonoBehaviour
{
    [SerializeField]
    private bool disableAfterHit = true;
    [SerializeField]
    private float knockbackForce;
    private CapsuleCollider hurtbox;
    private Rigidbody playerRB;
    void Start()
    {
        hurtbox = gameObject.GetComponent<CapsuleCollider>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        if (knockbackForce > 0)
        {
            knockbackForce = knockbackForce * -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && disableAfterHit)
        {
            playerRB.AddForce(other.transform.forward * knockbackForce, ForceMode.Impulse);
            hurtbox.enabled = false;
        }
    }
}
