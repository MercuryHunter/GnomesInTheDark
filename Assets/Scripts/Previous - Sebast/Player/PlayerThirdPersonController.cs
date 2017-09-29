using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdPersonController : MonoBehaviour {

	public float walkspeed = 2.5f;
	public float runspeed = 6.0f;

	public float smoothTurning = 0.2f;
	float SmoothTurnVelocity;

	public float moveSmooth = 0.1f;
	float moveSmoothVelocity;


	float currentSpeed;

	public GameObject player;
	public Transform cam;

	Vector2 movement;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	

	void FixedUpdate () {

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		move (moveHorizontal, moveVertical);
		Animation (moveHorizontal, moveVertical);


		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

	}


	void move(float h, float v){

		movement.Set (h, v); 
		Vector2 movementDir = movement.normalized; 

		if (movementDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (movementDir.x, movementDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle (transform.eulerAngles.y, targetRotation,
				ref SmoothTurnVelocity, smoothTurning);
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float speed = 0.0f;
		if (running) {
			speed = runspeed * movementDir.magnitude;
		} else {
			speed = walkspeed * movementDir.magnitude;
		}
		currentSpeed = Mathf.SmoothDamp (currentSpeed, speed, ref moveSmoothVelocity, moveSmooth);

		transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);

	}


	void Animation (float h, float v){
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
