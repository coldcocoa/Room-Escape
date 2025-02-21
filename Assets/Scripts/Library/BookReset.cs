using UnityEngine;
using System.Collections;

public class BookReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float resetSpeed = 2f;  // LERP 속도

    private void Start()
    {
        // 게임 시작 시, 원래 위치와 회전값 저장
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetToOriginal()
    {
        // LERP 코루틴을 실행하여 원래 자리로 부드럽게 이동
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float duration = 1f; // 리셋에 걸리는 시간

        // 지정 시간동안 LERP로 이동
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime * resetSpeed;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            transform.rotation = Quaternion.Lerp(startRot, originalRotation, t);
            yield return null;
        }
        // 완전히 원래 위치로 설정
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // 필요한 경우, Rigidbody 물리 효과 재활성화 등 추가 처리 가능
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
