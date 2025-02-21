using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public string correctBookTag;  // �ùٸ� å�� �±� (��: "Book1")
    public bool isCorrectlyPlaced = false;
    public GameObject placedBook;  // �� ���Կ� ���� å

    /// <summary>
    /// å�� ���Կ� ���� �� ȣ�� (��: LERP �̵� �Ϸ� ��)
    /// </summary>
    public void PlaceBook(GameObject book)
    {
        placedBook = book;
        // å�� �±װ� �ùٸ��� true, �ƴϸ� false
        isCorrectlyPlaced = book.CompareTag(correctBookTag);
    }

    public bool IsBookPlacedCorrectly()
    {
        return isCorrectlyPlaced;
    }

    // ���Կ� å�� ���� �ִ��� ���� (null�� �ƴϸ� ����)
    public bool IsSlotFilled()
    {
        return placedBook != null;
    }
    /// <summary>
    /// �ܺο��� ���� ���¸� ���� ������ �� �ֵ��� �ϴ� �Լ��Դϴ�.
    /// </summary>
    public void SetBookPlaced(bool value)
    {
        isCorrectlyPlaced = value;
    }
    // ���� ȣ�� ��, ���� å�� ���� �ڸ��� �ǵ����� ���� ���� �ʱ�ȭ
    public void ResetSlot()
    {
        if (placedBook != null)
        {
            BookReset resetComp = placedBook.GetComponent<BookReset>();
            if (resetComp != null)
            {
                // ���� �ڸ��� ���� (��� �̵��ϰų� �ڷ�ƾ���� LERP ó�� ����)
                resetComp.ResetToOriginal();
            }
            placedBook = null;
            isCorrectlyPlaced = false;
        }
    }
}
