using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    // The Gameobject that will be fired at the Target
    [SerializeField]
    private GameObject enemyProjectile;
    // The Target to fire at
    [SerializeField]
    private Transform target;

    // This Enemies Health
    [SerializeField]
    private int enemyHealth = 3;
    // This enemies Movement Speed
    [SerializeField]
    private float enemySpeed = 0.1f;

    // The current Distance this Enemy and the Target
    [SerializeField]
    private float targetDistance;
    // The radial Distance from this enemy to see a target from
    [SerializeField]
    private float radialSight = 5f;
    // The Distance the enemy will attack from
    [SerializeField]
    private float attackRange = 5f;


    // The Rate of Fire
    [SerializeField]
    private float fireRate = 2.5f;
    // If the enemy can currently fire
    [SerializeField]
    private bool canFire = false;

    // If the enemies current target is the Player
    [SerializeField]
    private bool targetPlayer;
    // If the Billboard angle is currently being used to fire
    [SerializeField]
    private bool useBillboard = true;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    private bool isIFrame = false;

    private IEnumerator enemyDeath;
    private IEnumerator enemyDamage;
    private IEnumerator enemyShooting;

    private Quaternion targetDirection;
    private Vector3 targetAim;
    [SerializeField]
    private Transform parentObject;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private BillboardScript billboardScript;
    private void Start()
    {
        GameManager.Instance.EnemyCount += 1;

        if (targetPlayer)
        {
            target = GameObject.Find("Player").transform;
        }
        if (parentObject.GetChild(1).name == "Sprite")
        {
            billboardScript = parentObject.GetChild(1).GetComponent<BillboardScript>();
        }
        else
        {
            billboardScript = transform.GetChild(0).GetComponent<BillboardScript>();
        }
        
        enemyDamage = enemyDamaged();
        enemyDeath = EnemyDefeat();
        enemyShooting = EnemyTelegraph();
        targetAim = target.position - transform.position;
        targetDirection = Quaternion.LookRotation(targetAim);
        
    }
    private void Update()
    {
        if (target != null)
        {
            Movement();
            Shooting();
        }
    }

    private void Movement()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);
        // Movement Script
        if (targetDistance <= radialSight && targetDistance >= attackRange && isAlive)
        {
            parentObject.LookAt(target);
            rb.MovePosition(parentObject.position + (parentObject.forward * enemySpeed * Time.deltaTime));
        }
    }
    private void Shooting()
    {
        if (targetDistance <= attackRange && isAlive && isIFrame == false)
        {
            // Follows the Players current Position
            transform.rotation = targetDirection;
            if (canFire == true)
            {
                StartCoroutine(enemyShooting);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Projectile") && isAlive && isIFrame == false)
        {
            enemyHealth -= 1;
            if (enemyHealth <= 0)
            {
                isAlive = false;
                StopCoroutine(enemyShooting);
                StartCoroutine(enemyDeath);
            }
            else
            {
                isIFrame = true;
                Destroy(collider.gameObject);
                StopCoroutine(enemyShooting);
                StartCoroutine(enemyDamage);
                Player_Projectile playerProjectile = collider.gameObject.GetComponent<Player_Projectile>();
                rb.AddForce(collider.transform.forward * playerProjectile.projectileKnockback, ForceMode.Impulse);
            }
        }
    }

    IEnumerator EnemyDefeat()
    {
        billboardScript.EnemyDefeat();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        GameManager.Instance.EnemyCount -= 1;
    }

    IEnumerator EnemyTelegraph()
    {
        canFire = false;
        billboardScript.EnemyTelegraph();
        yield return new WaitForSeconds(fireRate / 5);
        Instantiate(enemyProjectile, transform.position, targetDirection);
        yield return new WaitForSeconds(fireRate);
        billboardScript.EnemyNeutral();
        canFire = true;
    }

    IEnumerator enemyDamaged()
    {
        canFire = false;
        billboardScript.EnemyDamaged();
        yield return new WaitForSeconds(1.5f);
        isIFrame = false;
        canFire = true;
        billboardScript.EnemyNeutral();
    }
}
