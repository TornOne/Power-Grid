using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {
	public float money = 0;
	Text moneyText;

    private static MoneyTracker moneyTracker;

	void Start() {
		moneyText = GameObject.Find("Money").GetComponent<Text>();
	}

	void Update() {
		moneyText.text = "" + Mathf.RoundToInt(money) + "₡";
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
