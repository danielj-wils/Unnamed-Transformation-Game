using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject startPoint;
    public static PlayerMovement player;
    public TrailRenderer trailRenderer;

    private GameObject currentCharacter;
    
    private void Start() 
    {
        FindDepndencies();
    }

    private void Update() 
    {
        FindDepndencies();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bat") || other.gameObject.CompareTag("Lion"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    private IEnumerator ResetPlayer()
    {
        // Disable the TrailRenderer immediately
        trailRenderer.enabled = false;
        
        // Reset the player's rotation to the default (identity)
        
        // Optionally, reset other player states here
        // For example, you might need to reset velocity, animations, etc.
        //player.RigidBody2D.velocity = Vector2.zero;
        // Move the player to the start point
        currentCharacter.transform.SetPositionAndRotation(startPoint.transform.position, new Quaternion(0,0,0,0));

        // Wait for a short time to ensure the trail is not visible
        yield return new WaitForSeconds(0.5f);

        // Re-enable the TrailRenderer
        trailRenderer.enabled = true;
    }

    private void FindDepndencies()
    {
        if (player == null) 
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        if (trailRenderer == null) 
        {
            trailRenderer = FindObjectOfType<TrailRenderer>();
        }

        if(startPoint == null)
        {
            startPoint = GameObject.Find("StartPoint");
        }

        currentCharacter = AnimalChange.instance.currentCharacter;
    }
}
