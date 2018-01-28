using UnityEngine;

public class WindmillRotator : MonoBehaviour {
	void Update() {
		transform.rotation *= Quaternion.Euler(0, 360 * Time.deltaTime, 0);
	}
}
