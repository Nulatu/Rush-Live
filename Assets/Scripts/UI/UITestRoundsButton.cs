using UnityEngine;
using UnityEngine.EventSystems;

public class UITestRoundsButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) // => CanvasGame - ButtonsDevPanel - TestsForDeveloper - RoundsButton 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {           
            enemy.SendMessage("Die",false, SendMessageOptions.DontRequireReceiver);
        }

        RoundsSystem.In.RoundUp(5);
    }
}
