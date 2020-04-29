using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [SerializeField]
    private bool followPlayer = true;
    [SerializeField]
    private bool XAxisFollow = false;

    [SerializeField]
    public Transform billboardTarget;
    [SerializeField]
    private Sprite spriteNeutral;
    [SerializeField]
    private Sprite spriteDamaged;
    [SerializeField]
    private Sprite spriteDefeated;
    [SerializeField]
    private Sprite spriteTelegraph;

    private Transform spriteSource;

    private SpriteRenderer spriteRenderer;

    public AudioSource damagedSound;

    private void Awake()
    {
        if (followPlayer)
        { 
        billboardTarget = GameObject.Find("Player").transform;
        }

        if (transform.childCount > 0)
        {
            spriteSource = transform.GetChild(0);
        }
        else if (transform.childCount == 0)
        {
            spriteSource = transform;
        }

        spriteRenderer = spriteSource.GetComponent<SpriteRenderer>();
        spriteNeutral = spriteSource.GetComponent<SpriteRenderer>().sprite;

        damagedSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        if (billboardTarget != null)
        {
            if (XAxisFollow)
            {
                transform.LookAt(billboardTarget); 
            }
            else
            {
                Vector3 TagerPosition = new Vector3(billboardTarget.position.x, transform.position.y, billboardTarget.position.z);
                transform.LookAt(TagerPosition);
            }
        }
    }

    public IEnumerator defeated()
    {
        print(gameObject.name + "has been Defeated");
        spriteRenderer.sprite = spriteDefeated;
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator damaged()
    {
        print("Enemy was Damaged");
        damagedSound.Play();
        spriteRenderer.sprite = spriteDamaged;
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.sprite = spriteNeutral;
    }

    public void EnemyNeutral()
    {
        // Shown whenthe Enemy is not performing an action
        spriteRenderer.sprite = spriteNeutral;
    }
    public void EnemyDamaged()
    {
        // Shown when the Enemy is Damaged
        spriteRenderer.sprite = spriteDamaged;
        // Plays the Enemy Damaged Sound
        //damagedSound.Play();
    }
    public void EnemyTelegraph()
    {
        // Shown before the Enemy attacks
        spriteRenderer.sprite = spriteTelegraph;
    }
    public void EnemyDefeat()
    {
        // Shown once the Enemy has been defeated
        spriteRenderer.sprite = spriteDefeated;
    }
}
