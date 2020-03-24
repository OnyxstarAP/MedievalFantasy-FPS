using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveAttackerScript : MonoBehaviour
{
    public GameObject enemyProjectile;

    public GameObject target;

    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Quaternion targetDirection;

    void Start()
    {
        StartCoroutine(EnemyFire());
        targetDirection = Quaternion.LookRotation(target.transform.forward);
    }

        IEnumerator EnemyFire()
        {
            Instantiate(enemyProjectile, transform.position, targetDirection);
            yield return new WaitForSeconds(fireRate);
            StartCoroutine(EnemyFire());
        }
}
