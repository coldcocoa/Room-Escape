using UnityEngine;
using TMPro;
using DG.Tweening; // DOTween ���
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class Dialogue_Lobby : MonoBehaviour
{
    public Text dialogueText;     // ��� �ؽ�Ʈ (TextMeshPro ���)
    public GameObject dialogueBox;           // ��� �ڽ� (Panel)
    public string[] dialogues;               // ��� ���
    public float typingSpeed = 0.05f;        // �� ���� ��� �ӵ�
    public float fadeDuration = 0.3f;        // �ؽ�Ʈ ���̵� �ð�
    private int currentDialogueIndex = 0;    // ���� ��� �ε���
    private bool isTyping = false;           // ��� ��� ������ Ȯ��
    private bool canProceed = false;         // ���� ���� �Ѿ �� �ִ��� Ȯ��

    void Start()
    {
        dialogueBox.SetActive(true); // ��� �ڽ� Ȱ��ȭ
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // ù ��� ���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            if (canProceed)
            {
                if (currentDialogueIndex < dialogues.Length - 1)
                {
                    currentDialogueIndex++;
                    StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
                }
                else
                {
                    EndDialogue(); // ��� ��� ���� �� ó��
                }
            }
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        canProceed = false;
        dialogueText.text = "";
        dialogueText.DOFade(0, 0f);  // ���� �����ϰ� (ó���� �� ���̰�)
        yield return new WaitForSeconds(0.1f);

        dialogueText.DOFade(1, fadeDuration);  // ���� ������ ��Ÿ��
        foreach (char letter in dialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // �� ���ھ� ���
        }

        isTyping = false;
        canProceed = true;  // ���� ���� �Ѿ �غ� �Ϸ�
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false); // ��� �ڽ� ��Ȱ��ȭ
        SceneManager.LoadScene("Main"); // MainScene���� �̵�
    }
}
