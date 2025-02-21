using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueInteraction_Library : MonoBehaviour, IInteractable
{ 
    [Header("UI Elements")]
    public GameObject dialoguePanel;  // 대화 UI 패널
    public Text dialogueText;         // 대화 텍스트 표시용
    public float requiredDistance = 5f; // 상호작용 가능 거리

    [Header("Dialogue Settings")]
    public string[] dialogues;        // 대화 내용 배열
    private int currentDialogueIndex = 0;

    void Start()
    {
        // 시작할 때 대화 UI 비활성화
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
    // 버튼 클릭 시 다음 대화로 전환
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

    // 대화 종료 후 UI 패널 비활성화
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
