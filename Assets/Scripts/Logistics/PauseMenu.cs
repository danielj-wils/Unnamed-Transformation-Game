using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance; 

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private PlayerControls controls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        pauseMenuUI.SetActive(true);
    
        controls = new PlayerControls();
        
        // Initialize the controls
        controls.UI.Pause.performed += ctx => TogglePause();
    }

    private void OnEnable()
    {
        // Enable the input actions
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        controls.UI.Disable();
    }

    private void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
