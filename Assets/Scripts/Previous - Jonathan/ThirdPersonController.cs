using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonController : MonoBehaviour {

	public Transform cameraToMoveFrom;
	public float speed;

	AudioSource stepAudio; 

	Rigidbody Rigidbody;
	Animator Animator;

	// Use this for initialization
	void Start () {
		Animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody>();
		stepAudio = GetComponent<AudioSource> ();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Move (h, v);
	}

	private void Move(float h, float v) {
		if (h != 0 || v != 0)
			if(!stepAudio.isPlaying)
				stepAudio.Play();
		
		Vector3 moveVector = v * cameraToMoveFrom.forward + h * cameraToMoveFrom.right;//new Vector3 (h * cameraToPlayer.x, 0f, v * cameraToPlayer.z);// camera.right;
		moveVector.y = 0;

		moveVector = moveVector.normalized * Time.deltaTime * speed;

		Rigidbody.MovePosition (transform.position + moveVector);

		//float turnAmount = Mathf.Atan2(moveVector.x, moveVector.z);
		float forwardAmount = (Mathf.Abs(moveVector.z) + Mathf.Abs(moveVector.x)) * 20;

		Animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
		//Animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
	}
}
