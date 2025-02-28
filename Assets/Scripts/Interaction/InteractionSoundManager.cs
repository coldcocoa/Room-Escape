using UnityEngine;

public class InteractionSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip buttonPressSound;
    public AudioClip pickupSound;

    public void PlayDoorOpenSound()
    {
        audioSource.PlayOneShot(doorOpenSound);
    }

    public void PlayButtonPressSound()
    {
        audioSource.PlayOneShot(buttonPressSound);
    }

    public void PlayPickupSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }
}
