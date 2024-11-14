using UnityEngine;
using UnityEngine.Events; // For event handling

public class EntityHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maximum health for the entity
    private int currentHealth; // Current health of the entity

    [Header("Death Settings")]
    public bool isDead = false; // Tracks if the entity is dead

    [Header("Events")]
    public UnityEvent onDeath; // Event triggered when the entity dies
    public UnityEvent onTakeDamage; // Event triggered when the entity takes damage

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        isDead = false;

        // No need to set onDeath or onTakeDamage to null, Unity initializes them automatically
    }

    // Method to apply damage to the entity
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Don't take damage if already dead

        currentHealth -= damageAmount;

        // Trigger onTakeDamage event (can be used for playing hurt animations, etc.)
        onTakeDamage?.Invoke();

        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current health: " + currentHealth);

        // Check if health has dropped to 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal the entity (optional)
    public void Heal(int healAmount)
    {
        if (isDead) return; // Can't heal if dead

        currentHealth += healAmount;

        // Ensure health doesn't exceed max health
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log(gameObject.name + " healed by " + healAmount + ". Current health: " + currentHealth);
    }

    // Method to handle death of the entity
    private void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " has died.");

        // Trigger onDeath event (can be used for playing death animations, etc.)
        onDeath?.Invoke();

        // Disable or destroy the GameObject upon death (optional)
        // gameObject.SetActive(false);
    }

    // Method to check if the entity is dead
    public bool IsDead()
    {
        return isDead;
    }

    // Method to get current health value (useful for UI or debugging purposes)
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
