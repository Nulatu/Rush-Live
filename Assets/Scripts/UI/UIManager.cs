using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager In;

    [SerializeField] private GameObject pauseCanvas = default;

    public void Pause() // => CanvasGame - ButtonPause
    {
        Time.timeScale = 0; 
        pauseCanvas.SetActive(true);     
    }

    public void Unpause()  // => CanvasGame - Settings - PausePanel - ResumeButton
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    private void Awake()
    {
        In = this;
    }
}
