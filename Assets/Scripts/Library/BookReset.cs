using UnityEngine;
using System.Collections;

public class BookReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float resetSpeed = 2f;  // LERP �ӵ�

    private void Start()
    {
        // ���� ���� ��, ���� ��ġ�� ȸ���� ����
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetToOriginal()
    {
        // LERP �ڷ�ƾ�� �����Ͽ� ���� �ڸ��� �ε巴�� �̵�
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float duration = 1f; // ���¿� �ɸ��� �ð�

        // ���� �ð����� LERP�� �̵�
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime * resetSpeed;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            transform.rotation = Quaternion.Lerp(startRot, originalRotation, t);
            yield return null;
        }
        // ������ ���� ��ġ�� ����
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // �ʿ��� ���, Rigidbody ���� ȿ�� ��Ȱ��ȭ �� �߰� ó�� ����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
