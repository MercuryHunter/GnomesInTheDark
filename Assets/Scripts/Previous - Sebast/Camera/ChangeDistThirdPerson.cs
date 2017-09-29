using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDistThirdPerson : MonoBehaviour {

	public GameObject slider;

	public void ChangeDist(float d){
		slider.GetComponentInParent<ThirdPersonCameraSebast> ().ChangeDistToPlayer (d);
	}
}
