using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [Header("Flag Settings")]
    public string flag = "DEFAULT_FLAG";  // The flag value that will be printed when player interacts with chest

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the player entering the trigger
        if (other.CompareTag("Player"))
        {
            PrintFlag();
        }
    }

    private void PrintFlag()
    {
        Debug.Log("Flag: " + flag);
        // You can also display this in UI or trigger some other win condition
    }
}
