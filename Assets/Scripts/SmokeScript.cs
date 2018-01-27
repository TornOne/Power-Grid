using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour {
	public float lifeTime;
	float age = 0;
	public Vector3 moveDirection;
	Material material;

	void Start() {
		material = GetComponent<MeshRenderer>().material;
		Destroy(gameObject, lifeTime);
	}

	void Update() {
		age += Time.deltaTime;
		transform.Translate(moveDirection * Time.deltaTime, Space.World);
		Color color = material.color;
		material.color = new Color(color.r, color.g, color.b, 1 - age / lifeTime);
	}
}
