using UnityEngine;
using UnityEngine.UI;

public class BookInteraction : MonoBehaviour, IInteractable
{
    [Header("애니메이션 관련")]
    public float requiredDistance = 3f; // 상호작용 가능 거리
    public bool isOpen = false; // 책의 열린 상태

    private Animator animator;

    void Start()
    {
        // 📌 `book01_2`의 Animator 컴포넌트를 가져옴
        animator = transform.Find("book01_2").GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator가 book01_2에 없습니다! Animator를 추가하세요.");
        }
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= requiredDistance)
            {
                if (!isOpen)
                {
                    OpenBook();
                }
                else
                {
                    CloseBook();
                }
            }
            else
            {
                Debug.Log("1");
            }
        }
    }

    private void OpenBook()
    {
        isOpen = true;

        // 📌 Animator에서 `isOpen` 파라미터를 true로 설정하여 열기 애니메이션 실행
        if (animator != null)
        {
            animator.SetBool("isOpen", true);
        }
    }

    private void CloseBook()
    {
        isOpen = false;

        // 📌 Animator에서 `isOpen` 파라미터를 false로 설정하여 닫기 애니메이션 실행
        if (animator != null)
        {
            animator.SetBool("isOpen", false);
        }
    }
}
