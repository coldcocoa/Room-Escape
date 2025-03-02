using UnityEngine;
using TMPro;
using DG.Tweening; // DOTween 사용
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class Dialogue_Lobby : MonoBehaviour
{
    public Text dialogueText;     // 대사 텍스트 (TextMeshPro 사용)
    public GameObject dialogueBox;           // 대사 박스 (Panel)
    public string[] dialogues;               // 대사 목록
    public float typingSpeed = 0.05f;        // 한 글자 출력 속도
    public float fadeDuration = 0.3f;        // 텍스트 페이드 시간
    private int currentDialogueIndex = 0;    // 현재 대사 인덱스
    private bool isTyping = false;           // 대사 출력 중인지 확인
    private bool canProceed = false;         // 다음 대사로 넘어갈 수 있는지 확인

    void Start()
    {
        dialogueBox.SetActive(true); // 대사 박스 활성화
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // 첫 대사 출력
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
                    EndDialogue(); // 모든 대사 종료 시 처리
                }
            }
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        canProceed = false;
        dialogueText.text = "";
        dialogueText.DOFade(0, 0f);  // 글자 투명하게 (처음에 안 보이게)
        yield return new WaitForSeconds(0.1f);

        dialogueText.DOFade(1, fadeDuration);  // 글자 서서히 나타남
        foreach (char letter in dialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // 한 글자씩 출력
        }

        isTyping = false;
        canProceed = true;  // 다음 대사로 넘어갈 준비 완료
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false); // 대사 박스 비활성화
        SceneManager.LoadScene("Main"); // MainScene으로 이동
    }
}
