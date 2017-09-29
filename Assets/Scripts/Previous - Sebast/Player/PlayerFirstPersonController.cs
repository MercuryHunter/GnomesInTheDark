using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstPersonController : MonoBehaviour {

	public float walkspeed = 2.5f;
	public float runspeed = 6.0f;

	public GameObject player;

	Vector3 movement;
	Rigidbody playerRigidbody;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		playerRigidbody = GetComponent<Rigidbody> ();
	}


	void FixedUpdate () {

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		move (moveHorizontal, moveVertical);
		rotation ();
		Animation (moveHorizontal, moveVertical);


		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

	}


	void move(float h, float v){

		movement.Set (h, 0.0f, v);

		bool running = Input.GetKey (KeyCode.LeftShift);
		float currentspeed = ((running) ? runspeed : walkspeed);

		h *= currentspeed;
		v *= currentspeed;
		h *= Time.deltaTime;
		v *= Time.deltaTime;

		transform.Translate (h, 0, v);

	}


	void rotation(){



	}

	void Animation(float h, float v){

		bool running = Input.GetKey (KeyCode.LeftShift);

		bool isMoving = h != 0.0f || v != 0.0f;
		/*
		if (isMoving) {//Is moving
			if (running) {ChangeAnimation (3);} //Running
			else {ChangeAnimation (2);}	//Walking
		} else {
			ChangeAnimation (1);//Not running idle
		}
		*/

	}
/*
	void ChangeFace (QuerySDEmotionalController.QueryChanSDEmotionalType faceNumber) {

		player.GetComponent<QuerySDEmotionalController>().ChangeEmotion(faceNumber);

	}


	void ChangeAnimation (int animNumber)
	{
		player.GetComponent<QuerySDMecanimController>().ChangeAnimation((QuerySDMecanimController.QueryChanSDAnimationType)animNumber);
	}
	*/
}
