using UnityEngine;

public class DoorSound : MonoBehaviour
{
    [Header("Door Sounds")]
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenSound()
    {
        if (doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);
    }

    public void PlayDoorCloseSound()
    {
        if (doorCloseSound != null)
            audioSource.PlayOneShot(doorCloseSound);
    }
}
