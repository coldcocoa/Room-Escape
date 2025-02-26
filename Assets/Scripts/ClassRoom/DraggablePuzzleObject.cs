using System.Collections;
using UnityEngine;

public class DraggablePuzzleObject : MonoBehaviour
{
    [Header("드랍 설정")]
    public Transform correctDropZone; // 정답 위치로 지정할 Transform
    public float snapThreshold = 1f;    // 올바른 위치로 인식할 최대 거리

    private Vector3 originalPosition;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera cam;

    private bool isPlacedCorrectly = false; // 정답 위치에 배치된 상태

    void Start()
    {
        originalPosition = transform.position;
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        // 퍼즐 모드가 활성화되어 있고 아직 정답에 배치되지 않은 경우에만 드래그 시작
        if (!PuzzleModeController.instance.isPuzzleModeActive || isPlacedCorrectly)
            return;

        isDragging = true;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
        offset = transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z);
        Vector3 newPos = cam.ScreenToWorldPoint(screenPoint) + offset;
        transform.position = newPos;
    }

    void OnMouseUp()
    {
        if (!isDragging)
            return;

        isDragging = false;
        // 드래그 종료 시, 정답 위치와의 거리를 확인하여 스냅 여부 결정
        if (Vector3.Distance(transform.position, correctDropZone.position) <= snapThreshold)
        {
            // 정답이면 해당 위치에 스냅
            transform.position = correctDropZone.position;
            isPlacedCorrectly = true;
            Debug.Log("정답 위치에 배치됨!");
            // 추가: 정답 배치 후 드래그를 비활성화하려면 여기서 별도의 처리를 할 수 있습니다.
        }
        else
        {
            // 정답이 아니면 원래 위치로 부드럽게 리셋
            StartCoroutine(SmoothReset());
        }
    }

    IEnumerator SmoothReset()
    {
        float duration = 2f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
