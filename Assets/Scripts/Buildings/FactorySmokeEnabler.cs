using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySmokeEnabler : MonoBehaviour {
	public SmokeProducer smoker;
	public EnergyConsumer consumer;

	void FixedUpdate() {
		if (consumer.hasEnergy) {
			if (!smoker.enabled) {
				smoker.enabled = true;
			}
		} else if (smoker.enabled) {
			smoker.enabled = false;
		}
	}
}
