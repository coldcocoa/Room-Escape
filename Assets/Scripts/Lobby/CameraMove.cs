using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera cam;
    public Transform camPosition; // 카메라의 목표 위치 (필요 시 사용)
    public float moveSpeed = 5f;  // 카메라 이동 속도

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 현재 객체에 부착된 Camera 컴포넌트를 가져옴
        cam = GetComponent<Camera>();

        // cam이 null인지 확인
        if (cam == null)
        {
            Debug.LogWarning("Camera component not found on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라가 없으면 업데이트 중지
        if (cam == null) return;

        // 카메라의 앞 방향으로 이동 (transform.forward는 로컬 Z축 방향)
        Vector3 moveDirection = cam.transform.forward;
        cam.transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 필요하다면 camPosition을 활용해 특정 위치로 이동하거나 다른 로직 추가
        if (camPosition != null)
        {
            // 예: camPosition을 기준으로 보간 이동
            // cam.transform.position = Vector3.Lerp(cam.transform.position, camPosition.position, Time.deltaTime * moveSpeed);
        }
    }
}