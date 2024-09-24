using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public static BatMovement instance;

    private PlayerControls controls;

    private Rigidbody2D rb;

    private Vector2 moveInput; // Input vector from the player
    public float speed = 5f; // Speed of the bat's movement
    public float normalDrag = 1f; // Drag when the bat is moving normally
    public float slowDrag = 5f; // Drag when the bat is slowing down or hovering
    
    private float horizontalFlyingMultiplier = 1.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        rb = GetComponent<Rigidbody2D>();

        // Initialize the controls
        controls = new PlayerControls();

        // Set up input callbacks
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
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

    void FixedUpdate()
    {
        // Move the bat in the direction of the input
        Vector2 movement = speed * Time.deltaTime * moveInput;

        // If no platform is detected in the raycast direction, allow normal movement
        rb.AddForce(movement, (ForceMode2D)ForceMode.Acceleration);

        //make horizontal flying every so slightly faster
        if(movement.y != 0)
        {
            movement.y = speed * horizontalFlyingMultiplier * Time.deltaTime;
        }

        // Adjust drag based on movement speed
        if (movement.magnitude > 0.1f)
        {
            rb.drag = normalDrag;  // Less drag when moving
        }
        else
        {
            rb.drag = slowDrag;  // More drag when slowing down or hovering
        }

        Flip(); // Call the Flip method to flip the sprite based on the direction of movement
    }


    private void Flip()
    {
        // Check if the bat is moving left or right and flip the sprite accordingly
        if (moveInput.x > 0 && transform.localScale.x < 0)
        {
            // Moving right, face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x < 0 && transform.localScale.x > 0)
        {
            // Moving left, face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
