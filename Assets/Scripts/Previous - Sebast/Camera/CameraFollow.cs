using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5.0f;

	public GameObject canvas;

	public Transform Object;

	float change = 1f;

	Vector3 offset;//distance to player

	void Start(){
		offset = transform.position - target.position;
	}


	void FixedUpdate(){


		//Vector3 targetPos = target.position + offset;
		Vector3 targetPos = (target.position + offset)*change;
		transform.position = Vector3.Lerp (transform.position, targetPos, smoothing * Time.deltaTime);


		if (Input.GetKeyDown ("m")) {
			canvas.SetActive (!canvas.activeInHierarchy);
		}

	}


	public void ChangeDistanceToPlayer(float d){

		change = d;

	}

}
