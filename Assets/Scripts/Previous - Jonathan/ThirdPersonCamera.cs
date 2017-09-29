using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	// With thanks to: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230

	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
	Vector3 lookOffset = new Vector3 (0, 0.7f, 0);

	void Start () {
		offset = target.transform.position - transform.position;
	}

	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * 2.5f); // Smooth motion
		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt(target.transform.position + lookOffset);
	}

}
