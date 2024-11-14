using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damageAmount = 100; // Damage dealt by fireball
    public float timeToLive = 5f; // Time in seconds before the fireball is destroyed
    public LayerMask targetLayer; // LayerMask for valid targets (e.g., Player)

    void Start()
    {
        // Destroy the fireball after 'timeToLive' seconds
        Destroy(gameObject, timeToLive);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the object hit is part of the target layer (e.g., Player)
        if (IsTarget(hitInfo.gameObject))
        {
            if (hitInfo.TryGetComponent<EntityHealth>(out var targetHealth))
            {
                targetHealth.TakeDamage(damageAmount); // Deal damage to valid target
                Destroy(gameObject); // Destroy fireball after hitting something
            }
        }

        // Optionally destroy if it hits anything else like walls or obstacles
        if (IsObstacle(hitInfo.gameObject))
        {
            Destroy(gameObject); // Destroy fireball if it hits an obstacle
        }
    }

    // Method to check if the object is part of the target layer (e.g., Player)
    bool IsTarget(GameObject obj)
    {
        return ((1 << obj.layer) & targetLayer) != 0;
    }

    // Method to check if the object is part of an obstacle layer (optional)
    bool IsObstacle(GameObject obj)
    {
        return ((1 << obj.layer) & LayerMask.GetMask("Obstacle")) != 0;
    }
}
