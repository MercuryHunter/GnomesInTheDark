using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookVerticalBehaviour : MonoBehaviour {

	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;

	private Quaternion CameraTargetRotation;

	void Start () {
		CameraTargetRotation = transform.localRotation;
	}
	
	void Update () {
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		CameraTargetRotation *= Quaternion.Euler (-xRot, 0f, 0f);

		if(clampVerticalRotation)
			CameraTargetRotation = ClampRotationAroundXAxis (CameraTargetRotation);

		transform.localRotation = CameraTargetRotation;
	}

	// Standard Assets Rotation Clamping
	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
