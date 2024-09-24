using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string levelName;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bat") || other.gameObject.CompareTag("Lion"))
        {
            Debug.Log("You have completed the level");
            SceneManager.LoadScene(levelName);
        }
    }
}

