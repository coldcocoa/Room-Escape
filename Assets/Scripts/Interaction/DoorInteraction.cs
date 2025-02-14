using UnityEngine;

[System.Serializable]
public struct DoorAnimationPair
{
    public string openAnimName;      // ��: "Door_Open"
    public AnimationClip openClip;   // ���� �ִϸ��̼� Ŭ��
    public string closeAnimName;     // ��: "Door_Close"
    public AnimationClip closeClip;  // ���� �ִϸ��̼� Ŭ��
}

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public float requiredDistance = 3f; // ��ȣ�ۿ� ���� �Ÿ�
    public bool isOpen = false;

    // ���� �� �ִϸ��̼� ������ �迭�� ����
    public DoorAnimationPair[] doorAnimations;

    private Animation anim;

    void Start()
    {
        // Animation ������Ʈ�� ������ �߰�
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animation>();
        }
        // ��ϵ� ��� �ִϸ��̼� Ŭ���� Animation ������Ʈ�� �߰�
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
        // ����: ù ��° �� �ִϸ��̼��� ���� �ִϸ��̼� ���
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
