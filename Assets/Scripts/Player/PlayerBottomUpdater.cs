using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomUpdater : MonoBehaviour {

	private PlayerMovement playerMovement;

	private float attachedTimeToWaitCurrent = 0f;
	private float attachedTimeToWait = 0.3f;

	public void Start() {
		playerMovement = GetComponentInParent<PlayerMovement>();
	}

	public void Update() {
		attachedTimeToWaitCurrent -= Time.deltaTime;
	}
	
	private void OnTriggerEnter(Collider other) {
		if (other.transform.tag.Contains("Floor")) {
			playerMovement.allowJump();
		}
		// TODO: This should have some sort of timing element.
		if (other.transform.tag.Contains("PlayerHead")) {
			bool attached = other.transform.GetComponent<TwoPlayerCoordination>().attachOtherPlayer(transform.parent.gameObject);
			if (attached) attachedTimeToWaitCurrent = attachedTimeToWait;
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.transform.tag.Contains("Floor")) {
			playerMovement.allowJump();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.transform.tag.Contains("PlayerHead")) {
			//if (attachedTimeToWaitCurrent <= 0.0f) {
				other.transform.GetComponent<TwoPlayerCoordination>().detach(transform.parent.gameObject);
			//}
		}
	}
}
