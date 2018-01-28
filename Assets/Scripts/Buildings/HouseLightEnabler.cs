using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLightEnabler : MonoBehaviour {
	Material material;
	Color unlit = new Color(0, 0, 0.175f);
	Color lit = new Color(1, 0.914f, 0.149f);
	public EnergyConsumer consumer;

	void Start() {
		if (gameObject.name = "School") {
			material = GetComponent<MeshRenderer>().materials[2];
		} else {
			material = GetComponent<MeshRenderer>().material;
		}
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
