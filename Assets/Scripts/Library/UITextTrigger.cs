using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITextTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup uiCanvasGroup; // UI �г� (���̵� ��/�ƿ�)
    public RectTransform uiTransform; // UI ũ�� ����
    public Text uiText; // �ؽ�Ʈ ���
    public string fullText; // ����� ��ü ����
    public Transform uiWorldPosition; // UI�� ��ġ�� ���� ��ǥ (��: å�� ��, �� ��)

    [Header("Settings")]
    public float fadeDuration = 0.5f; // UI ���̵� �ִϸ��̼� ���� �ð�
    public float scaleDuration = 0.5f; // UI ������ �ִϸ��̼� ���� �ð�
    public float typingSpeed = 0.05f; // �� ���ھ� ��µǴ� �ӵ�

    private bool isTyping = false;
    private bool isPlayerInside = false;

    private void Awake()
    {
        // �ڵ����� CanvasGroup ã�� (Inspector���� ������ ��� ���)
        if (uiCanvasGroup == null)
        {
            uiCanvasGroup = GetComponentInChildren<CanvasGroup>();
            if (uiCanvasGroup == null)
            {
                Debug.LogError("[UITextTrigger] UI CanvasGroup�� �Ҵ���� �ʾҽ��ϴ�! Inspector���� �������ּ���.");
            }
        }
    }

    private void Start()
    {
        // ���� �� UI�� �����ϰ� ����
        uiCanvasGroup.alpha = 0;
        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;
        uiTransform.localScale = Vector3.zero; // ũ�⸦ 0���� ���� (�ִϸ��̼� ȿ����)
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
        if (uiCanvasGroup == null) return; // ���� ����

        // UI�� Ư�� ���� ��ǥ�� ��ġ�ǵ��� ����
        if (uiWorldPosition != null)
        {
            uiTransform.position = uiWorldPosition.position;
            uiTransform.LookAt(Camera.main.transform);
            uiTransform.Rotate(0, 180, 0);
        }

        // UI Ȱ��ȭ �� �ִϸ��̼� ����
        uiCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutSine);
        uiTransform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);

        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;

        // Ÿ���� ȿ�� ����
        StartCoroutine(TypeText());
    }

    private void HideUI()
    {
        if (uiCanvasGroup == null) return; // ���� ����

        // UI ��Ȱ��ȭ �ִϸ��̼� ����
        uiCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.InOutSine);
        uiTransform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack);

        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;

        // �ؽ�Ʈ �ʱ�ȭ
        uiText.text = "";
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        uiText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in fullText)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
