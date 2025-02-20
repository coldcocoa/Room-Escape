using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;  // ��� å ������ �迭�� ����

    private void Update()
    {
        if (CheckPuzzleCompletion())
        {
            Debug.Log("���� �Ϸ�! å�� �ùٸ��� ��ġ�Ǿ����ϴ�.");
            // ���⿡ ���� ������ ���� �߰� �̺�Ʈ ���� ����
        }
    }

    private bool CheckPuzzleCompletion()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly())  // �ϳ��� Ʋ���� ����
            {
                return false;
            }
        }
        return true;
    }
}
