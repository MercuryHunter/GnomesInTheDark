using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public float mouseSens = 5.0f;
	public Transform pivot;
	public float dist;
	public Vector2 pitchMinMax = new Vector2 (0, 85);

	public float smoothing = 0.12f;
	Vector3 rotSmooth;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	public GameObject canvas;

	void Start(){

	}

	//void LateUpdate(){
	void FixedUpdate(){
		yaw += Input.GetAxis ("Mouse X") * mouseSens;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSens;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotSmooth, smoothing);

		transform.eulerAngles = currentRotation;

		transform.position = pivot.position - transform.forward * dist;


		if (Input.GetKeyDown ("m")) {
			canvas.SetActive (!canvas.activeInHierarchy);
		}

	}



	public void ChangeDistToPlayer(float d){
		dist = d;
	}


}
