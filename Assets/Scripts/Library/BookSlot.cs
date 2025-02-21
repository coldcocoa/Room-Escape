using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public string correctBookTag;  // 올바른 책의 태그 (예: "Book1")
    public bool isCorrectlyPlaced = false;
    public GameObject placedBook;  // 이 슬롯에 꽂힌 책

    /// <summary>
    /// 책을 슬롯에 놓을 때 호출 (예: LERP 이동 완료 후)
    /// </summary>
    public void PlaceBook(GameObject book)
    {
        placedBook = book;
        // 책의 태그가 올바르면 true, 아니면 false
        isCorrectlyPlaced = book.CompareTag(correctBookTag);
    }

    public bool IsBookPlacedCorrectly()
    {
        return isCorrectlyPlaced;
    }

    // 슬롯에 책이 꽂혀 있는지 여부 (null이 아니면 꽂힘)
    public bool IsSlotFilled()
    {
        return placedBook != null;
    }
    /// <summary>
    /// 외부에서 슬롯 상태를 직접 설정할 수 있도록 하는 함수입니다.
    /// </summary>
    public void SetBookPlaced(bool value)
    {
        isCorrectlyPlaced = value;
    }
    // 리셋 호출 시, 꽂힌 책을 원래 자리로 되돌리고 슬롯 상태 초기화
    public void ResetSlot()
    {
        if (placedBook != null)
        {
            BookReset resetComp = placedBook.GetComponent<BookReset>();
            if (resetComp != null)
            {
                // 원래 자리로 리셋 (즉시 이동하거나 코루틴으로 LERP 처리 가능)
                resetComp.ResetToOriginal();
            }
            placedBook = null;
            isCorrectlyPlaced = false;
        }
    }
}
