using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsometricController : MonoBehaviour {

	public float walkspeed = 2.5f;
	public float runSpeed = 6.0f;

	public GameObject player;
	Rigidbody playerRigidbody;

	Vector3 movement;

	float camRayLegth = 100f;
	int floorMask;


	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;
		playerRigidbody = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask ("Floor");

	}


	void FixedUpdate () {

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		move (moveHorizontal, moveVertical);
		rotation ();
		Animation (moveHorizontal, moveVertical);


	}


	void move(float h, float v){

		movement.Set (h, 0.0f, v);

		bool running = Input.GetKey (KeyCode.LeftShift);
		float currentspeed = ((running) ? runSpeed : walkspeed);

		movement = movement.normalized * currentspeed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);

	}

	void rotation(){

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLegth, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0.0f;

			Quaternion newRotate = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotate);
		}
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
