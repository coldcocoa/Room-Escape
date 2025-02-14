using UnityEngine;

[System.Serializable]
public struct DoorAnimationPair
{
    public string openAnimName;      // 예: "Door_Open"
    public AnimationClip openClip;   // 열림 애니메이션 클립
    public string closeAnimName;     // 예: "Door_Close"
    public AnimationClip closeClip;  // 닫힘 애니메이션 클립
}

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public float requiredDistance = 3f; // 상호작용 가능 거리
    public bool isOpen = false;

    // 여러 문 애니메이션 설정을 배열로 관리
    public DoorAnimationPair[] doorAnimations;

    private Animation anim;

    void Start()
    {
        // Animation 컴포넌트가 없으면 추가
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animation>();
        }
        // 등록된 모든 애니메이션 클립을 Animation 컴포넌트에 추가
        foreach (var pair in doorAnimations)
        {
            if (pair.openClip != null)
            {
                anim.AddClip(pair.openClip, pair.openAnimName);
            }
            if (pair.closeClip != null)
            {
                anim.AddClip(pair.closeClip, pair.closeAnimName);
            }
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
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
            else
            {
                Debug.Log("Too far to interact with the door.");
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        Debug.Log("Door opened!");
        // 예시: 첫 번째 문 애니메이션의 열림 애니메이션 재생
        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].openAnimName);
    }

    private void CloseDoor()
    {
        isOpen = false;
        Debug.Log("Door closed!");
        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].closeAnimName);
    }
}
