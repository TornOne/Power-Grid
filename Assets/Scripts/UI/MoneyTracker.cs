using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {
	public float money = 0;
	Text moneyText;

	void Start() {
		moneyText = GameObject.Find("Money").GetComponent<Text>();
	}

	void Update() {
		moneyText.text = "$" + Mathf.RoundToInt(money);
	}
}
