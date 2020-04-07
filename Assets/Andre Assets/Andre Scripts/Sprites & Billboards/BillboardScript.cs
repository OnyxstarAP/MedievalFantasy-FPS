using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [SerializeField]
    private bool followPlayer = true;
    [SerializeField]
    private bool XAxisFollow = false;
    public Transform billboardTarget;

    private Transform enemySprite;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite spriteOriginal;
    [SerializeField]
    private Sprite spriteDamaged;
    [SerializeField]
    private Sprite spriteDefeated;
    [SerializeField]
    private Sprite spriteTelegraph;

    public AudioSource damagedSound;

    private void Awake()
    {
        billboardTarget = GameObject.Find("Player").transform;
        if (transform.childCount > 0)
        {
            enemySprite = transform.GetChild(0);
        }
        else if (transform.childCount == 0)
        {
            enemySprite = transform;
        }
        spriteRenderer = enemySprite.GetComponent<SpriteRenderer>();
        spriteOriginal = enemySprite.GetComponent<SpriteRenderer>().sprite;
        damagedSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        if (followPlayer == true)
        {
            if (XAxisFollow == false)
            {
                Vector3 TagerPosition = new Vector3(billboardTarget.position.x, transform.position.y, billboardTarget.position.z);
                transform.LookAt(TagerPosition);
            }
            else if (XAxisFollow == true)
            {
                transform.LookAt(billboardTarget);
            }
        }
    }

    public void EnemyDamaged()
    {
        StartCoroutine(damaged());

        IEnumerator damaged()
        {
            print("Enemy was Damaged");
            damagedSound.Play();
            spriteRenderer.sprite = spriteDamaged;
            yield return new WaitForSeconds(1.5f);
            spriteRenderer.sprite = spriteOriginal;
        }
    }

    public void EnemyDefeated()
    {
        StartCoroutine(defeated());

        IEnumerator defeated()
        {
            print("The Enemy has been Defeated");
            spriteRenderer.sprite = spriteDefeated;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void EnemyTelegraph()
    {
        spriteRenderer.sprite = spriteTelegraph;
    }

    public void EnemyNormal()
    {
        spriteRenderer.sprite = spriteOriginal;
    }

}
