using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour, BaseController {
	
	public bool isJumpingPressed() {
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	public float getXMovement() {
		return Input.GetAxisRaw("Horizontal");
	}

	public float getYMovement() {
		return Input.GetAxisRaw("Vertical");
	}

	public bool sprint() {
		return Input.GetKey(KeyCode.LeftShift);
	}

	public float getXLook() {
		return Input.GetAxisRaw("Mouse X");
	}

	public float getYLook() {
		return Input.GetAxisRaw("Mouse Y");
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
	
	// B
	public bool throwing() {
		return Input.GetKeyDown(KeyCode.Q);
	}

	public bool up() {
		return Input.GetKeyDown(KeyCode.UpArrow);
	}

	public bool down() {
		return Input.GetKeyDown(KeyCode.DownArrow);
	}

	public bool left() {
		return Input.GetKeyDown(KeyCode.LeftArrow);
	}

	public bool right() {
		return Input.GetKeyDown(KeyCode.RightArrow);
	}
}
