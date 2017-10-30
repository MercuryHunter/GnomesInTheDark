using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour {
	// Handles the players use of teleporters
	float teleportContactTimeRequired = 0.5f;
	float teleportContactTimeCurrent = 0;
    int playerNum;
    BaseController controller;

	private PlayerCamera camera;
	private BaseController controller;

	void Awake() {
		camera = GetComponentInChildren<PlayerCamera>();
        
	}
    private void Start()
    {
        playerNum = Convert.ToInt32(gameObject.name.Substring(6, 1));
	    controller = GetComponent<BaseController>();
    }

    void Update() {
		teleportContactTimeCurrent -= Time.deltaTime;
	}

	void OnTriggerStay(Collider other) {
		// TODO: UI Text for player about this
		if (controller.interact() && teleportContactTimeCurrent <= 0 && other.tag.Equals("Teleporter")) {
            int levelNumber = Convert.ToInt32(other.transform.parent.name.Substring(10,1));
            if (other.gameObject.name == "Location1")
            {
                levelNumber++;
            }
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
            GameObject.Find("GameManager").GetComponent<GameManager>().changeLevel(levelNumber, playerNum);
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
