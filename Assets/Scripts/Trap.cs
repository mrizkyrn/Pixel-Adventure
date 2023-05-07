using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private PlayerController player;

    public virtual void Start()
    {
        // Find the GameObject with the name "Player"
        GameObject playerObject = GameObject.Find("Player");

        // Check if the playerObject is not null
        if (playerObject != null)
        {
            // Get the Player component from the playerObject
            player = playerObject.GetComponent<PlayerController>();

            if (player == null)
            {
                Debug.LogError("Player component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with the name 'Player' not found.");
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.rb.bodyType = RigidbodyType2D.Static;
            player.animator.SetTrigger("Death");
        }
    }
}
