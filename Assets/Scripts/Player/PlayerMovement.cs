﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float walkspeed = 2.5f;
	public float runspeed = 6.0f;
	
	Rigidbody playerRigidbody;
	
	void Start () {
		// TODO: Put cursor stuff in mouse controller script?
		Cursor.lockState = CursorLockMode.Locked;
		
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Move (moveHorizontal, moveVertical);
		Rotate();

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void Move(float moveHorizontal, float moveVertical) {
		bool running = Input.GetKey (KeyCode.LeftShift);
		float currentSpeed = ((running) ? runspeed : walkspeed);
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		movement = movement.normalized * currentSpeed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
	}

	private void Rotate() {
		// TODO: Rotate according to camera?
	}
}
