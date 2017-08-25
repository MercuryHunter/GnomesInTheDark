using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Camera firstPerson, orbiting, thirdPerson;

	// Use this for initialization
	void Start () {
		//firstPerson = (Camera) GameObject.FindWithTag ("MainCamera");
		//orbiting = (Camera) GameObject.FindWithTag ("Orbiting");
		//thirdPerson = (Camera) GameObject.FindWithTag ("3rdPersonCamera");

		firstPerson.enabled = true;
		orbiting.enabled = false;
		thirdPerson.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			firstPerson.enabled = true;
			orbiting.enabled = false;
			thirdPerson.enabled = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			firstPerson.enabled = false;
			orbiting.enabled = true;
			thirdPerson.enabled = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			firstPerson.enabled = false;
			orbiting.enabled = false;
			thirdPerson.enabled = true;
		}
	}
}
