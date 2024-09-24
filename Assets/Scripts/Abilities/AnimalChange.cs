using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalChange : MonoBehaviour
{
    public static AnimalChange instance;

    public static GameObject startPoint;
    public PlayerControls controls;

    public GameObject playerPrefab;  // The original player prefab
    public GameObject batPrefab;     // The prefab to change into
    public GameObject lionPrefab;    // The lion prefab
    
    [SerializeField] public GameObject currentCharacter;  // Reference to the current character
    [SerializeField] private bool isBatActive = false;     // Track whether the bat is currently active
    [SerializeField] private bool isPlayerActive = true;   // Track whether the player is currently active
    [SerializeField] private bool isLionActive = false;    // Track whether the lion is currently active

    private Vector3 characterPosition;
    private Vector3 storedVelocity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Initialize the controls
        controls = new PlayerControls();

        // Set up input callbacks
        controls.Player.BatChange.performed += ctx => SwitchToBat();
        controls.Player.LionChange.performed += ctx => SwitchToLion();
        controls.Player.PlayerChange.performed += ctx => SwitchToPlayer();
    }

    private void Start()
    {
        if(startPoint == null)
        {
            startPoint = GameObject.Find("StartPoint");
        }

        // Spawn point for the first character instantiation
        characterPosition = startPoint.transform.position;

        // Find the existing player object in the scene and assign it
        if (currentCharacter == null)
        {
            // Create a player character initially
            currentCharacter = Instantiate(playerPrefab, characterPosition, Quaternion.identity);
            // Update the camera to follow the existing character
            CameraMovement.instance.SetTarget(currentCharacter.transform);
            isPlayerActive = true;
        }
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

    private void SwitchToBat()
    {
        if (isPlayerActive || isLionActive)  // If the player or lion is active, switch to bat
        {
            DestroyCharacter();
            currentCharacter = Instantiate(batPrefab, characterPosition, Quaternion.identity);
            isPlayerActive = false;
            isBatActive = true;
            isLionActive = false;
        }

        ApplyStoredVelocity();
    }

    private void SwitchToLion()
    {
        if (isPlayerActive || isBatActive)  // If the player or bat is active, switch to lion
        {
            DestroyCharacter();
            currentCharacter = Instantiate(lionPrefab, characterPosition, Quaternion.identity);
            isPlayerActive = false;
            isBatActive = false;
            isLionActive = true;
        }

        ApplyStoredVelocity();
    }

    private void SwitchToPlayer()
    {
        if (isLionActive || isBatActive)  // If the player or bat is active, switch to lion
        {
            DestroyCharacter();
            currentCharacter = Instantiate(playerPrefab, characterPosition, Quaternion.identity);
            isPlayerActive = true;
            isBatActive = false;
            isLionActive = false;
        }

        ApplyStoredVelocity();
        
    }

    private void DestroyCharacter()
    {
        if (currentCharacter != null)
        {
            // If the character has a Rigidbody, store its velocity
            if (currentCharacter.TryGetComponent<Rigidbody2D>(out var rb))
            {
                storedVelocity = rb.velocity;
            }

            // Store the position of the current character
            characterPosition = currentCharacter.transform.position;

            // Destroy the current character
            Destroy(currentCharacter);
        }
    }

    private void ApplyStoredVelocity()
    {
        if (currentCharacter != null)
        {
            if (currentCharacter.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.velocity = storedVelocity;
            }

            // Update the camera to follow the new character
            CameraMovement.instance.SetTarget(currentCharacter.transform);
        }
    }
}
