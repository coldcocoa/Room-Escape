using UnityEngine;

public class PuzzleModeController : MonoBehaviour
{
    public static PuzzleModeController instance;

    [Header("���� ������Ʈ")]
    public GameObject puzzleObjects; // �ð�ǥ �������� ���Ե� �θ� ������Ʈ (�⺻������ ��Ȱ��ȭ)

    public bool isPuzzleModeActive { get; private set; } = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Ư�� ���� ������ EŰ�� ������ �� ���� ��� ��� (�߰� ������ �ʿ��ϸ� ���⼭ ó��)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory.Instance != null && Inventory.Instance.currentSlotCount == 4)
            {
                if (!isPuzzleModeActive)
                    ActivatePuzzleMode();
                else
                    DeactivatePuzzleMode();
            }
            else
            {
                Debug.Log("�κ��丮�� ��� ä������ �ʾҽ��ϴ�.");
            }
        }
    }

    public void ActivatePuzzleMode()
    {
        isPuzzleModeActive = true;
        if (puzzleObjects != null)
            puzzleObjects.SetActive(true); // ���� ������Ʈ Ȱ��ȭ

        // �÷��̾� �̵� ����� �÷��̾� ��ũ��Ʈ���� ó���ϹǷ� ���⼭�� Ŀ���� Ȱ��ȭ
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivatePuzzleMode()
    {
        isPuzzleModeActive = false;
        if (puzzleObjects != null)
            puzzleObjects.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
