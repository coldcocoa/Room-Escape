using UnityEngine;

[System.Serializable]
public struct DoorAnimationPair_Cafe
{
    public string openAnimName;      // ��: "Door_Open"
    public AnimationClip openClip;   // ���� �ִϸ��̼� Ŭ��
    
}

public class OpenDoor_KeyPad : MonoBehaviour
{
    
    // ���� �� �ִϸ��̼� ������ �迭�� ����
    public DoorAnimationPair_Cafe[] doorAnimations;

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
            
        }
    }

    

    public void OpenDoor()
    {
        
        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].openAnimName);
    }

    
}
