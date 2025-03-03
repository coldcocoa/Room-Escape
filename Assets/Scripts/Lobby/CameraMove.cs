using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera cam;
    public Transform camPosition; // ī�޶��� ��ǥ ��ġ (�ʿ� �� ���)
    public float moveSpeed = 5f;  // ī�޶� �̵� �ӵ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���� ��ü�� ������ Camera ������Ʈ�� ������
        cam = GetComponent<Camera>();

        // cam�� null���� Ȯ��
        if (cam == null)
        {
            Debug.LogWarning("Camera component not found on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ������ ������Ʈ ����
        if (cam == null) return;

        // ī�޶��� �� �������� �̵� (transform.forward�� ���� Z�� ����)
        Vector3 moveDirection = cam.transform.forward;
        cam.transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // �ʿ��ϴٸ� camPosition�� Ȱ���� Ư�� ��ġ�� �̵��ϰų� �ٸ� ���� �߰�
        if (camPosition != null)
        {
            // ��: camPosition�� �������� ���� �̵�
            // cam.transform.position = Vector3.Lerp(cam.transform.position, camPosition.position, Time.deltaTime * moveSpeed);
        }
    }
}