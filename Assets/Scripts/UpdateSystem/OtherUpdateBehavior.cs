using UnityEngine;
using UnityEngine.EventSystems;

public class OtherUpdateBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool Pressed;
    [HideInInspector] public float Timer;

    private float timeForClickNextUpdate = 0.25f;

    public void OnPointerDown(PointerEventData eventData) // => CanvasGame - UpdateButtons - AllButtons
    {
        Pressed = true;
        GetComponent<UIUpdateBehavior>().ChangeUpdate();
    }

    public void OnPointerUp(PointerEventData eventData)  // => CanvasGame - UpdateButtons - AllButtons
    {
        Pressed = false;
        Timer = 0; // FIX // so that the variable "time" does not increase once again
    }

    private void Update()
    {
        if (Pressed)
        {
            Timer += Time.deltaTime;
            if (Timer >= timeForClickNextUpdate)
            {
                GetComponent<UIUpdateBehavior>().ChangeUpdate();
                Timer = 0;
            }
        }
    }
}
