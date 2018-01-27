using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
	public float dayLength;

	void Update () {
		transform.Rotate(360 * Time.deltaTime / dayLength, 0, 0, Space.World);
	}
}
