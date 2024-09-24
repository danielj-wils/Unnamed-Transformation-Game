using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public Transform target;  // Changed to a more generic Transform

    // Multiple in which the player hits for the camera to move
    public float yAxisMultiple;

    // Smoothing speed for the camera movement
    public float smoothSpeed;

    private float targetY;  // Store target Y position for smooth movement

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (target == null)
        {
            // Automatically find the Player object initially
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if (player != null)
            {
                target = player.transform;
                Debug.Log("Player found by Camera.");
            }
        }

        targetY = transform.position.y;  // Initialize targetY with the camera's current Y position
    }

    void Update()
    {
        if (target == null) return;  // If no target, do nothing

        if (target.CompareTag("Player") || target.CompareTag("Lion"))
        {
            // Always match the camera's X position with the target's X position instantly
            Vector3 targetPosition = new(target.position.x, transform.position.y, -10);

            // Check if the target's Y position is close to a multiple of the specified value
            if (Mathf.Abs(target.position.y % yAxisMultiple) < 0.1f || Mathf.Abs(target.position.y % yAxisMultiple) > yAxisMultiple - 0.1f)
            {
                // Set the target Y position to the target's Y position
                targetY = target.position.y;
            }

            // Smoothly interpolate the Y position of the camera to the target Y position
            float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed);

            // Update the camera position with the new smoothed Y position and fixed X position
            transform.position = new Vector3(target.position.x, smoothedY, -10);

            if (target.position.y - targetPosition.y <= 1f)
            {
                targetY = target.position.y;
            }
        }
        else if (target.CompareTag("Bat"))
        {
            //Debug.Log("Bat found by Camera.");
            
            // Always match the camera's X position with the target's X position instantly
            Vector3 targetPosition = new(target.position.x, transform.position.y, -10);

            //// Check if the target's Y position is close to a multiple of the specified value
            //if (Mathf.Abs(target.position.y % yAxisMultiple) < 0.1f || Mathf.Abs(target.position.y % yAxisMultiple) > yAxisMultiple - 0.1f)
            //{
            //    // Set the target Y position to the target's Y position
            //    targetY = target.position.y;
            //}

            // Smoothly interpolate the Y position of the camera to the target Y position
            float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed);

            // Update the camera position with the new smoothed Y position and fixed X position
            transform.position = new Vector3(target.position.x, target.position.y, -10);

            if (target.position.y - targetPosition.y <= 1f)
            {
                targetY = target.position.y;
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;  // Update the target to follow the new object
    }
}
