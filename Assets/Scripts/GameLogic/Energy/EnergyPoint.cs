using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyPoint : MonoBehaviour
{
    [SerializeField] private Image imageEnergy = default;

    private float basicMaxEnergy = 10;
    private float maxEnergy = 0;
    private bool activated;
    private float time;
    private float slowRecoveryEnergy = 6; 
    private int boostAmountEnergy = 5; // After the energy update, part of the energy is restored at all energy points

    public void TryActivate() // -> EnergyButton - OnPointerClick()
    {
        if (activated)
            Deactivated();
        else
            Activated();
    }

    public void UpEnergy() // -> EnergyPointSystem - UpAllEnergy() 
    { // After pressing the update, energy is restored at all energy points on the playing field and the maximum energy increases by + 1
        maxEnergy = basicMaxEnergy + (float)UpdateData.In.Updates["ENERGY"].GetData(0);

        time = Mathf.Clamp(time + boostAmountEnergy, 0, maxEnergy);

        imageEnergy.transform.DOPunchScale(Vector3.one * 0.15f, 0.4f, vibrato: 0).OnComplete(() => imageEnergy.transform.localScale = Vector3.one);
    }

    public void DownEnergy() // -> Enemy - EatEnergy()
    {
        time = Mathf.Clamp(time - 1, 0, maxEnergy);
    }

    public void Deactivated() // -> Player - MoveEnergyPoint()
    {
        GameManager.In.PlayerObject.DisableEnergy();
        activated = false;
    }

    private void Activated()
    {
        GameManager.In.PlayerObject.EnableEnergy();
        activated = true;
    }

    private void OnEnable()
    {
        maxEnergy = basicMaxEnergy + (float)UpdateData.In.Updates["ENERGY"].GetData(0);
        time = maxEnergy;
    }

    private void Update()
    {    
        if (activated && time > 0)
        {
            time -= Time.deltaTime;
            imageEnergy.fillAmount = time / maxEnergy;
        }
        else if (activated)
            Deactivated();
        else if (time <= maxEnergy) // An equal sign is necessary, otherwise there will be a bug during the energy update of the energy recovery scale at the energy point
        {
            time += Time.deltaTime / slowRecoveryEnergy;
            imageEnergy.fillAmount = time / maxEnergy;
        }      
    }
}
