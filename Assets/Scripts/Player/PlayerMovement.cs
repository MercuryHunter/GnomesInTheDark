using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float walkspeed = 5.0f;
	public float runspeed = 9.0f;

	/*
	public float timeBetweenJumps = 1.3f;
	private float timeToNextJump;
	*/
	private bool allowMovement;
	private bool canJump;

	Rigidbody playerRigidbody;
	private BaseController controller;
	private Animation anim;
	
	void Start () {
		// TODO: Put cursor stuff in mouse controller script?
		Cursor.lockState = CursorLockMode.Locked;
		
		playerRigidbody = GetComponent<Rigidbody> ();
		// TODO: Think about jump timing - check if on floor instead of timing?
		//timeToNextJump = 0;
		
		controller = GetComponent<BaseController>();
		anim = GetComponentInChildren<Animation>();

		allowMovement = true;
		canJump = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Cursor Stuff
		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
		
		float moveHorizontal = controller.getXMovement();
		float moveVertical = controller.getYMovement();

		if (allowMovement) {
			Move(moveHorizontal, moveVertical);
			// Rotation is done by the camera
			Animate(moveHorizontal, moveVertical);
		}
		
        //Jumping(Space bar)
		//timeToNextJump -= Time.deltaTime;
        if (canJump && controller.isJumpingPressed()) { //timeToNextJump <= 0) {
            Jump();
        }
	}

	private void Move(float moveHorizontal, float moveVertical) {
		bool running = controller.sprint();
		float currentSpeed = running ? runspeed : walkspeed;
		
		Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
		movement = movement.normalized * currentSpeed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
    }

    private void Jump() {
        // TODO: animation change here
	    //timeToNextJump = timeBetweenJumps;
	    canJump = false;
	    GetComponent<Rigidbody>().detectCollisions = true;
        playerRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }

	public void Throw() {
		canJump = false;
		GetComponent<Rigidbody>().detectCollisions = true;
		playerRigidbody.AddForce((Vector3.up + Vector3.forward) * 14, ForceMode.Impulse);
	}

	private void Animate(float h, float v) {
		bool walking = h != 0f || v != 0f;

		if (walking) { anim.CrossFade("Wizard_Run");}
		else { anim.CrossFade("Wizard_Idle"); }
	}

	public void attached() {
		allowMovement = false;
		canJump = true;
		Animate(0, 0);
	}

	public void detach() {
		allowMovement = true;
	}

	public void allowJump() {
		canJump = true;
	}
}
