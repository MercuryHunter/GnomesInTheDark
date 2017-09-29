using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour {

	public float radius = 1f;
	public float height = 1f;
	public float speedFactor = 0.5f;
	public GameObject target;
	Vector3 lookOffset = new Vector3 (0, 0.7f, 0);
	//private Vector3 offset;

	void Start () {
		//transform.position = new Vector3 (0, height, 0);
		//offset = transform.position - target.transform.position;
	}
	
	void FixedUpdate () {
		Vector3 rotation = new Vector3 (Mathf.Sin (Time.time) * radius * speedFactor, height, Mathf.Cos (Time.time) * radius * speedFactor);
		Vector3 finalPosition = target.transform.position + rotation;
		transform.position = finalPosition;
		transform.LookAt(target.transform.position + lookOffset);
	}
}
