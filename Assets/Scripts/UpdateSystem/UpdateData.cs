using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateData : MonoBehaviour
{
    //////////////////////////////////////////////// RULES \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    // size length allCostUpdates = size length data = maxUpdate
    // key Dictionary updates = NameObjectButtonUpdate
    //////////////////////////////////////////////// RULES \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    public static UpdateData In;

    public class Update
    {
        public int IndexUpdate;
        public int CurrentUpdate;
        public int MaxUpdate; // plus zero element (amount - 1)
        public double[] AllCostUpdates;
        public List<double[]> Data;
        public int Prestige;
        public Button ButtonUpdate;

        public double GetData(int indexData, int updateLvl = 0)
        {
            updateLvl += CurrentUpdate;
            return Data[indexData][updateLvl];
        }
    }
    public Dictionary<string, Update> Updates = new Dictionary<string, Update>();

    public Button DamageButton;
    public Button ProfitButton;
    public Button EnergyButton;

    private int maxUpdateLvlDamage = 50;
    private int maxUpdateLvlProfit = 50;
    private int maxUpdateLvlEnergy = 50;

    public void SetupUpdatesSystem() // -> GameManager - Awake()
    {
        /////  Inicialization Update DAMAGE
        double basicCostDamage = 10;
        double[] allCostDamage = new double[maxUpdateLvlDamage];
        double[] amountDamage = new double[maxUpdateLvlDamage];
        amountDamage[0] = 1;
        for (int i = 0; i < maxUpdateLvlDamage; i++)
        {
            if (i == 0)
            {
                allCostDamage[i] = basicCostDamage;
            }
            else
            {
                amountDamage[i] = amountDamage[i - 1] + 1;
                allCostDamage[i] = allCostDamage[i - 1] * 1.41f;
            }
        }     

        Update damage = new Update();
        
        damage = new Update
        {
            CurrentUpdate = 0,
            MaxUpdate = maxUpdateLvlDamage - 1,
            AllCostUpdates = allCostDamage,
            Data = new List<double[]> { amountDamage },
            ButtonUpdate = DamageButton,
        };
        Updates.Add("DAMAGE", damage);
        DamageButton.name = "DAMAGE";
        ///// Inicialization Update DAMAGE

        /////  Inicialization Update PROFIT
        double basicCostProfit = 15;
        double[] allCostProfit = new double[maxUpdateLvlProfit];
        double[] amountProfit = new double[maxUpdateLvlProfit];
        amountProfit[0] = 0;
        for (int i = 0; i < maxUpdateLvlProfit; i++)
        {
            if (i == 0)
            {
                allCostProfit[i] = basicCostProfit;
            }
            else
            {
                amountProfit[i] = amountProfit[i - 1] + 1;
                allCostProfit[i] = allCostProfit[i - 1] * 1.41f;
            }
        }

        Update profit = new Update();

        profit = new Update
        {
            CurrentUpdate = 0,
            MaxUpdate = maxUpdateLvlProfit - 1,
            AllCostUpdates = allCostProfit,
            Data = new List<double[]> { amountProfit },
            ButtonUpdate = ProfitButton,
        };
        Updates.Add("PROFIT", profit);
        ProfitButton.name = "PROFIT";
        ///// Inicialization Update PROFIT

        /////  Inicialization Update ENERGY
        double basicCostEnergy = 20;
        double[] allCostEnergy = new double[maxUpdateLvlEnergy];
        double[] amountEnergy = new double[maxUpdateLvlEnergy];
        amountEnergy[0] = 0;
        for (int i = 0; i < maxUpdateLvlEnergy; i++)
        {
            if (i == 0)
            {
                allCostEnergy[i] = basicCostEnergy;
            }
            else
            {
                amountEnergy[i] = amountEnergy[i - 1] + 1;
                allCostEnergy[i] = allCostEnergy[i - 1] * 1.41f;
            }
        }

        Update energy = new Update();

        energy = new Update
        {
            CurrentUpdate = 0,
            MaxUpdate = maxUpdateLvlEnergy - 1,
            AllCostUpdates = allCostEnergy,
            Data = new List<double[]> { amountEnergy },
            ButtonUpdate = EnergyButton,
        };
        Updates.Add("ENERGY", energy);
        EnergyButton.name = "ENERGY";
        ///// Inicialization Update ENERGY



        SaveManager.In.LoadSave();
        EnergyPointSystem.In.EnableEnergyPoints();
    }

    public void CheckAvailableUpdate() // -> Income - OutputProcessing()
    {
        List<string> keys = new List<string>(Updates.Keys); // necessarily through a copy is necessary! not via link
        foreach (string key in keys)
        { // SendMessageOptions.DontRequireReceiver - In order to avoid red errors in the tutorial (update buttons are hidden)
            Updates[key].ButtonUpdate.SendMessage("SetStateButton",SendMessageOptions.DontRequireReceiver); // Function is called through different scripts = UIUpdateBehavior // SimpleUIUpdateBehavior. Cannot be called via GetComponent of the script name without if else
        }
    }

    private double PercentOfNumber(double number, int percent)
    {
        return number * percent / 100f;
    }
}
