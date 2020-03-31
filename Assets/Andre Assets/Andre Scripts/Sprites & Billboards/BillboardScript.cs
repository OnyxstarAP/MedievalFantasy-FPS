using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [SerializeField]
    private bool followPlayer = true;
    [SerializeField]
    private bool XAxisFollow = false;
    public Transform target;

    private Transform enemySprite;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite spriteOriginal;
    [SerializeField]
    private Sprite spriteDamaged;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
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
                Vector3 TagerPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(TagerPosition);
            }
            else if (XAxisFollow == true)
            {
                transform.LookAt(target);
            }
        }
    }

    public void EnemyDamaged()
    {
        StartCoroutine(damaged());
    }
     IEnumerator damaged()
    {
        print("Damaged is working");
        spriteRenderer.sprite = spriteDamaged;
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.sprite = spriteOriginal;
    }

}
