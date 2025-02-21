using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueInteraction_Library : MonoBehaviour, IInteractable
{ 
    [Header("UI Elements")]
    public GameObject dialoguePanel;  // ��ȭ UI �г�
    public Text dialogueText;         // ��ȭ �ؽ�Ʈ ǥ�ÿ�
    public float requiredDistance = 5f; // ��ȣ�ۿ� ���� �Ÿ�

    [Header("Dialogue Settings")]
    public string[] dialogues;        // ��ȭ ���� �迭
    private int currentDialogueIndex = 0;

    void Start()
    {
        // ������ �� ��ȭ UI ��Ȱ��ȭ
        dialoguePanel.SetActive(false);

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Input_G();
        }
    }
    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= requiredDistance)
            {
                currentDialogueIndex = 0;
                dialoguePanel.SetActive(true);
                dialogueText.text = dialogues[currentDialogueIndex];
            }       
        }
    }
    
    public void Input_G()
    {
        DisplayNextDialogue();
    }
    // ��ư Ŭ�� �� ���� ��ȭ�� ��ȯ
    public void DisplayNextDialogue()
    {

        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    // ��ȭ ���� �� UI �г� ��Ȱ��ȭ
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
