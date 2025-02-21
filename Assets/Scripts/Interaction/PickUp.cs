using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject heldBook;           // ���� ��� �ִ� å
    public Transform holdPosition;         // å�� ��� ���� ��ġ

    [Header("Pickup Settings")]
    public float pickupRange = 5f;         // ���� �� �ִ� �Ÿ�
    public float bookmoveSpeed = 10f;      // �� �ձ��� �̵��ϴ� �ӵ�

    [Header("Slot Placement Settings")]
    public float slotLerpDuration = 1.0f;  // ���Կ� å�� ���� �� �ɸ��� �ð�(��)

    private void Start()
    {
        // �÷��̾��� ������ ����ϴ� ���� ī�޶�
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 1) ���� å�� ��� ���� �ʴٸ� �� "���� �õ�"
            if (heldBook == null)
            {
                TryPickUpBook();
            }
            else
            {
                // 2) �̹� å�� ��� �ִٸ� �� "���Կ� ���� �õ�" �Ǵ� "�ٴڿ� ��������"
                TryPlaceBookOrDrop();
            }
        }

        // ��� �ִ� å�� �ִٸ�, �׻� holdPosition���� �ε巴�� �̵�
        if (heldBook != null)
        {
            MoveHeldBook();
        }
    }

    /// <summary>
    /// ȭ�� �߾ӿ� ����ĳ��Ʈ�� ���� å�� �����ϰ�, ��� �ִ� ���·� �����.
    /// </summary>
    private void TryPickUpBook()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ����ĳ��Ʈ�� ��ü�� Ȯ��
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // Book1, Book2, Book3 �±׸� üũ
            if (hit.collider.CompareTag("Book1") ||
                hit.collider.CompareTag("Book2") ||
                hit.collider.CompareTag("Book3"))
            {
                heldBook = hit.collider.gameObject;
                // å ����ȿ�� ����
               // heldBook.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log($"[PickUp] {heldBook.name}��(��) �������ϴ�.");
            }
        }
    }

    /// <summary>
    /// ��� �ִ� å�� ���� �÷��̾� ��(holdPosition)���� �ε巴�� �̵�
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
    /// ��� �ִ� å�� ���Կ� ���ų�, ������ ������ �ٴڿ� ��������
    /// </summary>
    private void TryPlaceBookOrDrop()
    {
        // ȭ�� ���߾� ����ĳ��Ʈ (���콺 ������ ����)
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // ���� BookSlot�� �ִ��� Ȯ��
            BookSlot slot = hit.collider.GetComponent<BookSlot>();
            if (slot != null)
            {
                // ������ ã�Ҵٸ�, �ڷ�ƾ ����(�ε巴�� �̵�)
                StartCoroutine(PlaceBookToSlot(heldBook, slot));
                return;
            }
        }

        // ���� �� ã���� �׳� ���� ��������
        DropBook();
    }

    /// <summary>
    /// å�� ���� ��ġ�� �ε巴�� �̵���Ű��, �̵��� ������ �����Ѵ�.
    /// </summary>
    private IEnumerator PlaceBookToSlot(GameObject book, BookSlot slot)
    {
        float elapsed = 0f;

        // ������, ������ ����
        Vector3 startPos = book.transform.position;
        Quaternion startRot = book.transform.rotation;

        Vector3 endPos = slot.transform.position;
        Quaternion endRot = slot.transform.rotation;

        Rigidbody bookRb = book.GetComponent<Rigidbody>();
        //bookRb.isKinematic = true; // �̵� �� ������ �������� �ʵ���

        // LERP �̵�
        while (elapsed < slotLerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slotLerpDuration;

            book.transform.position = Vector3.Lerp(startPos, endPos, t);
            book.transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            yield return null;
        }

        // ���� ��ġ ����
        book.transform.position = endPos;
        book.transform.rotation = endRot;

        // ���Կ� '���� å'�� �´ٸ� BookSlot���� isCorrectlyPlaced = true ó��
        // (BookSlot ���� ������ ���� �޶��� �� ����)
        if (book.CompareTag(slot.correctBookTag))
        {
            //slot.SetBookPlaced(true);
            slot.PlaceBook(book);
            Debug.Log($"[Slot] {book.name}��(��) {slot.name}�� �ùٸ��� ��ġ�Ǿ����ϴ�!");
        }
        else
        {
            //slot.SetBookPlaced(false);
            slot.PlaceBook(book);
            Debug.LogWarning($"[Slot] {book.name}��(��) {slot.name} ������ ������ �ƴմϴ�!");
        }

        // å�� ���� ���Կ� ��������, �÷��̾�� ���
        heldBook = null;
    }

    /// <summary>
    /// å�� �׳� ���� �������´�.
    /// </summary>
    private void DropBook()
    {
        if (heldBook == null) return;

        Rigidbody rb = heldBook.GetComponent<Rigidbody>();
        //rb.isKinematic = false;  // ���� �ٽ� ����

        Debug.Log($"[PickUp] {heldBook.name}��(��) �������ҽ��ϴ�.");

        heldBook = null;         // �÷��̾�� �� �̻� å�� ��� ���� ����
    }
}
