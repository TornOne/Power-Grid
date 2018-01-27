using UnityEngine;

public class EnergyProducer : MonoBehaviour {
	public float energyProduction, moneyPerSecond;
	EnergyTransmitter transmitter;
	MoneyTracker moneyTracker;

	void Start() {
		transmitter = GetComponent<EnergyTransmitter>();
		moneyTracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MoneyTracker>();
	}

	void FixedUpdate() {
		transmitter.currentEnergy = Mathf.Min(transmitter.currentEnergy + energyProduction * Time.fixedDeltaTime, transmitter.energyCapacity);
		moneyTracker.money -= moneyPerSecond * Time.fixedDeltaTime;
	}
}
