using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Vector2 mouseLook;

	Vector2 smoothV;

	public float Sensitivity = 5.0f;
	public float smoothing = 2.0f;

	public GameObject target;

	void Update () {
		var camTurn = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		camTurn = Vector2.Scale (camTurn, new Vector2 (Sensitivity * smoothing, Sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, camTurn.x, 1f/ smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, camTurn.y, 1f/ smoothing);
		mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		target.transform.rotation = Quaternion.AngleAxis (mouseLook.x, target.transform.up);
	}
}
