using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDistIso : MonoBehaviour {

	public GameObject slider;

	public void ChangeDist(float d){
		slider.GetComponentInParent<CameraFollow> ().ChangeDistanceToPlayer (d);
	}
}
