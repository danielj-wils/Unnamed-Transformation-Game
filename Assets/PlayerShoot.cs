using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPoint;

    public PlayerControls controls;

    private void Awake()
    {
        // Initialize the controls
        controls = new PlayerControls();

        // Set up input callbacks
        controls.Player.Fire.performed += ctx => ShootProjectile();
    }

    private void OnEnable()
    {
        // Enable the input actions
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions to prevent memory leaks
        controls.Player.Disable();
    }

    private void ShootProjectile()
    {
        projectile = ObjectPool.instance.GetProjectileObject();
        Debug.Log("Got object from pool");
            if (projectile != null)
            {   
                Debug.Log("set object as active");
                projectile.transform.SetPositionAndRotation(shootPoint.position, Quaternion.identity);
                projectile.SetActive(true);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Assuming you want the projectile to move in a certain direction
                    rb.velocity = shootPoint.right * 20f; // Example velocity, adjust as needed
                }
            }
    }

}
