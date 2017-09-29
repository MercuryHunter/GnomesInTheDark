using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseController {

	bool isJumpingPressed();

	float getXMovement();
	float getYMovement();
	
	float getXLook();
	float getYLook();

	bool toggleLight();
	bool increaseLight();
	bool decreaseLight();

	bool interact();
	bool inventory();
}
