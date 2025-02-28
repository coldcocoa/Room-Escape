using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public Transform playerTransform;
    private Vector3 lastPosition;
    public float stepInterval = 0.5f;
    private float nextStepTime = 0f;

    void Start()
    {
        lastPosition = playerTransform.position;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);

        if (distanceMoved > 0.05f) // 이동 거리 감지
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
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}
