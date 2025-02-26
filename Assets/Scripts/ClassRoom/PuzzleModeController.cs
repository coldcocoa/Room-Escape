using UnityEngine;

public class PuzzleModeController : MonoBehaviour
{
    public static PuzzleModeController instance;

    [Header("퍼즐 오브젝트")]
    public GameObject puzzleObjects; // 시간표 조각들이 포함된 부모 오브젝트 (기본적으로 비활성화)

    public bool isPuzzleModeActive { get; private set; } = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // 특정 영역 내에서 E키를 눌렀을 때 퍼즐 모드 토글 (추가 조건이 필요하면 여기서 처리)
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
                Debug.Log("인벤토리가 모두 채워지지 않았습니다.");
            }
        }
    }

    public void ActivatePuzzleMode()
    {
        isPuzzleModeActive = true;
        if (puzzleObjects != null)
            puzzleObjects.SetActive(true); // 퍼즐 오브젝트 활성화

        // 플레이어 이동 제어는 플레이어 스크립트에서 처리하므로 여기서는 커서만 활성화
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
