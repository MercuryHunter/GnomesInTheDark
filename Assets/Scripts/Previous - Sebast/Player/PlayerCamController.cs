using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CamState {FIRSTPERSON, ISOMETRIC, THIRDPERSON};
//First person = 1
//Isometric = 2
//Third person = 3

public class PlayerCamController : MonoBehaviour {

	public GameObject player;
	CamState cState = CamState.ISOMETRIC;//Starting state

	public GameObject FirstPersonCam;
	public GameObject IsometricCam;
	public GameObject ThirdPersonCam;


	bool Alive;

	void Start () {
		Alive = true;
	}
	

	void FixedUpdate () {

		if (Input.GetKey ("1")) {
			cState = CamState.FIRSTPERSON;
		} else if (Input.GetKey ("2")) {
			cState = CamState.ISOMETRIC;
		} else if (Input.GetKey ("3")) {
			cState = CamState.THIRDPERSON;
		}

		if (Alive) {
			CamSwitching ();
		}
	}

	//Hard-coded and ugly, but if it gets the job done
	void CamSwitching(){
		if (cState == CamState.FIRSTPERSON) {
			
			//Cursor.lockState = CursorLockMode.Locked;

			FirstPersonCam.SetActive (true);
			IsometricCam.SetActive (false);
			ThirdPersonCam.SetActive (false);

			player.GetComponent<PlayerFirstPersonController> ().enabled = true;
			player.GetComponent<PlayerIsometricController> ().enabled = false;
			player.GetComponent<PlayerThirdPersonController> ().enabled = false;

		} else if (cState == CamState.ISOMETRIC) {
			
			//Cursor.lockState = CursorLockMode.None;

			FirstPersonCam.SetActive (false);
			IsometricCam.SetActive (true);
			ThirdPersonCam.SetActive (false);

			player.GetComponent<PlayerFirstPersonController> ().enabled = false;
			player.GetComponent<PlayerIsometricController> ().enabled = true;
			player.GetComponent<PlayerThirdPersonController> ().enabled = false;

		} else if (cState == CamState.THIRDPERSON) {

			//Cursor.lockState = CursorLockMode.Locked;

			FirstPersonCam.SetActive (false);
			IsometricCam.SetActive (false);
			ThirdPersonCam.SetActive (true);

			player.GetComponent<PlayerFirstPersonController> ().enabled = false;
			player.GetComponent<PlayerIsometricController> ().enabled = false;
			player.GetComponent<PlayerThirdPersonController> ().enabled = true;
		}
	}


	public void Died(){
		Alive = false;
	
	}

}
