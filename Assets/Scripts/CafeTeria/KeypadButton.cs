using NavKeypad;
using UnityEngine;

public class KeypadButton : MonoBehaviour, IInteractable
{
    public string buttonInput; // 버튼이 나타내는 값 (예: "1", "2", "enter")
    public Keypad keypad;     // 연결된 Keypad 오브젝트

    public void Interact()
    {
        if (keypad != null)
        {
            keypad.AddInput(buttonInput); // Keypad에 입력 전달
        }
    }
}