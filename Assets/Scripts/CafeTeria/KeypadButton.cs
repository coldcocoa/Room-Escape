using NavKeypad;
using UnityEngine;

public class KeypadButton : MonoBehaviour, IInteractable
{
    public string buttonInput; // ��ư�� ��Ÿ���� �� (��: "1", "2", "enter")
    public Keypad keypad;     // ����� Keypad ������Ʈ

    public void Interact()
    {
        if (keypad != null)
        {
            keypad.AddInput(buttonInput); // Keypad�� �Է� ����
        }
    }
}