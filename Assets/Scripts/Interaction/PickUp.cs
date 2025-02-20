using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject heldBook;
    public Transform holdPosition;  // å�� ��� ���� ��ġ (�÷��̾� ����)

    public float pickupRange = 5f;  // ���� �� �ִ� �Ÿ�
    public float bookmoveSpeed = 10f;   // å �̵� �ӵ�

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // E Ű�� å �ݱ�
        {
            if (heldBook == null)  // å�� ��� ���� ���� ���� ����
            {
                TryPickUpBook();
            }
            else  // å�� ����
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
            if (hit.collider.CompareTag("Book1"))  // å���� Ȯ��
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // ���� ȿ�� ����
            }
            else if (hit.collider.CompareTag("Book2"))  // å���� Ȯ��
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // ���� ȿ�� ����
            }
            else if (hit.collider.CompareTag("Book3"))  // å���� Ȯ��
            {
                heldBook = hit.collider.gameObject;
                heldBook.GetComponent<Rigidbody>().isKinematic = true;  // ���� ȿ�� ����
            }
        }
    }

    private void MoveHeldBook()
    {
        // �ε巴�� �� ��ġ�� �̵�
        heldBook.transform.position = Vector3.Lerp(heldBook.transform.position, holdPosition.position, Time.deltaTime * bookmoveSpeed);
        heldBook.transform.rotation = Quaternion.Lerp(heldBook.transform.rotation, holdPosition.rotation, Time.deltaTime * bookmoveSpeed);
    }

    private void DropBook()
    {
        heldBook.GetComponent<Rigidbody>().isKinematic = false;  // �ٽ� ���� ����
        heldBook = null;
    }
}
