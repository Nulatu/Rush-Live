using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager In;

    public Player PlayerObject;
    public GameObject PanelOfflineEarnings;
    public double Coins;
    public double OfflineEarningsOneSecond;
    public MoveToCoinsTopPanel RewardPanel;
    public Sprite BasicSpriteHudUpgrade;
    public Sprite BasicPressedSpriteHudUpgrade;
    public Sprite NotCoinsSpriteHudUpgrade;
    public Sprite PressedNotCoinsSpriteHudUpgrade;  
    public Income IncomeScript;
    public RoundsSystem RoundSystemScript;
    public Transform AnimationPrefabGetCoins;
    public EnergyPointSystem EnergyPointSystem;

    public void ExitGame() // => CanvasGame - ButtonsDevPanel - TestsForDeveloper - ExitButton - OnClick()
    {
        Application.Quit();
    }

    private void Awake()
    {
        In = this;
        UpdateData.In = GetComponent<UpdateData>(); // Sometimes Awake are called in different sequences. So that static objects never had nulls, we initialize everything at one point.
        SaveManager.In = GetComponent<SaveManager>(); // Sometimes Awake are called in different sequences. So that static objects never had nulls, we initialize everything at one point.
        Income.In = IncomeScript; // Sometimes Awake are called in different sequences. So that static objects never had nulls, we initialize everything at one point.
        EnergyPointSystem.In = EnergyPointSystem;
        RoundsSystem.In = RoundSystemScript;
        UpdateData.In.SetupUpdatesSystem(); 
    }
}
