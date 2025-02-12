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
        // ��Ŭ�� ���� (���콺 ���� ��ư: 0)
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ī�޶󿡼� ���콺 ��ġ�� �̿��� ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Physics.Raycast�� ����� ���� �浹 üũ
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // �浹�� ������Ʈ�� �̸� ���
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                // �ʿ� �� �ش� ������Ʈ�� �߰����� ó���� ����
                // ��: hit.collider.gameObject.GetComponent<YourScript>().DoSomething();
            }
            else
            {
                // �浹�� ������Ʈ�� ���� ���
                Debug.Log("No object hit.");
            }
        }
    }
}

