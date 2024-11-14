using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; 
    public TextMeshProUGUI dialogText; 
    public float typingSpeed = 0.05f; 

    private bool ongoing;
    private Coroutine typingCoroutine;
    private Coroutine timeupCoroutine;

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
            if (ongoing)
            {
                StopCoroutine(typingCoroutine);
                StopCoroutine(timeupCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(message));
            timeupCoroutine = StartCoroutine(HideText(message.Length * typingSpeed + 5));
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
        ongoing = true;
        dialogText.text = ""; 
        dialogText.gameObject.SetActive(true); 

        foreach (char letter in message.ToCharArray())
        {
            dialogText.text += letter; 
            yield return new WaitForSeconds(typingSpeed); 
        }
        ongoing = false;
    }

    private IEnumerator HideText(float timeout)
    {
        yield return new WaitForSeconds(timeout);
        dialogText.text = "";
    }
}
