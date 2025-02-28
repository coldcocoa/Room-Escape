using UnityEngine;

[System.Serializable]
public struct DoorAnimationPair_Cafe
{
    public string openAnimName;      // 예: "Door_Open"
    public AnimationClip openClip;   // 열림 애니메이션 클립
    
}

public class OpenDoor_KeyPad : MonoBehaviour
{
    
    // 여러 문 애니메이션 설정을 배열로 관리
    public DoorAnimationPair_Cafe[] doorAnimations;

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
            
        }
    }

    

    public void OpenDoor()
    {
        
        if (doorAnimations.Length > 0)
            anim.Play(doorAnimations[0].openAnimName);
    }

    
}
