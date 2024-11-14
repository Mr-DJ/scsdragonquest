using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; 
    public Text dialogText; 
    public float typingSpeed = 0.05f; 

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void ShowDialog(string message)
    {
        if (dialogText != null)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(message));
        }
    }

    public void HideDialog()
    {
        if (dialogText != null)
        {
            dialogText.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeText(string message)
    {
        dialogText.text = ""; 
        dialogText.gameObject.SetActive(true); 

        foreach (char letter in message.ToCharArray())
        {
            dialogText.text += letter; 
            yield return new WaitForSeconds(typingSpeed); 
        }
    }
}
