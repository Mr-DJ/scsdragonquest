using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string dialogMessage; // The dialog message to display
    public string targetTag = "Player"; // Tag to identify the triggering object
    public bool triggerOnce = true;
    
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the specified tag
        if (other.CompareTag(targetTag) && (triggerOnce && !triggered || !triggerOnce))
        {
            // Show the dialog message
            DialogManager.Instance.ShowDialog(dialogMessage);
            triggered = true;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     // Check if the object exiting the trigger has the specified tag
    //     if (other.CompareTag(targetTag))
    //     {
    //         // Hide the dialog message
    //         DialogManager.Instance.HideDialog();
    //     }
    // }
}
