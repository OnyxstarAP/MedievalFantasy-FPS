using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager_Script : MonoBehaviour
{
    [SerializeField]
    private bool interact;

    public Transform player;
    public Sprite hurtSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Projectile")
        {
            spriteRenderer.sprite = hurtSprite;
        }
    }
}
