using UnityEngine;
using UnityEngine.UI;

public class BookItem : MonoBehaviour, IInteractable
{
    public string BookYear; // 책의 연도 (예: "2024")
    public string title; // 책 제목

    public GameObject bookUIPanel; // UI 패널 (책 획득 메시지)
    public Text bookUIText; // UI에 표시될 텍스트
    public GameObject bookPlacementObject; // 활성화할 책 오브젝트 (테이블 위에 배치되는 책)

    private bool isCollected = false; // 책을 이미 획득했는지 여부

    public InteractionSoundManager soundManager;

    public void Interact()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<InteractionSoundManager>();
        if (isCollected) return; // 이미 획득한 경우 무시

        isCollected = true;
        ShowBookUI(); // UI 활성화
        ActivatePlacedBook(); // 새로운 위치에 책 활성화
        gameObject.SetActive(false); // 현재 책 비활성화
    }

    private void ShowBookUI()
    {
        if (bookUIPanel != null && bookUIText != null)
        {
            soundManager.PlaybookPickUpSound();
            bookUIPanel.SetActive(true);
            bookUIText.text = $"책을 획득했습니다!\n\n제목: {title}\n출판년도: {BookYear}"; // 텍스트 변경
            Invoke("HideBookUI", 4f); // 2초 후 UI 자동 숨김
        }
    }

    private void HideBookUI()
    {
        if (bookUIPanel != null)
        {
            bookUIPanel.SetActive(false);
        }
    }

    private void ActivatePlacedBook()
    {
        if (bookPlacementObject != null)
        {
            bookPlacementObject.SetActive(true); // 새로운 위치의 책 활성화
        }
    }
}
