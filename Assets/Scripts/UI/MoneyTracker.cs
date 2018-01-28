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

    public bool HaveMoney(float cost) {
        return true;
    }
}
