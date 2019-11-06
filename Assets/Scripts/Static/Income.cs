using UnityEngine;
using UnityEngine.UI;

public class Income : MonoBehaviour
{
    public static Income In;

    public Text TextCoins;

    public void ChangeCoins(double coins) // Many links to other scripts. This public function is called from everywhere.
    {
        GameManager.In.Coins += coins;
        OutputProcessing();
        SaveManager.In.Save();
    }

    public void OutputProcessing() // -> SaveManager - LoadSave()
    {
        double coins = GameManager.In.Coins;
        TextCoins.text = NumberSystem.OutputTwoNumbers(coins);
        UpdateData.In.CheckAvailableUpdate();
    }
}
