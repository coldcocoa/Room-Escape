using UnityEngine;

public class TimetablePiece : MonoBehaviour, IInteractable
{
    public Sprite pieceSprite; 

    public void Interact()
    {
        if (Inventory.Instance.AddItem(pieceSprite))
        {
            gameObject.SetActive(false); // È¹µæ ÈÄ ºñÈ°¼ºÈ­
            Debug.Log("Timetable piece collected!");
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
}