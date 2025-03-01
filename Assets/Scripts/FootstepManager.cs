using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepSounds;
    public Transform playerTransform;
    private CharacterController characterController; // 이동 방식에 따라 적절히 변경
    private Vector3 lastPosition;
    public float stepInterval = 0.5f;
    private float nextStepTime = 0f;
    private bool isGrounded = true; // 캐릭터가 땅에 있는지 확인

    void Start()
    {
        lastPosition = playerTransform.position;
        characterController = playerTransform.GetComponent<CharacterController>();
        if (audioSource == null || footstepSounds == null)
        {
            Debug.LogWarning("AudioSource 또는 FootstepSounds가 설정되지 않았습니다!");
        }
    }

    void Update()
    {
        // 캐릭터가 땅에 있는지 확인 (필요 시 다른 방식으로 체크)
        isGrounded = characterController != null ? characterController.isGrounded : true;

        // 수평 이동 거리만 계산 (xz 평면)
        Vector3 currentPos = playerTransform.position;
        Vector3 lastPos = lastPosition;
        currentPos.y = 0; // 수직 이동 제외
        lastPos.y = 0;
        float distanceMoved = Vector3.Distance(currentPos, lastPos);

        // 이동 중이고 땅에 있으면 발소리 재생
        if (isGrounded && distanceMoved > 0.01f) // 조건 완화
        {
            if (Time.time >= nextStepTime)
            {
                PlayFootstep();
                nextStepTime = Time.time + stepInterval;
            }
        }

        lastPosition = playerTransform.position;
    }

    void PlayFootstep()
    {
        if (audioSource != null && footstepSounds != null)
        {
            audioSource.PlayOneShot(footstepSounds);
            Debug.Log("발소리 재생 중...");
        }
        else
        {
            Debug.LogWarning("발소리 재생 실패: AudioSource 또는 AudioClip이 설정되지 않음");
        }
    }
}