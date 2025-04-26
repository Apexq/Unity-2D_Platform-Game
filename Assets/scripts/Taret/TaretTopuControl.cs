using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaretTopuControl : MonoBehaviour
{

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            {
            playerHealth.TakeDamage(10);
            Debug.Log("Player hit by cannonball! Current health: " + playerHealth.currentHealth);
            }
        // Destroy the cannonball when it hits the player
        Destroy(gameObject);
    }
    if(collision.CompareTag("Ground") || collision.CompareTag("Ceiling") || collision.CompareTag("Wall"))
    {
        // Destroy the cannonball when it hits the ground, ceiling, or wall
        Destroy(gameObject);
    }
}
}
