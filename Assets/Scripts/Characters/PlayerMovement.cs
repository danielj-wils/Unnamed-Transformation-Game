using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Animator animator;

    private PlayerControls controls;
    
    private Rigidbody2D rb;

    private Vector2 moveInput;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float lowJumpMultiplier = 2f;
    public float fallMultiplier = 3f;
    public float velocitySmoothing = 0.1f; // Smoothing factor for velocity

    [SerializeField] public float positionChangeLeftX = 1;
    [SerializeField] public float positionChangeRightX = 1;


    public bool isJumping;
    private bool isFalling; // Track if the player is falling

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            //Debug.Log("Player Awake: created and marked as DontDestroyOnLoad.");
        }
        
        rb = GetComponent<Rigidbody2D>();     

        // Initialize the controls
        controls = new PlayerControls();

        // Set up input callbacks
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();   

        isJumping = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Enable the input actions
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        controls.Player.Disable();
    }

    void Update()
    {
    animator.SetFloat("Speed", Mathf.Abs(moveInput.x)); 
    }

    void FixedUpdate()
    {
        // Horizontal movement
        //moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        // Update isFalling based on vertical velocity
        isFalling = rb.velocity.y < 0;

        // Jumping logic
        //if (Input.GetButtonDown("Jump") && !isJumping)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //    isJumping = true; // Set to true after jumping
        //}

        // Applying gravity multipliers for better jump control
        if (isFalling)
        {
            // If the player is falling, increase downward velocity smoothly
            float targetYVelocity = rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, targetYVelocity), velocitySmoothing);
        }
        else if (rb.velocity.y > 0 && !controls.Player.Jump.IsPressed())
        {
            // If the player releases the jump button while ascending, reduce upward velocity smoothly
            float targetYVelocity = rb.velocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, targetYVelocity), velocitySmoothing);
        }

        Flip();
    }

    private void Flip()
    {
        // Check if the player is moving left or right and flip the sprite accordingly
        if (moveInput.x > 0 && transform.localScale.x < 0)
        {
            // Moving right, face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x + positionChangeRightX, transform.position.y, transform.position.z);
        }
        else if (moveInput.x < 0 && transform.localScale.x > 0)
        {
            // Moving left, face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x - positionChangeLeftX, transform.position.y, transform.position.z);
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    // Ground collision
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isJumping = false; // Reset isJumping when player touches the ground
    //    }
    //}
//
    //private void OnCollisionExit2D(Collision2D other)
    //{
    //  // Leaving the ground
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isJumping = true; // Set isJumping to true when leaving the ground
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Entering the leeway zone
        if (other.CompareTag("GroundLeeway") && isFalling)
        {
            isJumping = false; // Only allow jumping again if the player is falling
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Leaving the leeway zone
        if (other.CompareTag("GroundLeeway"))
        {
            isJumping = true; // Disable jumping when leaving the leeway zone
        }
    }
}