using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float walkspeed = 5.0f;
	public float runspeed = 9.0f;

	public float timeBetweenJumps = 1.3f;
	private float timeToNextJump;

	Rigidbody playerRigidbody;
	private BaseController controller;
	
	void Start () {
		// TODO: Put cursor stuff in mouse controller script?
		Cursor.lockState = CursorLockMode.Locked;
		
		playerRigidbody = GetComponent<Rigidbody> ();
		// TODO: Think about jump timing - check if on floor instead of timing?
		timeToNextJump = 0;
		
		controller = GetComponent<BaseController>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = controller.getXMovement();
		float moveVertical = controller.getYMovement();

		Move (moveHorizontal, moveVertical);
        // Rotation is done by the camera
        
		if (Input.GetKeyDown ("escape")) {
            Cursor.lockState = CursorLockMode.None;
		}
		
        //Jumping(Space bar)
		timeToNextJump -= Time.deltaTime;
        if (controller.isJumpingPressed() && timeToNextJump <= 0) {
            Jump();
        }
	}

	private void Move(float moveHorizontal, float moveVertical) {
		bool running = Input.GetKey (KeyCode.LeftShift);
		float currentSpeed = ((running) ? runspeed : walkspeed);
		
		Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
		movement = movement.normalized * currentSpeed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
        
    }

    private void Jump() {
        // TODO: animation change here
	    timeToNextJump = timeBetweenJumps;
        playerRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }

    
}
