using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerCoordination : MonoBehaviour {

	private bool attached;
	private GameObject attachedPlayer;

	// Use this for initialization
	void Start () {
		attached = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (attached) {
			attachedPlayer.transform.position = transform.position;
			attachedPlayer.transform.rotation = transform.rotation;
		}
	}

	public void detach(GameObject player) {
		if (attached && player == attachedPlayer) {
			attachedPlayer.GetComponent<PlayerMovement>().detach();
			attachedPlayer.GetComponent<Rigidbody>().detectCollisions = true;
			attachedPlayer.GetComponent<Rigidbody>().useGravity = true;
			attached = false;
			attachedPlayer = null;
		}
	}

	public bool attachOtherPlayer(GameObject player) {
		if (!attached) {
			attached = true;
			attachedPlayer = player;
			attachedPlayer.GetComponent<PlayerMovement>().attached();
			attachedPlayer.GetComponent<Rigidbody>().detectCollisions = false;
			attachedPlayer.GetComponent<Rigidbody>().useGravity = false;
			return true;
		}
		return false;
	}

	public GameObject getAttachedPlayer() {
		return gameObject;
	}
	
}
