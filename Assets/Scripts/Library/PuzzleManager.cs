using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;      // ��� å ���� �迭
    public float resetDelay = 2f;     // Ʋ���� ��� �����ϱ� �� ���� �ð�
    public GameObject TimeTable2;
    private bool isOK = true;
    private float timer = 0f;
    public InteractionSoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<InteractionSoundManager>();
    }
    void Update()
    {
        if (!isOK) return; // ���� ���� �Ŀ��� �� �̻� �������� ����

        if (CheckAllSlotsFilled())
        {
            if (CheckPuzzleCompletion())
            {
                soundManager.PlaybookRightBookSound();
                TimeTable2.SetActive(true); // �ð�ǥ Ȱ��ȭ
                isOK = false; // ���ķ� Update���� ������� �ʵ��� ����
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= resetDelay)
                {
                    ResetIncorrectBooks();
                    timer = 0f;
                }
            }
        }
        else
        {
            timer = 0f;  // ������ ��� ä������ �ʾ����� Ÿ�̸� ����
        }
    }

    private bool CheckAllSlotsFilled()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsSlotFilled()) return false;
        }
        return true;
    }

    private bool CheckPuzzleCompletion()
    {
        if (!isOK) return true; // ���� ���� �Ŀ��� �׻� true ��ȯ
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly()) return false;
        }
        return true;
    }

    private void ResetIncorrectBooks()
    {
        if (!isOK) return; // ���� ���� �� ���� ����
        Debug.Log("���� ����: å�� ���� �ڸ��� �����մϴ�.");
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly())
            {
                slot.ResetSlot();
            }
        }
    }
}
