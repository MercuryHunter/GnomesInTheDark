using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour, BaseController {
	
	public bool isJumpingPressed() {
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	public float getXMovement() {
		return Input.GetAxis("Horizontal");
	}

	public float getYMovement() {
		return Input.GetAxis("Vertical");
	}
	
	public float getXLook() {
		return Input.GetAxis("Mouse X");
	}

	public float getYLook() {
		return Input.GetAxis("Mouse Y");
	}
	
	public bool toggleLight() {
		return Input.GetKeyDown(KeyCode.F);
	}
	
	public bool increaseLight() {
		return Input.GetKeyDown(KeyCode.Equals);
	}

	public bool decreaseLight() {
		return Input.GetKeyDown(KeyCode.Minus);
	}
	
	public bool interact() {
		return Input.GetKeyDown(KeyCode.E);
	}

	public bool inventory() {
		return Input.GetKeyDown(KeyCode.I);
	}
}
