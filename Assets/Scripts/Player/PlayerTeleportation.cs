using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour {
	// Handles the players use of teleporters
	float teleportContactTimeRequired = 0.5f;
	float teleportContactTimeCurrent = 0;

	private PlayerCamera camera;

	void Awake() {
		camera = GetComponentInChildren<PlayerCamera>();
	}

	void Update() {
		teleportContactTimeCurrent -= Time.deltaTime;
	}

	void OnTriggerStay(Collider other) {
		// TODO: UI Text for player about this
		if (Input.GetKeyDown(KeyCode.E) && teleportContactTimeCurrent <= 0 && other.tag.Equals("Teleporter")) {
			// Teleported, reset time
			teleportContactTimeCurrent = teleportContactTimeRequired;
		
			Transform newTransform =
				other.gameObject
					.GetComponentInParent<TeleporterManager>()
					.getOtherTeleporter(other)
					.transform;

			this.transform.position = newTransform.position;
			this.transform.eulerAngles = newTransform.eulerAngles;
			camera.setBodyPointVector(newTransform);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("Teleporter")) {
			// Left teleporter, so need to recontact for appropriate time now
			// May have issues with close together teleporters/edges of them
			teleportContactTimeCurrent = teleportContactTimeRequired;
		}
	}
}
