using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public float interactRange = 3f;             // ��ȣ�ۿ� ���� �Ÿ�
    public LayerMask interactableLayer;          // ��ȣ�ۿ� ��� ���̾�
    public Text promptText;                      // UI �ؽ�Ʈ (��: "Press E to interact")

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (promptText != null)
        {
            promptText.enabled = false;
        }
    }

    void Update()
    {
        // ī�޶󿡼� �������� Raycast ����
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        bool foundInteractable = false;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // ������Ʈ ǥ��
                if (promptText != null)
                {
                    promptText.text = "EŰ�� ���� ��ȣ�ۿ�";
                    promptText.enabled = true;
                }
                foundInteractable = true;
                
            }
        }

        // ��ȣ�ۿ� ����� ������ ������Ʈ ����
        if (!foundInteractable && promptText != null)
        {
            promptText.enabled = false;
        }
    }
}
