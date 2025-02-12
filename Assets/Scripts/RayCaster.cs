using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public float maxDistance = 100f;
    void Update()
    {
        OnClickMouse();
    }

    public void OnClickMouse()
    {
        // 좌클릭 감지 (마우스 왼쪽 버튼: 0)
        if (Input.GetMouseButtonDown(0))
        {
            // 메인 카메라에서 마우스 위치를 이용해 레이 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Physics.Raycast를 사용해 레이 충돌 체크
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // 충돌한 오브젝트의 이름 출력
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                // 필요 시 해당 오브젝트에 추가적인 처리를 수행
                // 예: hit.collider.gameObject.GetComponent<YourScript>().DoSomething();
            }
            else
            {
                // 충돌한 오브젝트가 없는 경우
                Debug.Log("No object hit.");
            }
        }
    }
}

