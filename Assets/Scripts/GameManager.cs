using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;  // Drag and drop the player GameObject here
    public GameObject dragon;  // Drag and drop the dragon GameObject here
    public GameObject chestPrefab;  // Drag and drop your chest prefab here
    public Transform chestSpawnPoint;  // The position where the chest will spawn (on platform)

    private EntityHealth playerHealth;
    private EntityHealth dragonHealth;

    private bool gameEnded = false; // Prevent multiple triggers

    void Start()
    {
        // Automatically get the EntityHealth component from the player and dragon
        if (player != null)
        {
            playerHealth = player.GetComponent<EntityHealth>();
            if (playerHealth != null)
            {
                playerHealth.onDeath.AddListener(OnPlayerDeath);
            }
            else
            {
                Debug.LogError("Player does not have an EntityHealth component!");
            }
        }

        if (dragon != null)
        {
            dragonHealth = dragon.GetComponent<EntityHealth>();
            if (dragonHealth != null)
            {
                dragonHealth.onDeath.AddListener(OnDragonDeath);
            }
            else
            {
                Debug.LogError("Dragon does not have an EntityHealth component!");
            }
        }
    }

    void OnPlayerDeath()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("Player has died! Game Over.");
            LoseGame();
        }
    }

    void OnDragonDeath()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("Dragon has been slain! You win!");
            WinGame();
        }
    }

    void WinGame()
    {
        // Securely handle win logic here (e.g., show victory screen, update server-side score)
        Debug.Log("Victory! Player has won.");

        // Instantiate chest at specified spawn point on platform
        if (chestPrefab != null && chestSpawnPoint != null)
        {
            Instantiate(chestPrefab, chestSpawnPoint.position, Quaternion.identity);
            Debug.Log("Chest spawned at platform.");
        }
        else
        {
            Debug.LogError("Chest prefab or spawn point not assigned!");
        }
        
        // Implement further logic like loading a victory screen or updating scores
    }

    void LoseGame()
    {
        Debug.Log("Defeat! Player has lost.");
        // Implement further logic like loading a game over screen
    }

    private void OnDrawGizmos()
    {
        if (chestSpawnPoint != null)
        {
            Gizmos.color = Color.cyan; // Set gizmo color for visibility
            Gizmos.DrawWireSphere(chestSpawnPoint.transform.position, 0.5f); // Draw sphere at attack point position
        }
    }
}
