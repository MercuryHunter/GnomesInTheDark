using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomUpdater : MonoBehaviour {

	private PlayerMovement playerMovement;

	public void Start() {
		playerMovement = GetComponentInParent<PlayerMovement>();
	}
	
	private void OnTriggerEnter(Collider other) {
		if (other.transform.tag.Contains("Floor")) {
			playerMovement.allowJump();
		}
		// TODO: This should have some sort of timing element.
		if (other.transform.tag.Contains("PlayerHead")) {
			Debug.Log("Trying to attach");
			other.transform.GetComponent<TwoPlayerCoordination>().attachOtherPlayer(transform.parent.gameObject);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.transform.tag.Contains("PlayerHead")) {
			Debug.Log("Detaching");
			other.transform.GetComponent<TwoPlayerCoordination>().detach(transform.parent.gameObject);
		}
	}
}
