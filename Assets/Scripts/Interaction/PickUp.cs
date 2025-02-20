using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject heldBook;
    public Transform holdPosition;  // 책을 들고 있을 위치 (플레이어 앞쪽)

    public float pickupRange = 5f;  // 집을 수 있는 거리
    public float bookmoveSpeed = 10f;   // 책 이동 속도

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // E 키로 책 줍기
        {
            if (heldBook == null)  // 책을 들고 있지 않을 때만 실행
            {
                TryPickUpBook();
            }
            else  // 책을 놓기
            {
                DropBook();
            }
        }

        if (heldBook != null)
        {
            MoveHeldBook();
        }
    }

    private void TryPickUpBook()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Book1"))  // 책인지 확인
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // 물리 효과 끄기
            }
            else if (hit.collider.CompareTag("Book2"))  // 책인지 확인
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // 물리 효과 끄기
            }
            else if (hit.collider.CompareTag("Book3"))  // 책인지 확인
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // 물리 효과 끄기
            }
        }
    }

    private void MoveHeldBook()
    {
        // 부드럽게 손 위치로 이동
        heldBook.transform.position = Vector3.Lerp(heldBook.transform.position, holdPosition.position, Time.deltaTime * bookmoveSpeed);
        heldBook.transform.rotation = Quaternion.Lerp(heldBook.transform.rotation, holdPosition.rotation, Time.deltaTime * bookmoveSpeed);
    }

    private void DropBook()
    {
        heldBook.GetComponent<Rigidbody>().isKinematic = false;  // 다시 물리 적용
        heldBook = null;
    }
}
