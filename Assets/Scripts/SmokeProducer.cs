using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeProducer : MonoBehaviour {
	public GameObject smokePuff;
	public Vector3 offset;
	public Vector3 size;
	public float lifeTime;
	public float moveDistance;
	public Color color;

	void Start() {
		StartCoroutine(SpawnSmoke());
	}

	IEnumerator SpawnSmoke() {
		while (true) {
			GameObject smoke = Instantiate(smokePuff, transform.position + offset, Random.rotation, transform);
			smoke.transform.localScale = size;
			SmokeScript smokeScript = smoke.GetComponent<SmokeScript>();
			smokeScript.lifeTime = lifeTime;
			smokeScript.moveDirection = new Vector3(Random.Range(0f, moveDistance / 2), moveDistance, Random.Range(0f, moveDistance / 2));

			yield return new WaitForSeconds(0.1f);
		}
	}
}
