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
        transform.SetParent(transform.root); // Canvas �ֻ������ �̵�
        image.raycastTarget = false; // �巡�� �� Ŭ�� ����
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ���� ��, ���� �������� ��� ��� ����
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        
        // ��ȿ�� ��� ����� ������ ���� UI ��ġ�� ����
        transform.SetParent(originalParent);
        RectTransform rtReset = GetComponent<RectTransform>();
        rtReset.anchoredPosition = originalAnchoredPos;
        image.raycastTarget = true;
    }

}
