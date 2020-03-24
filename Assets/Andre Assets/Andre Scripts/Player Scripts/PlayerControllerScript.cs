﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 8;

    [SerializeField]
    private float jumpForce = 300;
    [SerializeField]
    private float jumpTimer = 0;
    [SerializeField]
    private float jumpLimit = 1.5f;
    [SerializeField]
    private bool isJumping;

    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float groundedRayLength = 0.1f;
    [SerializeField]
    public LayerMask groundMask;
    [SerializeField]
    public Transform groundCheck;

    
    public int playerHealth = 3;
    public Text healthDisplay;

    public GameObject playerProjectile;

    public Transform firePoint;

    public Rigidbody rb;
    [SerializeField]
    private float startingMass;

    private Vector3 movementInput;
    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;

    public Transform mainCamera;
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        groundCheck = transform.GetChild(2);
        mainCamera = transform.GetChild(0);
        firePoint = mainCamera.GetChild(0);
        startingMass = rb.mass;
    }

    void Update()
    {
        PlayerInput();
        GroundCheck();
        PlayerFire();
        //SceneReset();
        CanvasDisplay();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        JumpInput();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
    }

    private void GroundCheck()
    {
        Ray groundedDetection = new Ray(groundCheck.position, -transform.up);
        RaycastHit groundedHit;
        Debug.DrawRay(groundedDetection.origin, groundedDetection.direction * groundedRayLength, Color.blue);

        isGrounded = Physics.Raycast(groundedDetection, groundedRayLength, groundMask);

        if (isGrounded == true)
        {
            isJumping = false;
            rb.mass = startingMass;
            jumpTimer = 0;
        }
    }
    private void JumpInput()
    {
            if (Input.GetKey(KeyCode.Space) && jumpTimer < jumpLimit && isJumping != true)
            {
                rb.AddForce(transform.up * (jumpForce * jumpInput), ForceMode.Impulse);
                jumpTimer += 1 * Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
            isJumping = true;
            }
    }

    private void PlayerMovement()
    {
        movementInput = new Vector3(horizontalInput, 0, verticalInput);
        // Changes movement direction from Global to Local translation
        movementInput = transform.TransformDirection(movementInput);
        rb.MovePosition(transform.position + (movementInput * playerSpeed * Time.deltaTime));
    }

    private void PlayerFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(playerProjectile, firePoint.position,mainCamera.rotation);
        }
    }

    private void PlayerDamage()
    {
        if (playerHealth >= 1)
        {
        playerHealth -= 1;
        }
        else
        {
            // Reloads the Scene (Requires the 'Using UnityEngine.SceneManager'
            //SceneManager.LoadScene("HubWorld_Scene");
        }
    }

    private void CanvasDisplay()
    {
        if (healthDisplay != null)
        { 
        healthDisplay.text = "Health: " + playerHealth.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerDamage();
            print(playerHealth);
        }
    }

    private void SceneReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("HubWorld_Scene");
        }
    }
}
