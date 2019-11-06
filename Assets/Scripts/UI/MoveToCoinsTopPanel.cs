using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveToCoinsTopPanel : MonoBehaviour
{
    [SerializeField] private Transform pointFly = default;
    [SerializeField] private Transform startFlyPoint = default;
    [SerializeField] private GameObject imageCoinIncrease = default;
    [SerializeField] private Image panelReward = default;
    [SerializeField] private Button buttonClosePanelReward = default;
    [SerializeField] private Text bottomText = default;
    [SerializeField] private GameObject parentForFly = default;

    private double rewardCoins;

    public void OpenRewardPanel(double rewardCoins) // -> UIGetBonusCoins - GetBonusCoins()
    {
        this.rewardCoins = rewardCoins;
        bottomText.text = NumberSystem.Output(rewardCoins);
        buttonClosePanelReward.gameObject.SetActive(true);
    }

    public void CallAnimationClose() // => CanvasGame - ButtonCloseRewardBasicPanel - OnClick
    {
        buttonClosePanelReward.interactable = false;
        GameAnims.moveLabelAnimation(gameObject, parentForFly, imageCoinIncrease, startFlyPoint.localPosition, pointFly.localPosition, 1, ArrivalEndPoint);

        foreach (Transform child in panelReward.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Image>() == null)
                child.GetComponent<Text>().DOFade(0, 1);
            else
                child.GetComponent<Image>().DOFade(0, 1);
        }
    }

    private void ArrivalEndPoint()
    {
        GameAnims.lerpTextLabel(Income.In.TextCoins, GameManager.In.Coins, GameManager.In.Coins + rewardCoins, 1, StartInicState);
        Income.In.ChangeCoins(rewardCoins);
    }

    private void StartInicState()
    {
        buttonClosePanelReward.gameObject.SetActive(false);
        panelReward.color = new Color(0, 0, 0, 0.713f);
        Transform[] childs = panelReward.GetComponentsInChildren<Transform>();
        for (int i = 1; i < childs.Length; i++) // i = 1 ; miss the panelReward
        {
            if (childs[i].GetComponent<Image>() == null)
                childs[i].GetComponent<Text>().DOFade(1, 0);
            else
                childs[i].GetComponent<Image>().DOFade(1, 0);
        }
        buttonClosePanelReward.interactable = true;

        buttonClosePanelReward.GetComponent<Image>().raycastTarget = true; // so that after the beginning of the animation of the disappearance of the black panel, you could immediately use another game. No delay
    }
}
