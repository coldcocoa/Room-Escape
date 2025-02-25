using UnityEngine;

public class TimetablePiece : MonoBehaviour, IInteractable
{
    public Sprite pieceSprite; 

    public void Interact()
    {
        if (Inventory.Instance.AddItem(pieceSprite))
        {
            gameObject.SetActive(false); // ȹ�� �� ��Ȱ��ȭ
            Debug.Log("Timetable piece collected!");
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
}