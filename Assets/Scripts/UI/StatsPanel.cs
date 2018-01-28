using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour {

    public Text moneyText;
    public Text incomeText;
    public Text upkeepText;
    public Text productioText;
    public Text consumptionText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    moneyText.text = "Money: " + Mathf.RoundToInt(MoneyTracker.GetMoneyTracker().money) + "₡";
	    incomeText.text = "Global Income: " + Mathf.RoundToInt(MoneyTracker.GetMoneyTracker().totalIncome) + "₡/t";
	    upkeepText.text = "Global Upkeep: " + Mathf.RoundToInt(MoneyTracker.GetMoneyTracker().totalUpkeep) + "₡/t";
        productioText.text = "Global Production: " + Mathf.RoundToInt(PowerTracker.GetPowerTracker().totalProduction) + "PU/t";
	    consumptionText.text = "Global Consumption: " + Mathf.RoundToInt(PowerTracker.GetPowerTracker().totalConsumption) + "PU/t";
    }
}
