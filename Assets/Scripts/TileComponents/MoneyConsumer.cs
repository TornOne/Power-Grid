using UnityEngine;

public class MoneyConsumer : MonoBehaviour {
	public float moneyPerSecond;
	MoneyTracker moneyTracker;

	void Start() {
		moneyTracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyTracker>();
	}

	void Update() {
		moneyTracker.money -= moneyPerSecond * Time.deltaTime;
	}
}
