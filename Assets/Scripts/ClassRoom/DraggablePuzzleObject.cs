using System.Collections;
using UnityEngine;

public class DraggablePuzzleObject : MonoBehaviour
{
    [Header("��� ����")]
    public Transform correctDropZone; // ���� ��ġ�� ������ Transform
    public float snapThreshold = 1f;    // �ùٸ� ��ġ�� �ν��� �ִ� �Ÿ�

    private Vector3 originalPosition;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera cam;

    private bool isPlacedCorrectly = false; // ���� ��ġ�� ��ġ�� ����

    void Start()
    {
        originalPosition = transform.position;
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        // ���� ��尡 Ȱ��ȭ�Ǿ� �ְ� ���� ���信 ��ġ���� ���� ��쿡�� �巡�� ����
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
        // �巡�� ���� ��, ���� ��ġ���� �Ÿ��� Ȯ���Ͽ� ���� ���� ����
        if (Vector3.Distance(transform.position, correctDropZone.position) <= snapThreshold)
        {
            // �����̸� �ش� ��ġ�� ����
            transform.position = correctDropZone.position;
            isPlacedCorrectly = true;
            Debug.Log("���� ��ġ�� ��ġ��!");
            // �߰�: ���� ��ġ �� �巡�׸� ��Ȱ��ȭ�Ϸ��� ���⼭ ������ ó���� �� �� �ֽ��ϴ�.
        }
        else
        {
            // ������ �ƴϸ� ���� ��ġ�� �ε巴�� ����
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
