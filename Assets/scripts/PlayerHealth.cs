using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float poisonTimer = 0f;
    public float poisonInterval = 1f; // seconds between poison ticks
    public int poisonDamage = 5;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player died");
            // Add death logic here
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void TakePoisonDamage()
    {
        poisonTimer += Time.deltaTime;
        if (poisonTimer >= poisonInterval)
        {
            TakeDamage(poisonDamage);
            Debug.Log("Player poisoned! Current health: " + currentHealth);
            poisonTimer = 0f;
        }
    }
}
