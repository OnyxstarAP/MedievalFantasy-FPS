using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeScript : MonoBehaviour
{
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
    private float attackCooldown = 2.5f;
    // If the enemy can currently fire
    [SerializeField]
    private bool canAttack = false;

    // If the enemies current target is the Player
    [SerializeField]
    private bool targetPlayer;
    // If the Billboard angle is currently being used to fire
    [SerializeField]
    private bool useBillboard = true;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    private bool isStunned = false;
    [SerializeField]
    private bool isInvulnerable = false;

    private IEnumerator enemyDeath;
    private IEnumerator enemyDamage;
    private IEnumerator enemyAttacking;

    private Quaternion targetDirection;
    private Vector3 targetAim;

    [SerializeField]
    private GameObject hurtbox;
    private CapsuleCollider hurtboxCollider;
    [SerializeField]
    private float attackForce;

    [SerializeField]
    private Transform parentObject;
    private Rigidbody rb;
    [SerializeField]
    private BillboardScript billboardScript;

    private void Start()
    {
        GameWatcher.enemyCount++;
        if (targetPlayer)
        {
            target = GameObject.Find("Player").transform;
            if (target == null)
            {
                Debug.Log("Target could not be found on " + transform.name + " Object.");
            }
        }
        if (parentObject.GetChild(1).name == "Sprite")
        {
            billboardScript = parentObject.GetChild(1).GetComponent<BillboardScript>();
        }
        else
        {
            billboardScript = transform.GetChild(0).GetComponent<BillboardScript>();
        }
        // Test Code: Note used due to time constraints
        //parentObject.Find("Sprite").GetComponent<BillboardScript>();
        enemyDamage = enemyDamaged();
        enemyDeath = EnemyDefeat();
        enemyAttacking = EnemyTelegraph();
        hurtboxCollider = hurtbox.GetComponent<CapsuleCollider>();
        rb = parentObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (target != null)
        {
            Movement();
        }
        transform.LookAt(target);
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
        else if (targetDistance <= attackRange && canAttack && isStunned == false && isAlive)
        {
            Attack();
        }
    }
    private void Attack()
    {
        // Follows the Players current Position
        targetAim = target.position - transform.position;
        targetDirection = Quaternion.LookRotation(targetAim);
        canAttack = false;
        StartCoroutine(enemyAttacking);
        Debug.Log("Enemy Attacked");
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Projectile playerProjectile = other.GetComponent<Player_Projectile>();
        Debug.Log(other.name + " is Making Contact Trigger");
        if (other.CompareTag("Projectile") && isAlive && isStunned == false && isInvulnerable == false)
        {
            enemyHealth -= 1;
            if (enemyAttacking != null)
            {
                StopCoroutine(enemyAttacking);
            }

            if (enemyHealth <= 0)
            {
                isAlive = false;
                StartCoroutine(enemyDeath);
            }
            else
            {
                isStunned = true;
                StartCoroutine(enemyDamage);
                rb.AddForce(other.transform.forward * playerProjectile.projectileKnockback, ForceMode.Impulse);
            }
        }
    }

    IEnumerator EnemyDefeat()
    {
        billboardScript.EnemyDefeat();
        yield return new WaitForSeconds(3f);
        Destroy(parentObject.gameObject);
        GameWatcher.enemyCount--;
    }

    IEnumerator EnemyTelegraph()
    {
        billboardScript.EnemyTelegraph();
        yield return new WaitForSeconds(attackCooldown / 5);
        isInvulnerable = true;
        hurtboxCollider.enabled = true;
        rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(attackCooldown);
        isInvulnerable = false;
        if (hurtboxCollider.enabled)
        { 
            hurtboxCollider.enabled = false;
        }
        billboardScript.EnemyNeutral();
        canAttack = true;
        enemyAttacking = EnemyTelegraph();
    }

    IEnumerator enemyDamaged()
    {
        isStunned = true;
        canAttack = false;
        billboardScript.EnemyDamaged();
        yield return new WaitForSeconds(1.5f);
        isStunned = false;
        canAttack = true;
        billboardScript.EnemyNeutral();
        enemyDamage = enemyDamaged();
    }
}
