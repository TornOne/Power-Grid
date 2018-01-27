using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLightEnabler : MonoBehaviour {
	public Material material;
	Color unlit = new Color(0, 0, 0.175f);
	Color lit = new Color(1, 0.914f, 0.149f);
	public EnergyConsumer consumer;

	void Start() {
		material.color = unlit;
	}

	void FixedUpdate() {
		if (consumer.hasEnergy) {
			if (material.color == unlit) {
				material.color = lit;
			}
		} else if (material.color == lit) {
			material.color = unlit;
		}
	}
}
