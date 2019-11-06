using UnityEngine;

public class UIGetCoins : MonoBehaviour
{
    private double[] coins = new double[] { 50000, 50000000, 50000000000, 50000000000000, 50000000000000000, 500000000000000000000.0, 500000000000000000000000.00, 500000000000000000000000000.00, 500000000000000000000000000000.00 };

    public void GetCoins (int indexButton) // => CanvasGame - ButtonsDevPanel - TestsForDeveloper - GetCoinsButton - OnClick()
    {
        Income.In.ChangeCoins(coins[indexButton]);
	}
}
