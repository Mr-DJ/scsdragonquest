using UnityEngine;
public class ChestInteraction : MonoBehaviour
{
    private DialogManager dialogManager;
    
    [Header("Flag Settings")]
    public string flag;  // The flag value that will be printed when player interacts with chest

    private void Start()
    {
        // Get flag value from environment variable or define symbol (if applicable)
#if UNITY_EDITOR
        // For testing in editor, you can set this manually
        flag = "EDITOR_FLAG";
#else
        // In production or compiled builds, get from environment variable or define symbol
        flag = GetFlagFromEnvironment();
#endif

        dialogManager = DialogManager.Instance;
    }

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
        string message = "Congratulations! Your secret flag is: " + flag;
        dialogManager.ShowDialog(message);
        Debug.Log("Flag: " + flag);
    }

    private string GetFlagFromEnvironment()
    {
        // Example of getting an environment variable (you can set this during build)
        string envFlag = System.Environment.GetEnvironmentVariable("GAME_FLAG");

        if (!string.IsNullOrEmpty(envFlag))
        {
            return envFlag;
        }
        
        // Default fallback if no environment variable is set
        return "DEFAULT_FLAG";
    }
}
