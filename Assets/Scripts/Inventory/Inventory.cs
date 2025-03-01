using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Inventory Slots")]
    public Image[] slots; // UI ���� 4��
    public Sprite emptySlotSprite;
    private Sprite[] timetablePieces = new Sprite[4];
    public int currentSlotCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (Image slot in slots)
        {
            slot.sprite = emptySlotSprite;
        }
        //gameObject.SetActive(false); // �⺻������ ��Ȱ��ȭ
    }

    public bool AddItem(Sprite pieceSprite)
    {
        if (currentSlotCount < 4)
        {
            timetablePieces[currentSlotCount] = pieceSprite;
            slots[currentSlotCount].sprite = pieceSprite;
            currentSlotCount++;
            CheckPuzzleReady();
            return true;
        }
        return false;
    }

    void CheckPuzzleReady()
    {
        if (currentSlotCount == 4)
        {
            Debug.Log("All timetable pieces collected!");
        }
    }

    public void ShowInventory(bool show)
    {
        gameObject.SetActive(show);
    }

    public Sprite GetPiece(int index)
    {
        return index >= 0 && index < 4 ? timetablePieces[index] : null;
    }

    public void RemovePiece(int index) // ���� ���� ���� ����
    {
        if (index >= 0 && index < 4)
        {
            timetablePieces[index] = null;
            slots[index].sprite = emptySlotSprite;
        }
    }
}