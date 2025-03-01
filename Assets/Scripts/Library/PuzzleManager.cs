using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots;      // 모든 책 슬롯 배열
    public float resetDelay = 2f;     // 틀렸을 경우 리셋하기 전 지연 시간
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
        if (!isOK) return; // 퍼즐 성공 후에는 더 이상 실행하지 않음

        if (CheckAllSlotsFilled())
        {
            if (CheckPuzzleCompletion())
            {
                soundManager.PlaybookRightBookSound();
                TimeTable2.SetActive(true); // 시간표 활성화
                isOK = false; // 이후로 Update에서 실행되지 않도록 막음
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
            timer = 0f;  // 슬롯이 모두 채워지지 않았으면 타이머 리셋
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
        if (!isOK) return true; // 퍼즐 성공 후에는 항상 true 반환
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsBookPlacedCorrectly()) return false;
        }
        return true;
    }

    private void ResetIncorrectBooks()
    {
        if (!isOK) return; // 퍼즐 성공 시 리셋 방지
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
