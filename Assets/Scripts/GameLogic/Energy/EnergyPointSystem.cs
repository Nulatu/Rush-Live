using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnergyPointSystem : MonoBehaviour
{
    public static EnergyPointSystem In;

    public List<EnergyPoint> EnergyPoints;

    [SerializeField] private GameObject parentEnergyPoints = default;

    public void EnableEnergyPoints() // -> UpdateData -> SetupUpdatesSystem() after funktion LoadSave() = First, the save game is loaded, and only then the maximum energy data for points from the downloaded update saves is loaded
    {
        parentEnergyPoints.SetActive(true); // -> EnergyPoint - OnEnable(); - get UpdateData.In.Updates["Energy"]
    }

    public void UpAllEnergy() // -> UIUpdateBehavior - ChangeUpdate()
    { // After pressing the update, energy is restored at all energy points on the playing field and the maximum energy increases by + 1
        foreach (EnergyPoint energyPoint in EnergyPoints)
        {
            energyPoint.UpEnergy();
        }
    }

    public void AnimationAppearanceEnergyPoints() // -> UIManager - FinishSecondStepTutorual()
    {
        foreach (EnergyPoint energyPoint in EnergyPoints)
        {
            energyPoint.transform.DOScale(Vector3.one,1);
        }
    }
}
