using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public string correctBookTag;  // �ùٸ� å�� �±� (��: "Book1", "Book2", "Book3")
    private bool isCorrectlyPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctBookTag))  // �´� å�� ���Դ��� Ȯ��
        {
            isCorrectlyPlaced = true;
            other.gameObject.transform.position = transform.position;  // ��Ȯ�� ��ġ�� ����
            other.gameObject.transform.rotation = transform.rotation;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;  // ���� ���� ����

            Debug.Log(correctBookTag + "�� �ùٸ��� ��ġ��!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctBookTag))  // å�� ������ �ٽ� ���� �ʱ�ȭ
        {
            isCorrectlyPlaced = false;
            Debug.Log(correctBookTag + "�� ���Կ��� ���!");
        }
    }

    public bool IsBookPlacedCorrectly()
    {
        return isCorrectlyPlaced;
    }
}
