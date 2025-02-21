using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;      // ��� å ���� �迭
    public float resetDelay = 2f;     // Ʋ���� ��� �����ϱ� �� ���� �ð�

    private float timer = 0f;

    void Update()
    {
        // ��� ������ ä�������� ���� Ȯ��
        if (CheckAllSlotsFilled())
        {
            if (CheckPuzzleCompletion())
            {
                Debug.Log("���� �Ϸ�! å�� �ùٸ��� ��ġ�Ǿ����ϴ�.");
                // ���⼭ �߰� �̺�Ʈ (��: �� ���� ��) ����
            }
            else
            {
                // ��� ������ ä�������� ������ Ʋ�ȴٸ�, ���� �� ����
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

    // ��� ������ ä�������� Ȯ��
    private bool CheckAllSlotsFilled()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsSlotFilled())
                return false;
        }
        return true;
    }

    // ��� ������ �ùٸ� å���� �������� Ȯ��
    private bool CheckPuzzleCompletion()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly())
            {
                return false;
            }
        }
        return true;
    }

    // ������ Ʋ�� ������ å�� ���� �ڸ��� �ǵ���
    private void ResetIncorrectBooks()
    {
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
