using UnityEngine;
using UnityEngine.UI;

public class UIGetBonusCoins : MonoBehaviour
{
    [HideInInspector] public double FreeCoins;
    [HideInInspector] public double AdsCoins;

    public Text FreeButton;
    public Text AdsButton;

    public void GetBonusCoins(Button button) // => PanelOfflineEarnings - FreeButton,AdsButton
    {
        if (button.tag == "Simple")
        {
            GameManager.In.RewardPanel.OpenRewardPanel(FreeCoins); // --> Income.I.ChangeCoins(freeCoins);
        }
        else if (button.tag == "Ads")
        {
            Income.In.ChangeCoins(AdsCoins);
        }

        gameObject.SetActive(false);      
    }
}
