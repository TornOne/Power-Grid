using UnityEngine;

public class EnergyConsumer : MonoBehaviour {
	public float energyConsumption, moneyPerSecond;
	EnergyTransmitter transmitter;
	MoneyTracker moneyTracker;

	void Start() {
		transmitter = GetComponent<EnergyTransmitter>();
		moneyTracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyTracker>();
	}

	void FixedUpdate() {
		float energyThisFrame = energyConsumption * Time.fixedDeltaTime;

		if (transmitter.currentEnergy > energyThisFrame) {
			transmitter.currentEnergy -= energyThisFrame;
			moneyTracker.money += moneyPerSecond * Time.fixedDeltaTime;
		}
	}
}
