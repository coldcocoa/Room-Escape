using UnityEngine;

public class TimetablePiece : MonoBehaviour, IInteractable
{
    public Sprite pieceSprite; 
    public InteractionSoundManager soundManager;

    public void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<InteractionSoundManager>();
    }
    public void Interact()
    {
        if (Inventory.Instance.AddItem(pieceSprite))
        {
            soundManager.PlayPickupSound();
            gameObject.SetActive(false); // ȹ�� �� ��Ȱ��ȭ
            Debug.Log("Timetable piece collected!");
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
}