using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private GameObject enemyTarget;
    [SerializeField]
    private int enemyHealth = 3;
    [SerializeField]
    private float enemySpeed = 0.1f;

    [SerializeField]
    private float lineOfSight = 5f;
    [SerializeField]
    private float distanceToAttack = 5f;
    [SerializeField]
    private float targetDistance;


    [SerializeField]
    private float fireRate = 2.5f;
    [SerializeField]
    private bool canFire = false;
    [SerializeField]
    private bool useBillboard = true;
    [SerializeField]
    private bool targetPlayer;

    private Rigidbody rb;
    private BillboardScript billboardScript;

    private void Start()
    {
        GameManager.Instance.EnemyCount += 1;
        // Will auto assign the Enemy Target to be the Player
        // MAKE SURE TO REMOVE DURING HOUSE DEFENDER, THIS WILL CAUSE ALL ENEMIES TO ONLY ATTACK THE PLAYER
        if (targetPlayer)
        { 
        enemyTarget = GameObject.Find("Player");
        }
        rb = transform.GetComponent<Rigidbody>();
        if (transform.GetChild(0).name == "EyeballSprite")
        {
            billboardScript = transform.GetComponent<BillboardScript>();
        }
        else
        {
            billboardScript = transform.GetChild(0).GetComponent<BillboardScript>();
        }
    }
    private void Update()
    {
        if (enemyTarget != null)
        { 
        EnemyLookAndFire();
        EnemyMovement();
        }
    }

    private void EnemyMovement()
    {
        if (targetDistance <= lineOfSight && targetDistance >= distanceToAttack)
        {
            if (useBillboard != true)
            {
                transform.LookAt(enemyTarget.transform);
            }
            //transform.Translate(0, 0, enemySpeed * Time.deltaTime);
            rb.MovePosition(transform.position + (transform.forward * enemySpeed * Time.deltaTime));
        }
    }
    private void EnemyLookAndFire()
    {
        targetDistance = Vector3.Distance(enemyTarget.transform.position, transform.position);
        Vector3 targetAim = enemyTarget.transform.position - transform.position;
        Quaternion targetDirection = Quaternion.LookRotation(targetAim);

        if (targetDistance <= distanceToAttack)
        {  
            // Follows the Players current Position
            transform.rotation = targetDirection;

            if (canFire == true )
            {
                StartCoroutine(EnemyFire());
            }
        }

        IEnumerator EnemyFire()
        {
            canFire = false;
            Instantiate(enemyProjectile, transform.position, targetDirection);
            yield return new WaitForSeconds(fireRate);
            canFire = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Projectile"))
        {
            enemyHealth -= 1;
            billboardScript.EnemyDamaged();
            if (enemyHealth <= 0)
            {
            GameManager.Instance.EnemyCount -= 1;
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
            }
            else
            {
                Player_Projectile playerProjectile = collider.gameObject.GetComponent<Player_Projectile>();
                rb.AddForce(collider.transform.forward * playerProjectile.projectileKnockback, ForceMode.Impulse);
            }
        }
    }
}
