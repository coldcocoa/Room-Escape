using UnityEngine;

[System.Serializable]
public struct DoorAnimationPair
{
    public string openAnimName;
    public AnimationClip openClip;
    public string closeAnimName;
    public AnimationClip closeClip;
}

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public float requiredDistance = 3f;
    public bool isOpen = false;
    public InteractionSoundManager soundManager; // 공용 사운드 매니저 (사용 안 함)
    public DoorSound doorSound; // 문별 개별 사운드 시스템 추가
    public DoorAnimationPair[] doorAnimations;
    private Animation anim;

    void Start()
    {
        // 문별 사운드 컴포넌트 가져오기
        doorSound = GetComponent<DoorSound>();

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

        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].openAnimName);

        if (doorSound != null)
            doorSound.PlayDoorOpenSound(); // 문별 열리는 소리 재생
    }

    private void CloseDoor()
    {
        isOpen = false;
        Debug.Log("Door closed!");

        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].closeAnimName);

        if (doorSound != null)
            doorSound.PlayDoorCloseSound(); // 문별 닫히는 소리 재생
    }
}
