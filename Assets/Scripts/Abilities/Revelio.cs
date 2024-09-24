using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revelio : MonoBehaviour
{
    public static Revelio instance;
    private PlayerControls controls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Initialize the controls
        controls = new PlayerControls();

        // Set up input callbacks
        controls.Player.Revelio.performed += ctx => CastRevelio();
    }

    private void CastRevelio()
    {
        Debug.Log("Revelio cast successfully");
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
}
