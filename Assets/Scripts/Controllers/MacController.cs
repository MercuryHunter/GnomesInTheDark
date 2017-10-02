using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacController : MonoBehaviour, BaseController {
	
	/* 
	===================== Axes =====================
							Win.	Mac.	Linux.
	Left Stick X Axis:		1		1		1	
	Left Stick Y Axis:		2		2		2
	Right Stick X Axis:		4		3		4	
	Right Stick Y Axis:		5		4		5	
	D-Pad X Axis:			6				7	On Linux, only wired controllers support using the D-Pad as axises
	D-Pad Y Axis:			7				8
	Triggers:				3					The left trigger is represented by the range -1 to 0, while the right trigger is represented by the range 0 to 1.
	Left Trigger:			9		5		3	Windows supports a 0 to 1 range for both triggers. Mac OS X supports -1 to 1, however the trigger initially starts at 0 until it is first used.
	Right Trigger:			10		6		6
	
	==================== Buttons ===================
							Win.	Mac.	Linux.
	A Button:				0		16		0	
	B Button:				1		17		1	
	X Button:				2		18		2	
	Y Button:				3		19		3	
	Left Bumper:			4		13		4	
	Right Bumper:			5		14		5	
	Back Button:			6		10		6	
	Start Button:			7		9		7	
	Left Stick Click:		8		11		9	
	Right Stick Click:		9		12		10	
	D-Pad Up:						5		13	On Linux, only wireless controllers support using the D-Pad as buttons
	D-Pad Down:						6		14
	D-Pad Left:						7		11
	D-Pad Right:					8		12
	Xbox Button:					15	
	*/
	
	string getJoystickAxisString(int joystickNumber, int axis) {
		return "joystick " + joystickNumber + " axis " + axis;
	}

	string getJoystickButtonString(int joystickNumber, int buttonNumber) {
		return "joystick " + joystickNumber + " button " + buttonNumber;
	}
	
	private int joystickNumber;
	private bool sprinting;
	private Vector2 movement;

	public MacController(int joystickNumber) {
		this.joystickNumber = joystickNumber;
	}

	public void setJoyStickNumber(int joystickNumber) {
		this.joystickNumber = joystickNumber;
	}
	
	// A
	public bool isJumpingPressed() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 16));
	}
	
	// Left Stick
	public float getXMovement() {
		movement.x = Input.GetAxis(getJoystickAxisString(joystickNumber, 1));
		return movement.x;
	}

	// Left Stick
	public float getYMovement() {
		movement.y = -Input.GetAxis(getJoystickAxisString(joystickNumber, 2));
		return movement.y;
	}

	public bool sprint() {
		bool input = Input.GetKeyDown(getJoystickButtonString(joystickNumber, 11));
		if (input) sprinting = true;
		else if (sprinting) {
			// If movement is sufficiently small, we're no longer sprinting
			if (movement.magnitude < 0.1) sprinting = false;
		}
		return sprinting;
	}

	// Right Stick
	public float getXLook() {
		return Input.GetAxis(getJoystickAxisString(joystickNumber, 3));
	}

	// Right Stick
	public float getYLook() {
		return - Input.GetAxis(getJoystickAxisString(joystickNumber, 4));
	}
	
	// Right Bumper
	public bool toggleLight() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 14));
	}
	
	// D-Pad Up
	public bool increaseLight() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 5));
	}

	// D-Pad Down
	public bool decreaseLight() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 6));
	}
	
	// X
	public bool interact() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 18));
	}

	// Y
	public bool inventory() {
		return Input.GetKeyDown(getJoystickButtonString(joystickNumber, 19));
	}
}
