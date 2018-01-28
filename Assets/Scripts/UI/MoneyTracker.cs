using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {
	public float money = 0;
    public float totalIncome;
    public float totalUpkeep;

    private static MoneyTracker moneyTracker;

	void Start() {

	}

	void Update() {

	}

    public static MoneyTracker GetMoneyTracker() {
        if (moneyTracker == null) {
            moneyTracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MoneyTracker>();
        }

        return moneyTracker;
    }

    public bool CanAfford(float cost) {
        if (money - cost > 0)
            return true;

        return false;
    }

    public void BuyFor(float cost) {
        money -= cost;
    }

    public void SellFor(float cost) {
        money += cost;
    }
}
