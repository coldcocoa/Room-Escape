using UnityEngine;

public class CanvasOpen : MonoBehaviour
{
   
    public GameObject canvas;
    public GameObject canvas_Start;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtn()
    {
        
        canvas.SetActive(false);
        canvas_Start.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
