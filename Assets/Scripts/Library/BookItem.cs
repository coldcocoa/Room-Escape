using UnityEngine;
using UnityEngine.UI;

public class BookItem : MonoBehaviour, IInteractable
{
    public string BookYear; // å�� ���� (��: "2024")
    public string title; // å ����

    public GameObject bookUIPanel; // UI �г� (å ȹ�� �޽���)
    public Text bookUIText; // UI�� ǥ�õ� �ؽ�Ʈ
    public GameObject bookPlacementObject; // Ȱ��ȭ�� å ������Ʈ (���̺� ���� ��ġ�Ǵ� å)

    private bool isCollected = false; // å�� �̹� ȹ���ߴ��� ����

    public InteractionSoundManager soundManager;

    public void Interact()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<InteractionSoundManager>();
        if (isCollected) return; // �̹� ȹ���� ��� ����

        isCollected = true;
        ShowBookUI(); // UI Ȱ��ȭ
        ActivatePlacedBook(); // ���ο� ��ġ�� å Ȱ��ȭ
        gameObject.SetActive(false); // ���� å ��Ȱ��ȭ
    }

    private void ShowBookUI()
    {
        if (bookUIPanel != null && bookUIText != null)
        {
            soundManager.PlaybookPickUpSound();
            bookUIPanel.SetActive(true);
            bookUIText.text = $"å�� ȹ���߽��ϴ�!\n\n����: {title}\n���ǳ⵵: {BookYear}"; // �ؽ�Ʈ ����
            Invoke("HideBookUI", 4f); // 2�� �� UI �ڵ� ����
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
            bookPlacementObject.SetActive(true); // ���ο� ��ġ�� å Ȱ��ȭ
        }
    }
}
