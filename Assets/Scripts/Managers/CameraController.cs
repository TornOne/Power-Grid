using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float moveSpeed;
	bool isMoving = false;

	void Update() {
		if (!isMoving) {
			float currentRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
			transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Mathf.Cos(currentRotation) + Input.GetAxisRaw("Vertical") * Mathf.Sin(currentRotation),
			                                 0,
			                                 Input.GetAxisRaw("Vertical") * Mathf.Cos(currentRotation) - Input.GetAxisRaw("Horizontal") * Mathf.Sin(currentRotation)) * moveSpeed * Time.deltaTime;

			if (Input.GetButtonDown("RotateLeft")) {
				StartCoroutine(Turn(new Vector3(transform.position.x + Mathf.Sin(currentRotation) * 5, 0, transform.position.z + Mathf.Cos(currentRotation) * 5), 90));
			} else if (Input.GetButtonDown("RotateRight")) {
				StartCoroutine(Turn(new Vector3(transform.position.x + Mathf.Sin(currentRotation) * 5, 0, transform.position.z + Mathf.Cos(currentRotation) * 5), -90));
			}

			/*Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			if (mousePos.x < 0.05f && mousePos.x >= 0) {
				transform.position += new Vector3(-Mathf.Cos(currentRotation), 0, Mathf.Sin(currentRotation)) * moveSpeed * Time.deltaTime;
			} else if (mousePos.x > 0.95f && mousePos.x <= 1) {
				transform.position += new Vector3(Mathf.Cos(currentRotation), 0, -Mathf.Sin(currentRotation)) * moveSpeed * Time.deltaTime;
			}
			if (mousePos.y < 0.05f && mousePos.y >= 0) {
				transform.position += new Vector3(-Mathf.Sin(currentRotation), 0, -Mathf.Cos(currentRotation)) * moveSpeed * Time.deltaTime;
			} else if (mousePos.y > 0.95f && mousePos.y <= 1) {
				transform.position += new Vector3(Mathf.Sin(currentRotation), 0, Mathf.Cos(currentRotation)) * moveSpeed * Time.deltaTime;
			}*/
		}
	}

	IEnumerator Turn(Vector3 position, float angle) {
		isMoving = true;
		float duration = 0.5f;
		float endTime = Time.time + duration;
		float remainingAngle = angle;

		while (Time.time < endTime) {
			float turn = angle * Time.deltaTime / duration;
			transform.RotateAround(position, Vector3.up, turn);
			remainingAngle -= turn;
			yield return null;
		}
		transform.RotateAround(position, Vector3.up, remainingAngle);

		isMoving = false;
	}
}
