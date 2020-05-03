using System.Collections;
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

    private AudioSource stepSource;
    private AudioSource jumpSource;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        groundCheck = transform.GetChild(2);
        mainCamera = transform.GetChild(0);
        firePoint = mainCamera.GetChild(0);
        stepSource = groundCheck.GetComponent<AudioSource>();
        jumpSource = mainCamera.GetComponent<AudioSource>();
        startingMass = rb.mass;
    }

    void Update()
    {
        if (GameWatcher.gamePaused == false)
        { 
            PlayerInput();
            GroundCheck();
            PlayerFire();
            //SceneReset();
            CanvasDisplay();
            MovementSFX();
        }
    }

    void FixedUpdate()
    {
        if (GameWatcher.gamePaused == false)
        { 
            PlayerMovement();
            JumpInput();
        }
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
            //jumpSource.pitch = 0.8f;
            //jumpSource.Play();
            isJumping = false;
            rb.mass = startingMass;
            jumpTimer = 0;

        }
    }
    private void JumpInput()
    {
            if (Input.GetKey(KeyCode.Space) && jumpTimer < jumpLimit && isJumping != true)
            {
                jumpSource.pitch = 1;
                jumpSource.Play();
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

    private void MovementSFX()
    {
        if (stepSource.isPlaying == false && isGrounded && (Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0))
        {
            stepSource.volume = Random.Range(0.9f, 1);
            stepSource.pitch = Random.Range(0.9f, 1.0f);
            stepSource.Play();
        }
    }

    private void PlayerFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(playerProjectile, firePoint.position,mainCamera.rotation);
        }
    }

    private void PlayerHit()
    {
        GameWatcher.PlayerDamage();
        if (playerHealth <= 0)
        {
            GameWatcher.playerAlive = false;
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
        if (other.CompareTag("EnemyHurtbox"))
        {
            
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
