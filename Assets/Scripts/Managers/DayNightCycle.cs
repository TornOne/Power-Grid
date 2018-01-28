using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
	public float dayLength;
	private new Light light;
	private Camera mainCam;
	private float timeOfDay = 0;
	private Color dayColor = new Color(1, 0.961f, 0.843f);
	private Color dayBackColor = new Color(0.58f, 0.722f, 0.941f);
	private Color sunsetColor = new Color(0.85f, 0.22f, 0.075f);
	private Color nightColor = new Color(0.071f, 0.02f, 0.286f);
	private Color nightBackColor = new Color(0, 0, 0);

	void Start() {
		light = GetComponent<Light>();
		mainCam = Camera.main;
	}

	void Update () {
		timeOfDay = (timeOfDay + Time.deltaTime / dayLength) % 1f;

		transform.localEulerAngles = new Vector3(360 * timeOfDay, -30, 0);

		Color lightColor, backgroundColor;
		if (timeOfDay < 0.1f) {
			lightColor = Color.Lerp(sunsetColor, dayColor, timeOfDay * 10);
			backgroundColor = Color.Lerp(nightBackColor, dayBackColor, timeOfDay * 5 + 0.5f);
		} else if (timeOfDay < 0.4f) {
			lightColor = dayColor;
			backgroundColor = dayBackColor;
		} else if (timeOfDay < 0.5f) {
			lightColor = Color.Lerp(dayColor, sunsetColor, (timeOfDay - 0.4f) * 10);
			backgroundColor = Color.Lerp(dayBackColor, nightBackColor, (timeOfDay - 0.4f) * 5);
		} else if (timeOfDay < 0.6f) {
			lightColor = Color.Lerp(sunsetColor, nightColor, (timeOfDay - 0.5f) * 10);
			backgroundColor = Color.Lerp(dayBackColor, nightBackColor, (timeOfDay - 0.4f) * 5);
		} else if (timeOfDay < 0.9f) {
			lightColor = nightColor;
			backgroundColor = nightBackColor;
		} else {
			lightColor = Color.Lerp(nightColor, sunsetColor, (timeOfDay - 0.9f) * 10);
			backgroundColor = Color.Lerp(nightBackColor, dayBackColor, (timeOfDay - 0.9f) * 5);
		}

		light.color = lightColor;
		mainCam.backgroundColor = backgroundColor;
	}
}
