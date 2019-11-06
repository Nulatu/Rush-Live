using UnityEngine;
using UnityEngine.EventSystems;

public class EnergyButton : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale <= 0.1f) // RoundSystem.In.CurrentRound == 0 // OneStepTutorial // Better such a condition check than through Singolton. Faster.
        {
            Tutorial.In.FinishFirstStepTutorual();
        }

        GameManager.In.PlayerObject.GetEnergyPoint().TryActivate();
    }
}
