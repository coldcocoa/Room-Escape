using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITextTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup uiCanvasGroup; // UI 패널 (페이드 인/아웃)
    public RectTransform uiTransform; // UI 크기 조절
    public Text uiText; // 텍스트 출력
    public string fullText; // 출력할 전체 문장
    public Transform uiWorldPosition; // UI가 배치될 월드 좌표 (예: 책상 위, 벽 등)

    [Header("Settings")]
    public float fadeDuration = 0.5f; // UI 페이드 애니메이션 지속 시간
    public float scaleDuration = 0.5f; // UI 스케일 애니메이션 지속 시간
    public float typingSpeed = 0.05f; // 한 글자씩 출력되는 속도

    private bool isTyping = false;
    private bool isPlayerInside = false;

    private void Awake()
    {
        // 자동으로 CanvasGroup 찾기 (Inspector에서 누락될 경우 대비)
        if (uiCanvasGroup == null)
        {
            uiCanvasGroup = GetComponentInChildren<CanvasGroup>();
            if (uiCanvasGroup == null)
            {
                Debug.LogError("[UITextTrigger] UI CanvasGroup이 할당되지 않았습니다! Inspector에서 설정해주세요.");
            }
        }
    }

    private void Start()
    {
        // 시작 시 UI를 투명하게 설정
        uiCanvasGroup.alpha = 0;
        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;
        uiTransform.localScale = Vector3.zero; // 크기를 0으로 시작 (애니메이션 효과용)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTyping)
        {
            isPlayerInside = true;
            ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            HideUI();
        }
    }

    private void ShowUI()
    {
        if (uiCanvasGroup == null) return; // 오류 방지

        // UI가 특정 월드 좌표에 배치되도록 설정
        if (uiWorldPosition != null)
        {
            uiTransform.position = uiWorldPosition.position;
            uiTransform.LookAt(Camera.main.transform);
            uiTransform.Rotate(0, 180, 0);
        }

        // UI 활성화 및 애니메이션 실행
        uiCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutSine);
        uiTransform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);

        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;

        // 타이핑 효과 실행
        StartCoroutine(TypeText());
    }

    private void HideUI()
    {
        if (uiCanvasGroup == null) return; // 오류 방지

        // UI 비활성화 애니메이션 실행
        uiCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.InOutSine);
        uiTransform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack);

        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;

        // 텍스트 초기화
        uiText.text = "";
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        uiText.text = ""; // 기존 텍스트 초기화

        foreach (char letter in fullText)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
