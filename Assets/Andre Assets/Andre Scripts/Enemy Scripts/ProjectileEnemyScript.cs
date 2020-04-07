using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    // The Object fired at the Target
    [SerializeField]
    private GameObject enemyProjectile;
    // The Object to Fire at
    [SerializeField]
    private GameObject enemyTarget;
    // How many hits the Enemy can take before dying
    [SerializeField]
    private int enemyHealth = 3;
    // The speed at which the enemy moves at
    [SerializeField]
    private float enemySpeed = 0.1f;

    // The Distance the enemy can see a target from
    [SerializeField]
    private float lineOfSight = 5f;
    // The Distance the enemy will attack from
    [SerializeField]
    private float distanceToAttack = 5f;
    // The current distance of the target

    // The speed in which bullets are fired at
    [SerializeField]
    private float fireRate = 2.5f;
    // If the enemy can currently fire
    [SerializeField]
    private bool canFire = false;
    // If the Billboard angle is currently being used to fire
    [SerializeField]
    private bool useBillboard = true;
    // If the enemies current target is the Player
    [SerializeField]
    private bool targetPlayer;
    [SerializeField]
    private bool alive = true;

    private Rigidbody rb;
    private BillboardScript billboardScript;
    private void Start()
    {
        GameManager.Instance.EnemyCount += 1;
        rb = transform.GetComponent<Rigidbody>();
        // Will auto assign the Enemy Target to be the Player
        // MAKE SURE TO REMOVE DURING HOUSE DEFENDER, THIS WILL CAUSE ALL ENEMIES TO ONLY ATTACK THE PLAYER
        if (targetPlayer)
        { 
        enemyTarget = GameObject.Find("Player");
        }
        
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
            MoveAndFire();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Projectile") && alive)
        {
            enemyHealth -= 1;
            if (enemyHealth <= 0)
            {
                alive = false;
                StartCoroutine(EnemyDefeat());
            }
            else
            {
                billboardScript.EnemyDamaged();
                Player_Projectile playerProjectile = collider.gameObject.GetComponent<Player_Projectile>();
                rb.AddForce(collider.transform.forward * playerProjectile.projectileKnockback, ForceMode.Impulse);
            }
        }

        IEnumerator EnemyDefeat()
        {
            billboardScript.EnemyDefeated();
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
            GameManager.Instance.EnemyCount -= 1;
        }
    }

    private void MoveAndFire()
    {
        float targetDistance = Vector3.Distance(enemyTarget.transform.position, transform.position);
        Vector3 targetAim = enemyTarget.transform.position - transform.position;
        Quaternion targetDirection = Quaternion.LookRotation(targetAim);

        // Movement Script
        if (targetDistance <= lineOfSight && targetDistance >= distanceToAttack && alive)
        {
            if (useBillboard != true)
            {
                transform.LookAt(enemyTarget.transform);
            }
            rb.MovePosition(transform.position + (transform.forward * enemySpeed * Time.deltaTime));
        }

        if (targetDistance <= distanceToAttack && alive)
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
            billboardScript.EnemyTelegraph();
            yield return new WaitForSeconds(fireRate / 5);
            billboardScript.EnemyNormal();
            Instantiate(enemyProjectile, transform.position, targetDirection);
            yield return new WaitForSeconds(fireRate);
            canFire = true;
        }
    }

}
