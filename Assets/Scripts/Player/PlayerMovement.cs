using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float walkspeed = 5.0f;
	public float runspeed = 9.0f;

	public float timeBetweenJumps = 1.3f;
	private float timeToNextJump;

    private Animator animator;
    public Animation anim;
    

	Rigidbody playerRigidbody;
	
	void Start () {
		// TODO: Put cursor stuff in mouse controller script?
		Cursor.lockState = CursorLockMode.Locked;
		
		playerRigidbody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator>();
        
		// TODO: Think about jump timing - check if on floor instead of timing?
		timeToNextJump = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Move (moveHorizontal, moveVertical);
        // Rotation is done by the camera
        Animating(moveHorizontal, moveVertical);
        
		if (Input.GetKeyDown ("escape")) {
            Cursor.lockState = CursorLockMode.None;
		}
		
        //Jumping(Space bar)
		timeToNextJump -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeToNextJump <= 0) {
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

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger");
        if (other.gameObject.tag == "Item")
        {

            Inventory.inItemTrigger = true;
            Inventory.item = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Inventory.inItemTrigger = false;
            Inventory.item = null;
        }
    }


    private void Animating(float mh, float mv)
    {
        bool walking = mh != 0f || mv != 0f;
        animator.SetBool("IsWalking", walking);
		if (walking) anim.CrossFade("Wizard_Run"); 
		else { anim.CrossFade("Wizard_Idle"); };
        
    }
}
