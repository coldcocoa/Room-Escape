using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public string correctBookTag;  // 올바른 책의 태그 (예: "Book1", "Book2", "Book3")
    private bool isCorrectlyPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctBookTag))  // 맞는 책이 들어왔는지 확인
        {
            isCorrectlyPlaced = true;
            other.gameObject.transform.position = transform.position;  // 정확한 위치로 고정
            other.gameObject.transform.rotation = transform.rotation;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;  // 물리 적용 해제

            Debug.Log(correctBookTag + "이 올바르게 배치됨!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(correctBookTag))  // 책이 나가면 다시 상태 초기화
        {
            isCorrectlyPlaced = false;
            Debug.Log(correctBookTag + "이 슬롯에서 벗어남!");
        }
    }

    public bool IsBookPlacedCorrectly()
    {
        return isCorrectlyPlaced;
    }
}
