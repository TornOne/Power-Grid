using UnityEngine;

public class EnergyConsumer : MonoBehaviour {
	public float energyConsumption, moneyPerSecond;
	EnergyTransmitter transmitter;
	MoneyTracker moneyTracker;
	public bool hasEnergy;

	void Start() {
		transmitter = GetComponent<EnergyTransmitter>();
		moneyTracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MoneyTracker>();
	}

	void FixedUpdate() {
		float energyThisFrame = energyConsumption * Time.fixedDeltaTime;

		if (transmitter.currentEnergy > energyThisFrame) {
			transmitter.currentEnergy -= energyThisFrame;
			moneyTracker.money += moneyPerSecond * Time.fixedDeltaTime;
			hasEnergy = true;
		} else {
			hasEnergy = false;
		}
	}
}
