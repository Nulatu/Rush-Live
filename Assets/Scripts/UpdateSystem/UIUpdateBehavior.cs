using UnityEngine;
using UnityEngine.UI;

public class UIUpdateBehavior : MonoBehaviour
{
    [HideInInspector] public double CurrentCostUpdate;

    [SerializeField] private Text currentLvl = default;
    [SerializeField] private Text cost = default;
    [SerializeField] private Button updateButton = default;

    private UpdateData.Update updateData;
    private SpriteState st; // protected // in order for the Unity log to not swear yellow warning about the absence of initialization of this variable

    public void ChangeUpdate() // -> OtherUpdateBehavior - OnPointerDown(),Update()
    {
        if (updateButton.GetComponent<Image>().sprite == GameManager.In.NotCoinsSpriteHudUpgrade) return;

        SoundManager.In.PlaySound(Sounds.Upgrade);
        updateData.CurrentUpdate += 1;    
        Income.In.ChangeCoins(-CurrentCostUpdate); // call CheckEnoughMoney()

        if (name.Contains("DAMAGE"))
        {
            GameManager.In.PlayerObject.UpAmountRedShot();
        }

        if (name.Contains("PROFIT"))
        {
            GameManager.In.PlayerObject.GoldEnemies();
        }

        if (name.Contains("ENERGY"))
        {
            EnergyPointSystem.In.UpAllEnergy();
        }

        UIUpdateData();
    }

    public void SetStateButton() // -> UpdateData - CheckAvailableUpdate()
    {
        if (updateData == null)
            updateData = UpdateData.In.Updates[name];

        if (updateData.CurrentUpdate < updateData.MaxUpdate && GameManager.In.Coins >= CurrentCostUpdate)
        {
            UIChangeButtonActive();
        }
        else
        {
            UIChangeButtonInActive();
        }
    }

    private void UIChangeButtonActive()
    {
        updateButton.GetComponent<Image>().sprite = GameManager.In.BasicSpriteHudUpgrade;
        st.pressedSprite = GameManager.In.BasicPressedSpriteHudUpgrade;
        updateButton.spriteState = st;
    }

    private void UIChangeButtonInActive()
    {
        updateButton.GetComponent<Image>().sprite = GameManager.In.NotCoinsSpriteHudUpgrade;
        st.pressedSprite = GameManager.In.PressedNotCoinsSpriteHudUpgrade;
        updateButton.spriteState = st;
    }

    private void Start()
    {
        updateData = UpdateData.In.Updates[name];
        UIUpdateData();
    }

    private void UIUpdateData()
    {
        CurrentCostUpdate = updateData.AllCostUpdates[updateData.CurrentUpdate];
        if (updateData.CurrentUpdate == updateData.MaxUpdate)
            cost.text = "MAX";
        else
            cost.text = NumberSystem.Output(CurrentCostUpdate);

        currentLvl.text = "Lv " + (1 + updateData.CurrentUpdate).ToString();

        CheckEnoughMoney(); // After opening the update window(OpenUpdate();), there is no check for opening buttons(updateButton.interactable)
    }

    private void CheckEnoughMoney()
    {
        SetStateButton();
    }
}
