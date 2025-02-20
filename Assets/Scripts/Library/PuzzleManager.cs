using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;  // 모든 책 슬롯을 배열로 관리

    private void Update()
    {
        if (CheckPuzzleCompletion())
        {
            Debug.Log("퍼즐 완료! 책이 올바르게 배치되었습니다.");
            // 여기에 문이 열리는 등의 추가 이벤트 실행 가능
        }
    }

    private bool CheckPuzzleCompletion()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly())  // 하나라도 틀리면 실패
            {
                return false;
            }
        }
        return true;
    }
}
