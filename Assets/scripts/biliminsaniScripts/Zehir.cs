using UnityEngine;

public class Zehir : MonoBehaviour
{
    private Collider2D other; // Store the collider reference
    private bool isEntered=false;
       void Update()
    {
        if (isEntered)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakePoisonDamage();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            this.other = other; // Store the collider reference
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
    {
        isEntered = false;
    }
    }
}
