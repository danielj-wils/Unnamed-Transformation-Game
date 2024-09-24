using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //public ICollectableBehaviour collectableBehaviour;
    public static PlayerMovement player;

    private void Start() 
    {
        if (player == null) 
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }
    
    private void Update() 
    {
        if (player == null) 
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        //collectableBehaviour = GetComponent<ICollectableBehaviour>();
//
        //// Check if the component is found
        //if (collectableBehaviour == null)
        //{
        //    Debug.LogError("ICollectableBehaviour not found on this GameObject.");
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}
