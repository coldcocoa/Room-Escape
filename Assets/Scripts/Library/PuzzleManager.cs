using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;      // 모든 책 슬롯 배열
    public float resetDelay = 2f;     // 틀렸을 경우 리셋하기 전 지연 시간

    private float timer = 0f;

    void Update()
    {
        // 모든 슬롯이 채워졌는지 먼저 확인
        if (CheckAllSlotsFilled())
        {
            if (CheckPuzzleCompletion())
            {
                Debug.Log("퍼즐 완료! 책이 올바르게 배치되었습니다.");
                // 여기서 추가 이벤트 (예: 문 열기 등) 실행
            }
            else
            {
                // 모든 슬롯이 채워졌지만 퍼즐이 틀렸다면, 지연 후 리셋
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
            timer = 0f;  // 슬롯이 모두 채워지지 않았으면 타이머 리셋
        }
    }

    // 모든 슬롯이 채워졌는지 확인
    private bool CheckAllSlotsFilled()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsSlotFilled())
                return false;
        }
        return true;
    }

    // 모든 슬롯이 올바른 책으로 꽂혔는지 확인
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

    // 퍼즐이 틀린 슬롯의 책을 원래 자리로 되돌림
    private void ResetIncorrectBooks()
    {
        Debug.Log("퍼즐 실패: 책을 원래 자리로 리셋합니다.");
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly())
            {
                slot.ResetSlot();
            }
        }
    }
}
