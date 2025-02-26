using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public float interactRange = 3f;             // 상호작용 감지 거리
    public LayerMask interactableLayer;          // 상호작용 대상 레이어
    public Text promptText;                      // UI 텍스트 (예: "Press E to interact")

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
        // 카메라에서 정면으로 Raycast 실행
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        bool foundInteractable = false;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // 프롬프트 표시
                if (promptText != null)
                {
                    promptText.text = "E키를 눌러 상호작용";
                    promptText.enabled = true;
                }
                foundInteractable = true;
                
            }
        }

        // 상호작용 대상이 없으면 프롬프트 숨김
        if (!foundInteractable && promptText != null)
        {
            promptText.enabled = false;
        }
    }
}
