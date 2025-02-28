using UnityEngine;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    public static BookUIManager Instance;
    public GameObject bookMessagePanel;
    public Text bookMessageText;
    public Text bookMessageText2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowMessage(string message,string message2)
    {
        if (bookMessagePanel != null && bookMessageText != null)
        {
            bookMessagePanel.SetActive(true);
            bookMessageText.text = message;
            bookMessageText2.text = message2;
            Invoke("HideMessage", 5f); // 2ÃÊ ÈÄ ÀÚµ¿ ¼û±è
        }
    }

    private void HideMessage()
    {
        if (bookMessagePanel != null)
        {
            bookMessagePanel.SetActive(false);
        }
    }
}
