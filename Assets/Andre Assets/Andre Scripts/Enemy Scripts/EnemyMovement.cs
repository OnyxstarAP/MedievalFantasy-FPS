using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private bool attacking;
    [SerializeField]
    private Transform target;

    private BillboardScript billboardScript;

    private Rigidbody rb;
    void Start()
    {
        billboardScript = transform.GetComponent<BillboardScript>();
        target = GameObject.Find("Player").transform;
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float _enemyDistance = Vector3.Distance(transform.position, target.position);

        if (_enemyDistance > attackRange)
        { 
        rb.MovePosition(transform.position + (transform.forward * movementSpeed * Time.deltaTime));
        }
        else
        {
            if (attacking == false)
                attacking = true;
                billboardScript.EnemyTelegraph();
        }
    }
}
