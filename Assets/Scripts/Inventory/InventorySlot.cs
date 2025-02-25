using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Transform originalParent;

    private Vector2 originalAnchoredPos;

    void Awake()
    {
        image = GetComponent<Image>();
        originalAnchoredPos = GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // Canvas 최상단으로 이동
        image.raycastTarget = false; // 드래그 중 클릭 방지
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시, 월드 공간에서 드롭 대상 감지
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        
        // 유효한 드롭 대상이 없으면 원래 UI 위치로 복귀
        transform.SetParent(originalParent);
        RectTransform rtReset = GetComponent<RectTransform>();
        rtReset.anchoredPosition = originalAnchoredPos;
        image.raycastTarget = true;
    }

}
