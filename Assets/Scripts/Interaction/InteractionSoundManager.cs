using UnityEngine;

public class InteractionSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip buttonPressSound;
    public AudioClip pickupSound;
    public AudioClip bookPickUp;
    public AudioClip wrongBook;
    public AudioClip rightBook;

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

    public void PlaybookPickUpSound()
    {
        audioSource.PlayOneShot(bookPickUp);
    }

    public void PlaybookwrongBookSound()
    {
        audioSource.PlayOneShot(wrongBook);
    }

    public void PlaybookRightBookSound()
    {
        audioSource.PlayOneShot(rightBook);
    }
}
