using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject heldBook;           // 현재 들고 있는 책
    public Transform holdPosition;         // 책을 들고 있을 위치

    [Header("Pickup Settings")]
    public float pickupRange = 5f;         // 집을 수 있는 거리
    public float bookmoveSpeed = 10f;      // 손 앞까지 이동하는 속도

    [Header("Slot Placement Settings")]
    public float slotLerpDuration = 1.0f;  // 슬롯에 책을 놓을 때 걸리는 시간(초)

    private void Start()
    {
        // 플레이어의 시점을 담당하는 메인 카메라
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 1) 아직 책을 들고 있지 않다면 → "집기 시도"
            if (heldBook == null)
            {
                TryPickUpBook();
            }
            else
            {
                // 2) 이미 책을 들고 있다면 → "슬롯에 놓기 시도" 또는 "바닥에 내려놓기"
                TryPlaceBookOrDrop();
            }
        }

        // 들고 있는 책이 있다면, 항상 holdPosition으로 부드럽게 이동
        if (heldBook != null)
        {
            MoveHeldBook();
        }
    }

    /// <summary>
    /// 화면 중앙에 레이캐스트를 쏴서 책을 감지하고, 들고 있는 상태로 만든다.
    /// </summary>
    private void TryPickUpBook()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 레이캐스트로 객체를 확인
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // Book1, Book2, Book3 태그를 체크
            if (hit.collider.CompareTag("Book1") ||
                hit.collider.CompareTag("Book2") ||
                hit.collider.CompareTag("Book3"))
            {
                heldBook = hit.collider.gameObject;
                // 책 물리효과 중지
               // heldBook.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log($"[PickUp] {heldBook.name}을(를) 집었습니다.");
            }
        }
    }

    /// <summary>
    /// 들고 있는 책을 현재 플레이어 앞(holdPosition)으로 부드럽게 이동
    /// </summary>
    private void MoveHeldBook()
    {
        if (heldBook == null) return;

        heldBook.transform.position = Vector3.Lerp(
            heldBook.transform.position,
            holdPosition.position,
            Time.deltaTime * bookmoveSpeed
        );

        heldBook.transform.rotation = Quaternion.Lerp(
            heldBook.transform.rotation,
            holdPosition.rotation,
            Time.deltaTime * bookmoveSpeed
        );
    }

    /// <summary>
    /// 들고 있는 책을 슬롯에 놓거나, 슬롯이 없으면 바닥에 내려놓기
    /// </summary>
    private void TryPlaceBookOrDrop()
    {
        // 화면 정중앙 레이캐스트 (마우스 포인터 기준)
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // 만약 BookSlot이 있는지 확인
            BookSlot slot = hit.collider.GetComponent<BookSlot>();
            if (slot != null)
            {
                // 슬롯을 찾았다면, 코루틴 실행(부드럽게 이동)
                StartCoroutine(PlaceBookToSlot(heldBook, slot));
                return;
            }
        }

        // 슬롯 못 찾으면 그냥 땅에 내려놓기
        DropBook();
    }

    /// <summary>
    /// 책을 슬롯 위치로 부드럽게 이동시키고, 이동이 끝나면 고정한다.
    /// </summary>
    private IEnumerator PlaceBookToSlot(GameObject book, BookSlot slot)
    {
        float elapsed = 0f;

        // 시작점, 도착점 정의
        Vector3 startPos = book.transform.position;
        Quaternion startRot = book.transform.rotation;

        Vector3 endPos = slot.transform.position;
        Quaternion endRot = slot.transform.rotation;

        Rigidbody bookRb = book.GetComponent<Rigidbody>();
        //bookRb.isKinematic = true; // 이동 중 물리에 간섭받지 않도록

        // LERP 이동
        while (elapsed < slotLerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slotLerpDuration;

            book.transform.position = Vector3.Lerp(startPos, endPos, t);
            book.transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            yield return null;
        }

        // 최종 위치 고정
        book.transform.position = endPos;
        book.transform.rotation = endRot;

        // 슬롯에 '정답 책'이 맞다면 BookSlot에서 isCorrectlyPlaced = true 처리
        // (BookSlot 내부 구현에 따라 달라질 수 있음)
        if (book.CompareTag(slot.correctBookTag))
        {
            //slot.SetBookPlaced(true);
            slot.PlaceBook(book);
            Debug.Log($"[Slot] {book.name}이(가) {slot.name}에 올바르게 배치되었습니다!");
        }
        else
        {
            //slot.SetBookPlaced(false);
            slot.PlaceBook(book);
            Debug.LogWarning($"[Slot] {book.name}은(는) {slot.name} 슬롯의 정답이 아닙니다!");
        }

        // 책은 이제 슬롯에 놓였으니, 플레이어는 빈손
        heldBook = null;
    }

    /// <summary>
    /// 책을 그냥 땅에 내려놓는다.
    /// </summary>
    private void DropBook()
    {
        if (heldBook == null) return;

        Rigidbody rb = heldBook.GetComponent<Rigidbody>();
        //rb.isKinematic = false;  // 물리 다시 적용

        Debug.Log($"[PickUp] {heldBook.name}을(를) 내려놓았습니다.");

        heldBook = null;         // 플레이어는 더 이상 책을 들고 있지 않음
    }
}
