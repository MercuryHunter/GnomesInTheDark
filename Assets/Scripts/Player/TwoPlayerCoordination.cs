using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerCoordination : MonoBehaviour {

	private bool attached;
	private GameObject attachedPlayer;
	private BaseController controller;

	// Use this for initialization
	void Start () {
		attached = false;
		controller = GetComponentInParent<BaseController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (attached) {
			attachedPlayer.transform.position = transform.position;
			attachedPlayer.transform.rotation = transform.rotation;
			
			if (controller.throwing()) {
				PlayerMovement playerMovement = attachedPlayer.GetComponent<PlayerMovement>();
				playerMovement.Throw();
				//detach(attachedPlayer);
			}
		}
	}

	public void detach(GameObject player) {
		if (attached && player == attachedPlayer) {
			attached = false;
			
			attachedPlayer.GetComponent<PlayerMovement>().detach();
			
			Rigidbody rb = attachedPlayer.GetComponent<Rigidbody>();
			rb.detectCollisions = true;
			rb.useGravity = true;
			
			PlayerCamera playerCamera = attachedPlayer.GetComponentInChildren<PlayerCamera>();
			playerCamera.rotationLock = false;
			playerCamera.setBodyPointVector(transform);
			
			attachedPlayer = null;
		}
	}

	public bool attachOtherPlayer(GameObject player) {
		if (!attached) {
			attached = true;
			attachedPlayer = player;
			
			attachedPlayer.GetComponent<PlayerMovement>().attached();
			
			Rigidbody rb = attachedPlayer.GetComponent<Rigidbody>();
			rb.detectCollisions = false;
			rb.useGravity = false;

			// TODO: Consider a lerp here or in player cam
			PlayerCamera playerCamera = attachedPlayer.GetComponentInChildren<PlayerCamera>();
			playerCamera.setBodyPointVector(transform);
			playerCamera.rotationLock = true;
			
			return true;
		}
		return false;
	}

	public GameObject getAttachedPlayer() {
		return gameObject;
	}
	
}
