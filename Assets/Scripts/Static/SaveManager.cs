using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager In;

    [SerializeField] private bool canNotSave = default;

    private TimeSpan timeDifference;

    public void Save() // -> Income - ChangeCoins()
    {
        if (canNotSave) return;

        PlayerPrefs.SetFloat("Version", 1f);


        string updatesSave = "";
        Dictionary<string, UpdateData.Update> updates = UpdateData.In.Updates;
        List<string> keys = new List<string>(updates.Keys); // necessarily through a copy is necessary! not via link
        foreach (string key in keys)
        {
            updatesSave += key + "=" + updates[key].CurrentUpdate + ",";
        }

        PlayerPrefs.SetString("Coins", GameManager.In.Coins.ToString());
        PlayerPrefs.SetString("SaveUpdates", updatesSave);
        PlayerPrefs.SetInt("Round", RoundsSystem.In.CurrentRound);

        long currentTime = DateTime.Now.ToBinary();
        PlayerPrefs.SetString("QuitTime", currentTime.ToString());
        PlayerPrefs.SetString("OfflineEarningsOneSecond", GameManager.In.OfflineEarningsOneSecond.ToString());
    }

    public void LoadSave() // -> UpdateData - SetupUpdatesSystem()
    {
        string saveCoins = PlayerPrefs.GetString("Coins");

        if (saveCoins == "")
        {
            Income.In.ChangeCoins(0); 
            return;
        }

        double.TryParse(saveCoins, out GameManager.In.Coins);
        string saveUpdates;
   
        saveUpdates = PlayerPrefs.GetString("SaveUpdates");
        RoundsSystem.In.CurrentRound = PlayerPrefs.GetInt("Round");

        string[] openSave;
        openSave = saveUpdates.Split(new char[] { ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < openSave.Length - 1; i += 2)
        {
            int.TryParse(openSave[i + 1], out UpdateData.In.Updates[openSave[i]].CurrentUpdate);
        }

        double.TryParse(PlayerPrefs.GetString("OfflineEarningsOneSecond"), out GameManager.In.OfflineEarningsOneSecond);  // in order to initialize coin income in one second in UI ( Income.I.OutputProcessing();)
        Income.In.OutputProcessing(); // This line should be done after the initialization of updates //   Otherwise, update objects will be glow on zero update levels.

        SetupOfflineEarnings();
    }

    public void DeleteSave() // => CanvasGame - ButtonsDevPanel - TestsForDeveloper - TestSaveLoadButton - OnClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
        }
        else
        {
            SetupOfflineEarnings();
        }
    }

    private void CalculateTimeSpawn()
    {
        string quitTime = PlayerPrefs.GetString("QuitTime");
        long temp;
        Int64.TryParse(quitTime, out temp);
        DateTime quitTimeDT = DateTime.FromBinary(temp);
        timeDifference = DateTime.Now - quitTimeDT;
    }

    private void SetupOfflineEarnings()
    {
        CalculateTimeSpawn();

        if (timeDifference.TotalSeconds < 30) return; // so that after RewardVideo we do not open this panel
        if (GameManager.In.OfflineEarningsOneSecond == 0) return;

        float limitTotalSeconds = (float)timeDifference.TotalSeconds;
        
        double freeOfflineEarnings = (GameManager.In.OfflineEarningsOneSecond * limitTotalSeconds) / 10f;

        string textFreeOfflineEarnings = NumberSystem.Output(freeOfflineEarnings);
        double adsOfflineEarnings = freeOfflineEarnings * 5f;
        string textadsOfflineEarnings = NumberSystem.Output(adsOfflineEarnings);
        textadsOfflineEarnings += "(Ads)";

        UIGetBonusCoins uiOfflineEarnings = GameManager.In.PanelOfflineEarnings.GetComponent<UIGetBonusCoins>();
        uiOfflineEarnings.FreeCoins = freeOfflineEarnings;
        uiOfflineEarnings.AdsCoins = adsOfflineEarnings;
        uiOfflineEarnings.FreeButton.text = "+" + textFreeOfflineEarnings;
        uiOfflineEarnings.AdsButton.text = "+" + textadsOfflineEarnings;

        GameManager.In.PanelOfflineEarnings.SetActive(true);
    }
}
